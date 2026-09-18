using System;
using System.Drawing;
using System.Windows.Forms;
using Taquin.Screens;

namespace Taquin
{
    /// <summary>
    /// Represents the game scene in the Taquin game.
    /// </summary>
    internal class GameScene : TableLayoutPanel
    {
        private MainScene window;
        private Game game;
        private GameGrid game_grid;
        private ObjectiveGrid objectiveGrid;

        private Board currentBoard;
        private int total_time;
        private TimerGame timer;
        private RobotConsole robotConsole;
        public bool IsChallenge { get; private set; }
        public bool ChallengeFinished { get; private set; }
        private int challengeMoves;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameScene"/> class.
        /// </summary>
        /// <param name="window">The main window.</param>
        /// <param name="game">The current game.</param>
        public GameScene(MainScene window, Game game, bool challenge = false)
        {
            IsChallenge = challenge;
            if (challenge)
            {
                game = new Game("Défi");
                game.Get_current_level().PrepareChallenge();
            }
            this.game = game;
            this.window = window;
            BackColor = Color.FloralWhite;
            ColumnCount = 4;
            RowCount = 4;
            AutoSize = true;
            Anchor = AnchorStyles.Top;
            Padding = new Padding(10);


            this.currentBoard = this.game.Get_current_level().Get_current_board();

            InitGameScene();

            window.AutoScroll = true;
            this.Left = 0;
            this.Top = Math.Max(0, (window.ClientSize.Height - this.PreferredSize.Height) / 2);
            timer = new TimerGame(total_time, window);
            this.Controls.Add(timer, 2, 1);
            if (IsChallenge)
            {
                timer.Visible = false;
            }
            else
            {
                timer.Start();
            }
        }

        /// <summary>
        /// Initializes the game scene by adding the necessary components.
        /// </summary>
        public void InitGameScene()
        {
            Control courseBanner = MainScene.CreateCourseBanner();
            this.Controls.Add(courseBanner, 0, 0);
            this.SetColumnSpan(courseBanner, 3);
            this.game_grid = new GameGrid(game, window, this);

            this.objectiveGrid = new ObjectiveGrid(game, window);
            Label scoreLabel = new Label
            {
                Text = "Ton score: " + game.Get_Player().Get_points() + " / " + game.Get_current_level().Get_target_score().ToString(),
                AutoSize = true,
                Font = new Font("Arial", 12, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Margin = new Padding(30, 0, 0, 0),
            };

            Panel level_instruction_container = new Panel
            {
                Padding = new Padding(10),
                Dock = DockStyle.Fill,
                AutoSize = true,
                Margin = new Padding(10, 0, 0, 0),
            };
            int current_Target_Directon = game.Get_current_level().Get_current_board().Get_Target_direction();
            Label level = new Label
            {
                AutoSize = true,
                Font = new Font("Arial", 11, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
            };
            if (!game.Get_current_level().Get_specificOrder())
            {
                if (current_Target_Directon == 1)
                {
                    level.Text = "Niveau: " + game.Get_current_level().Get_id() + "\n" +
                        "– Respectez l'ordre. \n– Colonne au choix.";
                }
                else if (current_Target_Directon == 2)
                {
                    level.Text = "Niveau: " + game.Get_current_level().Get_id() + "\n" +
                        "– Respectez l'ordre. \n– Ligne au choix.";
                }
            }
            else
            {
                if (current_Target_Directon == 1)
                {
                    level.Text = "Niveau: " + game.Get_current_level().Get_id() + "\n" +
                        "Ordre et colonne \ndoivent correspondre.";
                }
                else if (current_Target_Directon == 2)
                {
                    level.Text = "Niveau: " + game.Get_current_level().Get_id() + "\n" +
                        "Ordre et ligne \ndoivent correspondre.";
                }
            }
            if (IsChallenge)
                level.Text = "Défi fixe : complète la première ligne\ndans l'ordre indiqué.\nObjectif : 5 mouvements, sans chrono.";
            level_instruction_container.Controls.Add(level);

            total_time = game.Get_current_level().Get_secs();

            Button skip_button = new Button()
            {

                Dock = DockStyle.Fill,
                BackColor = Color.LightGreen,
                Font = new Font("Segoe Script", 20F, FontStyle.Bold),
                Text = "Passer",
                Size = new Size(50, 60),
                ForeColor = Color.Black,
            };
            skip_button.Visible = !IsChallenge;
            skip_button.Click += new System.EventHandler(skip_Click);

            Panel numberedGrid = CreateNumberedGrid();
            this.Controls.Add(numberedGrid, 1, 1);
            scoreLabel.Visible = !IsChallenge;
            this.Controls.Add(scoreLabel, 2, 2);
            this.Controls.Add(objectiveGrid.InitLifes(), 2, 3);           
            this.Controls.Add(objectiveGrid, 2,4);
            this.Controls.Add(level_instruction_container, 2, 5);
            this.Controls.Add(skip_button, 2, 6);
            this.SetRowSpan(numberedGrid, 5);
            robotConsole = new RobotConsole(game_grid, this);
            this.Controls.Add(robotConsole, 0, 1);
            this.SetRowSpan(robotConsole, 5);

        }

        private Panel CreateNumberedGrid()
        {
            const int headerSize = 28;
            var container = new Panel
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Margin = game_grid.Margin,
                BackColor = Color.FloralWhite
            };
            game_grid.Margin = Padding.Empty;
            game_grid.Location = new Point(headerSize, headerSize);
            container.Controls.Add(game_grid);
            // Les repères restent à l'extérieur pour conserver les coordonnées du jeu.
            container.Paint += (sender, e) =>
            {
                for (int index = 0; index < currentBoard.Get_GridSize(); index++)
                {
                    Control columnCell = game_grid.GetControlFromPosition(index, 0);
                    Control rowCell = game_grid.GetControlFromPosition(0, index);
                    string number = (index + 1).ToString();
                    var flags = TextFormatFlags.HorizontalCenter |
                                TextFormatFlags.VerticalCenter | TextFormatFlags.NoPadding;
                    if (columnCell != null)
                        TextRenderer.DrawText(e.Graphics, number, Font,
                            new Rectangle(game_grid.Left + columnCell.Left, 0,
                                columnCell.Width, headerSize), Color.DarkSlateGray, flags);
                    if (rowCell != null)
                        TextRenderer.DrawText(e.Graphics, number, Font,
                            new Rectangle(0, game_grid.Top + rowCell.Top,
                                headerSize, rowCell.Height), Color.DarkSlateGray, flags);
                }
            };
            game_grid.Layout += (sender, e) => container.Invalidate();
            return container;
        }

        public void StartChallenge()
        {
            stopTimer();
            window.Controls.Clear();
            window.Controls.Add(new GameScene(window, game, true));
        }

        public void ReturnToGame()
        {
            stopTimer();
            window.Controls.Clear();
            window.Controls.Add(new GameScene(window, window.Get_game()));
        }

        public void CheckChallengeMove()
        {
            challengeMoves++;
            if (game.Is_correct_target())
            {
                ChallengeFinished = true;
                game_grid.Enabled = false;
                robotConsole.ShowChallengeSuccess(challengeMoves);
            }
        }
        private void skip_Click(object sender, EventArgs e)
        {
            stopTimer();
            this.window.Controls.Clear();
            SkipScene skipScene = new SkipScene(window);
            this.window.Controls.Add(skipScene);
        }

        /// <summary>
        /// Stops the timer of the game scene.
        /// </summary>
        public void stopTimer()
        {
            if (this.timer != null) this.timer.Stop();
        }
    }
}

