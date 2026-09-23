using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Taquin.Metier;

namespace Taquin
{
    /// <summary>
    /// Represents the game grid in the Taquin game.
    /// </summary>
    internal class GameGrid : TableLayoutPanel
    {
        private Game game;
        private Button[] grid_Buttons;
        private Board currentBoard;
        private int target;
        private GameScene parent_scene;
        MainScene window;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameGrid"/> class.
        /// </summary>
        /// <param name="game">The current game.</param>
        /// <param name="window">The main window.</param>
        /// <param name="parent_scene">The parent scene.</param>
        public GameGrid(Game game, MainScene window, GameScene parent_scene)
        {
            this.game = game;
            this.window = window;
            this.parent_scene = parent_scene;

            currentBoard = this.game.Get_current_round().Get_Board();
            this.target = currentBoard.Get_Target();
            int size_Grid = currentBoard.Get_GridSize();

            AutoSize = true;
            Location = new Point(200, 200);
            BorderStyle = BorderStyle.Fixed3D;

            this.grid_Buttons = new Button[size_Grid];
            List<List<Cell>> current_grid_board = currentBoard.Get_Board_Grid();

            for (int row = 0; row < size_Grid; row++)
            {
                for (int col = 0; col < size_Grid; col++)
                {
                    Button button_Tile = new Button()
                    {
                        Size = new Size(100, 100),
                        Font = new Font("Segoe Script", 30, FontStyle.Bold),
                    };

                    if (current_grid_board[row][col].IsOccupied())
                    {
                        button_Tile.Text = current_grid_board[row][col].ToString();
                        if (button_Tile.Text == target.ToString())
                        {
                            button_Tile.BackColor = Color.LightPink;
                        }
                        else
                        {
                            button_Tile.BackColor = Color.LightBlue;
                        }
                    }
                    else
                    {
                        button_Tile.Enabled = false;
                        button_Tile.BackColor = Color.LightGray;
                    }
                    button_Tile.Click += new EventHandler(this.Button_Tile_Click);
                    this.Controls.Add(button_Tile, col, row);
                }
            }

            this.game = game;
        }

        /// <summary>
        /// Updates the game grid to reflect the current state of the board.
        /// </summary>
        private void update_grid()
        {
            List<List<Cell>> current_grid_board = currentBoard.Get_Board_Grid();
            int grid_size = currentBoard.Get_GridSize();

            for (int row = 0; row < grid_size; row++)
            {
                for (int col = 0; col < grid_size; col++)
                {
                    Button button_Tile = this.GetControlFromPosition(col, row) as Button;

                    if (current_grid_board[row][col].IsOccupied())
                    {
                        button_Tile.Text = current_grid_board[row][col].ToString();
                        button_Tile.Enabled = true;
                        if (button_Tile.Text == target.ToString())
                        {
                            button_Tile.BackColor = Color.LightPink;
                        }
                        else
                        {
                            button_Tile.BackColor = Color.LightBlue;
                        }
                    }
                    else
                    {
                        button_Tile.BackColor = Color.LightGray;
                        button_Tile.Enabled = false;
                        button_Tile.Text = "";
                    }
                }
            }
            this.Focus();
        }

        /// <summary>
        /// Event triggered when a button on the grid is clicked.
        /// </summary>
        /// <param name="sender">The object that triggered the event.</param>
        /// <param name="e">The event data.</param>
        private void Button_Tile_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;

            if (clickedButton != null)
            {
                int column = this.GetPositionFromControl(clickedButton).Row;
                int row = this.GetPositionFromControl(clickedButton).Column;
                if (!currentBoard.move_tile(column, row))
                {
                    if (currentBoard.MoveAdjacentTile(column, row))
                    {
                        currentBoard.move_tile(column, row);
                    }
                }
                if (!this.game.IsCorrect_Target())
                {
                    update_grid();
                }
                else
                {
                    this.parent_scene.stopTimer();
                    this.window.Controls.Clear();
                    this.game.Get_Player().Increment_Score();
                    WinScene finalScene = new WinScene(this.window, game);
                    this.window.Controls.Add(finalScene);
                }
            }
        }
    }
}
