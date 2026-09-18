using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace Taquin
{
    internal class GameGrid : TableLayoutPanel
    {
        private Game game;
        private Board currentBoard;
        private List<Tile> target;
        private GameScene parent_scene;
        private MainScene window;
        private List<List<Cell>> current_grid_board;
        private TileMovementHandler movementHandler;
        private int selectedRow = -1;
        private int selectedCol = -1;

        public GameGrid(Game game, MainScene window, GameScene parent_scene)
        {
            this.game = game;
            this.window = window;
            this.parent_scene = parent_scene;

            currentBoard = this.game.Get_current_board();
            this.target = currentBoard.Get_Target();
            int size_Grid = currentBoard.Get_GridSize();

            AutoSize = true;
            BorderStyle = BorderStyle.Fixed3D;

            int button_size = 450 / size_Grid;
            current_grid_board = currentBoard.Get_Board_Grid();

            for (int row = 0; row < size_Grid; row++)
            {
                for (int col = 0; col < size_Grid; col++)
                {
                    // Le même bouton peut venir d'une ancienne grille.
                    Cell cell = current_grid_board[row][col];
                    GameGrid oldGrid = cell.Parent as GameGrid;
                    if (oldGrid != null)
                    {
                        cell.Click -= oldGrid.Button_Tile_Click;
                        cell.Click -= oldGrid.Select_Tile_Click;
                    }
                    cell.FlatAppearance.BorderSize = 1;
                    cell.FlatAppearance.BorderColor = SystemColors.ControlDark;
                    cell.FlatStyle = FlatStyle.Standard;
                    cell.Image = null;
                    cell.BackColor = SystemColors.Control;
                    current_grid_board[row][col].Size = new Size(button_size, button_size);

                    if (current_grid_board[row][col].Is_occupied())
                    {
                        current_grid_board[row][col].Enabled = true;
                        Assembly myAssembly = Assembly.GetExecutingAssembly();
                        int tile_symbol_index = current_grid_board[row][col].Get_tile().Get_symbol_index();
                        string image_path = "Taquin.Resources." + tile_symbol_index + ".jpg";
                        Stream myStream = myAssembly.GetManifestResourceStream(image_path);

                        if (myStream == null)
                            throw new FileNotFoundException($"Ressource introuvable : {image_path}");

                        Bitmap bmp = new Bitmap(myStream);
                        Bitmap resized_bmp = new Bitmap(bmp, current_grid_board[row][col].Width, current_grid_board[row][col].Height);

                        current_grid_board[row][col].Image = resized_bmp;
                        current_grid_board[row][col].ImageAlign = ContentAlignment.MiddleCenter;
                    }
                    else
                    {
                        current_grid_board[row][col].Enabled = false;
                        current_grid_board[row][col].BackColor = Color.LightGray;
                    }

                    // Pendant le défi, les élèves jouent avec la console.
                    if (parent_scene.IsChallenge)
                        cell.Click += Select_Tile_Click;
                    else
                        current_grid_board[row][col].Click += new EventHandler(this.Button_Tile_Click);
                    this.Controls.Add(current_grid_board[row][col], col, row);
                }
            }

            movementHandler = new TileMovementHandler(this, currentBoard, current_grid_board);
        }

        // Coordonnées de la console : ligne et colonne commencent à 1.
        public bool SelectRobotTile(int ligne, int colonne)
        {
            int size = currentBoard.Get_GridSize();
            if (parent_scene.ChallengeFinished || ligne < 1 || ligne > size ||
                colonne < 1 || colonne > size ||
                !current_grid_board[ligne - 1][colonne - 1].Is_occupied())
            {
                ClearRobotSelection();
                return false;
            }
            ClearRobotSelection();
            selectedRow = ligne - 1;
            selectedCol = colonne - 1;
            HighlightSelection();
            return true;
        }

        private void ClearRobotSelection()
        {
            if (selectedRow >= 0 && selectedCol >= 0)
            {
                Cell cell = (Cell)GetControlFromPosition(selectedCol, selectedRow);
                cell.FlatAppearance.BorderSize = 1;
                cell.FlatAppearance.BorderColor = SystemColors.ControlDark;
                cell.FlatStyle = FlatStyle.Standard;
            }
            selectedRow = -1;
            selectedCol = -1;
        }

        private void HighlightSelection()
        {
            Cell cell = (Cell)GetControlFromPosition(selectedCol, selectedRow);
            cell.FlatStyle = FlatStyle.Flat;
            cell.FlatAppearance.BorderColor = Color.DodgerBlue;
            cell.FlatAppearance.BorderSize = 3;
        }

        private void Select_Tile_Click(object sender, EventArgs e)
        {
            Control cell = sender as Control;
            if (cell == null || cell.Parent != this) return;
            TableLayoutPanelCellPosition position = GetPositionFromControl(cell);
            SelectRobotTile(position.Row + 1, position.Column + 1);
        }
        private bool TryRobotDestination(string direction, out int row, out int col)
        {
            row = selectedRow;
            col = selectedCol;
            if (row < 0 || col < 0 || !current_grid_board[row][col].Is_occupied())
                return false;
            switch (direction)
            {
                case "haut": row--; break;
                case "bas": row++; break;
                case "gauche": col--; break;
                case "droite": col++; break;
                default: return false;
            }
            int size = currentBoard.Get_GridSize();
            return row >= 0 && row < size && col >= 0 && col < size &&
                !current_grid_board[row][col].Is_occupied();
        }

        public bool CheckRobotCondition(string condition)
        {
            switch (condition)
            {
                case "vide_en_haut": return CanMoveRobot("haut");
                case "vide_en_bas": return CanMoveRobot("bas");
                case "vide_a_gauche": return CanMoveRobot("gauche");
                case "vide_a_droite": return CanMoveRobot("droite");
                case "haut_possible": return CanMoveRobot("haut");
                case "bas_possible": return CanMoveRobot("bas");
                case "gauche_possible": return CanMoveRobot("gauche");
                case "droite_possible": return CanMoveRobot("droite");
                default: return false;
            }
        }

        public bool CanMoveRobot(string direction)
        {
            int row, col;
            return !parent_scene.ChallengeFinished &&
                TryRobotDestination(direction, out row, out col);
        }

        public bool MoveRobot(string direction)
        {
            int row, col;
            if (parent_scene.ChallengeFinished ||
                !TryRobotDestination(direction, out row, out col)) return false;

            Cell origin = current_grid_board[selectedRow][selectedCol];
            Tile movedTile = origin.Get_tile();
            // Conserver le gestionnaire existant pour l'affichage et la victoire.
            Control visualTile = GetControlFromPosition(selectedCol, selectedRow);
            ClearRobotSelection();
            Button_Tile_Click(visualTile, EventArgs.Empty);
            bool moved = !origin.Is_occupied() &&
                current_grid_board[row][col].Is_occupied() &&
                ReferenceEquals(current_grid_board[row][col].Get_tile(), movedTile);
            ClearRobotSelection();
            if (moved)
            {
                selectedRow = row;
                selectedCol = col;
                HighlightSelection();
            }
            return moved;
        }
        private void Button_Tile_Click(object sender, EventArgs e)
        {
            this.SuspendLayout();
            Button clickedButton = sender as Cell;

            if (clickedButton != null)
            {
                movementHandler.HandleTileClick(clickedButton);

                if (parent_scene.IsChallenge)
                {
                    parent_scene.CheckChallengeMove();
                }
                else if (this.game.Is_correct_target())
                {
                    this.parent_scene.stopTimer();
                    this.window.Controls.Clear();
                    this.game.Get_Player().Increment_Score();
                    if (game.Get_current_level().Get_id() < game.Get_nb_levels())
                    {
                        this.window.Controls.Add(new WinScene(this.window, game));


                    }
                    else
                    {
                        this.window.Controls.Add(new FinalWinScene(this.window, game));
                    }
                }
            }

            this.ResumeLayout();
        }
    }
}


