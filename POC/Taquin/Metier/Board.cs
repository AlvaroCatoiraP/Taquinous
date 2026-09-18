using System.Collections.Generic;
using System;
using System.Linq;

namespace Taquin.Metier
{
    /// <summary>
    /// Represents the board of the Taquin game.
    /// </summary>
    public class Board
    {
        private List<Tile> tiles;
        private List<List<Cell>> grid;
        private int size;

        private int target;
        private int direction_Target; // 1 for vertical, 2 for horizontal
        private int position_target;
        private Random random;

        /// <summary>
        /// Initializes a new instance of the <see cref="Board"/> class.
        /// </summary>
        /// <param name="size">Size of the board.</param>
        public Board(int size)
        {
            this.size = size;
            tiles = new List<Tile>();
            grid = new List<List<Cell>>();
            this.random = new Random();
            this.target = random.Next(1, size + 2);

            Init_Tiles();
            this.tiles = tiles.OrderBy(t => random.Next()).ToList();
            PlaceTiles();

            this.direction_Target = random.Next(1, 3);
            this.position_target = random.Next(0, size);
        }

        /// <summary>
        /// Initializes the tiles of the board.
        /// </summary>
        public void Init_Tiles()
        {
            int tiles_size = (size * size) - size - 1;

            for (int i = 0; i < size; i++)
            {
                tiles.Add(new Tile(target));
            }
            for (int i = 0; i < tiles_size; i++)
            {
                tiles.Add(new Tile(random.Next(target + 1, 25)));
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
        /// Moves an adjacent tile if possible.
        /// </summary>
        /// <param name="row_Current_Tile">Row of the current tile.</param>
        /// <param name="col_Current_Tile">Column of the current tile.</param>
        /// <returns>True if a move has been made, otherwise false.</returns>
        public bool MoveAdjacentTile(int row_Current_Tile, int col_Current_Tile)
        {
            if (row_Current_Tile >= 0 && row_Current_Tile < size - 1)
                if (move_tile(row_Current_Tile + 1, col_Current_Tile, 1))
                    return true;
            if (row_Current_Tile > 1 && row_Current_Tile < size)
                if (move_tile(row_Current_Tile - 1, col_Current_Tile, 1))
                    return true;
            if (col_Current_Tile >= 0 && col_Current_Tile < size - 1)
                if (move_tile(row_Current_Tile, col_Current_Tile + 1, 2))
                    return true;
            if (col_Current_Tile > 1 && col_Current_Tile < size)
                if (move_tile(row_Current_Tile, col_Current_Tile - 1, 2))
                    return true;

            return false;
        }

        /// <summary>
        /// Moves a tile in a specified direction.
        /// </summary>
        /// <param name="row">Row of the tile.</param>
        /// <param name="col">Column of the tile.</param>
        /// <param name="direction">Direction of the move (1 for vertical, 2 for horizontal).</param>
        /// <returns>True if a move has been made, otherwise false.</returns>
        public bool move_tile(int row, int col, int direction = 0)
        {
            bool isMoved = false;
            if (col >= 0 && direction != 1)
            {
                if (col < size - 1 && !grid[row][col + 1].IsOccupied())
                {
                    grid[row][col + 1].Set_Tile(grid[row][col].Get_tile());
                    grid[row][col].Free_Cell();
                    isMoved = true;
                }
                if (col > 0 && col < size && !grid[row][col - 1].IsOccupied())
                {
                    grid[row][col - 1].Set_Tile(grid[row][col].Get_tile());
                    grid[row][col].Free_Cell();
                    isMoved = true;
                }
            }
            if (row >= 0 && direction != 2)
            {
                if (row < size - 1 && !grid[row + 1][col].IsOccupied())
                {
                    grid[row + 1][col].Set_Tile(grid[row][col].Get_tile());
                    grid[row][col].Free_Cell();
                    isMoved = true;
                }
                if (row > 0 && row < size && !grid[row - 1][col].IsOccupied())
                {
                    grid[row - 1][col].Set_Tile(grid[row][col].Get_tile());
                    grid[row][col].Free_Cell();
                    isMoved = true;
                }
            }
            return isMoved;
        }

        /// <summary>
        /// Gets the grid of the board.
        /// </summary>
        public List<List<Cell>> Get_Board_Grid() => grid;

        /// <summary>
        /// Gets the target value.
        /// </summary>
        public int Get_Target() => target;

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