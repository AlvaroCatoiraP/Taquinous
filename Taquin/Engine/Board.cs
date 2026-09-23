using System.Collections.Generic;
using System;
using System.Linq;

namespace Taquin
{
    /// <summary>
    /// Represents the board of the Taquin game.
    /// </summary>
    public class Board
    {
        public const double NB_IMAGES = 31; // number of images on the resources folder

        private List<Tile> tiles;
        private List<List<Cell>> grid;
        private List<Tile> target;
        private int size;

        private int direction_Target; // 1 for vertical, 2 for horizontal
        private int position_target;
        private Random random;
        private List<int> allowed_indexes;

        /// <summary>
        /// Initializes a new instance of the <see cref="Board"/> class.
        /// </summary>
        /// <param name="size">Size of the board.</param>
        public Board(int size)
        {
            this.size = size;
            tiles = new List<Tile>();
            grid = new List<List<Cell>>();
            allowed_indexes = new List<int>();
            for (int i = 1; i < NB_IMAGES; i++) 
            {
                allowed_indexes.Add(i);
            }

            this.random = new Random();

            Init_Tiles();
            this.tiles = tiles.OrderBy(t => random.Next()).ToList();
            PlaceTiles();

            this.direction_Target = random.Next(1, 3);
            this.position_target = random.Next(0, size);
        }

        /// <summary>
        /// Initializes the tiles of the board.
        /// </summary>
        public static Board CreateChallenge()
        {
            Board board = new Board(3);
            // Défi en cinq mouvements : bas, bas, droite, droite, haut.
            int[,] values = { { 2, 3, 5 }, { 1, 4, 8 }, { 6, 7, 0 } };
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    board.grid[row][col].Free_Cell();
                    if (values[row, col] != 0)
                        board.grid[row][col].Set_Tile(new Tile(values[row, col]));
                }
            }
            board.target = new List<Tile> { new Tile(1), new Tile(2), new Tile(3) };
            board.direction_Target = 2;
            board.position_target = 0;
            return board;
        }
        public void Init_Tiles()
        {
            int tiles_size = (size * size) - size - 1;

            this.target = new List<Tile>();
            for (int i = 0; i < size; i++)
            {
                int value = allowed_indexes[random.Next(allowed_indexes.Count)];
                Tile tile = new Tile(value);
                tiles.Add(tile);
                target.Add(tile);
                allowed_indexes.Remove(value);
            }
            for (int i = 0; i < tiles_size; i++)
            {
                int value = allowed_indexes[random.Next(allowed_indexes.Count)];
                tiles.Add(new Tile(value));
                allowed_indexes.Remove(value);
            }
        }

        /// <summary>
        /// Places the tiles on the game board.
        /// </summary>
        public void PlaceTiles()
        {
            int indexTile = 0;
            for (int row = 0; row < size; row++)
            {
                grid.Add(new List<Cell>());

                for (int col = 0; col < size; col++)
                {
                    grid[row].Add(new Cell());
                    if (indexTile < tiles.Count)
                    {
                        grid[row][col].Set_Tile(tiles[indexTile]);
                    }
                    indexTile++;
                }
            }

        }

        /// <summary>
        /// Moves an adjacent tile if possible and returns the direction of the move.
        /// </summary>
        /// <param name="row_Current_Tile">Row of the current tile.</param>
        /// <param name="col_Current_Tile">Column of the current tile.</param>
        /// <returns>1 if moved up, -1 if moved down, 2 if moved right, -2 if moved left, 0 if no move was made.</returns>
        public int MoveAdjacentTile(int row_Current_Tile, int col_Current_Tile)
        {
            int directionOfMovement = 0;
            if (row_Current_Tile >= 0 && row_Current_Tile < size - 1)
                if (move_tile(row_Current_Tile + 1, col_Current_Tile, 1) !=0)
                    directionOfMovement = -1;
            if (row_Current_Tile > 1 && row_Current_Tile < size)
                if (move_tile(row_Current_Tile - 1, col_Current_Tile, 1) != 0)
                    directionOfMovement = 1;
            if (col_Current_Tile >= 0 && col_Current_Tile < size - 1)
                if (move_tile(row_Current_Tile, col_Current_Tile + 1, 2) != 0)
                    directionOfMovement = 2;
            if (col_Current_Tile > 1 && col_Current_Tile < size)
                if (move_tile(row_Current_Tile, col_Current_Tile - 1, 2) != 0)
                    directionOfMovement = -2;

            return directionOfMovement;


            return 0;
        }


        /// <summary>
        /// Moves a tile in a specified direction and returns the direction of the move.
        /// </summary>
        /// <param name="row">Row of the tile.</param>
        /// <param name="col">Column of the tile.</param>
        /// <param name="direction">Direction of the move (1 for vertical, 2 for horizontal).</param>
        /// <returns> 1 if moved up, -1 if moved down, 2 if moved right, -2 if moved left, 0 if no move was made.</returns>
        public int move_tile(int row, int col, int direction = 0)
        {
            int moveDirection = 0; // 0 means no move was made

            if (col >= 0 && direction != 1) // Horizontal move
            {
                if (col < size - 1 && !grid[row][col + 1].Is_occupied()) // Move to the right
                {
                    grid[row][col + 1].Set_Tile(grid[row][col].Get_tile());
                    grid[row][col].Free_Cell();
                    moveDirection = 2; // Right
                }
                else if (col > 0 && !grid[row][col - 1].Is_occupied()) // Move to the left
                {
                    grid[row][col - 1].Set_Tile(grid[row][col].Get_tile());
                    grid[row][col].Free_Cell();
                    moveDirection = -2; // Left
                }
            }

            if (row >= 0 && direction != 2) // Vertical move
            {
                if (row < size - 1 && !grid[row + 1][col].Is_occupied()) // Move down
                {
                    grid[row + 1][col].Set_Tile(grid[row][col].Get_tile());
                    grid[row][col].Free_Cell();
                    moveDirection = -1; // Down
                }
                else if (row > 0 && !grid[row - 1][col].Is_occupied()) // Move up
                {
                    grid[row - 1][col].Set_Tile(grid[row][col].Get_tile());
                    grid[row][col].Free_Cell();
                    moveDirection = 1; // Up
                }
            }

            return moveDirection;
        }


        /// <summary>
        /// Gets the grid of the board.
        /// </summary>
        public List<List<Cell>> Get_Board_Grid() => grid;

        /// <summary>
        /// Gets the target values.
        /// </summary>
        public List<Tile> Get_Target() => target;

        /// <summary>
        /// Gets the size of the grid.
        /// </summary>
        public int Get_GridSize() => grid.Count;

        /// <summary>
        /// Gets the target direction.
        /// </summary>
        public int Get_Target_direction() => direction_Target;

        /// <summary>
        /// Gets the target position.
        /// </summary>
        public int Get_Target_position() => position_target;
    }
}