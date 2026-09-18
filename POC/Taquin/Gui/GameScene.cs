using System.Drawing;
using System;
using System.Windows.Forms;
using Taquin.Metier;

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

        /// <summary>
        /// Initializes a new instance of the <see cref="GameScene"/> class.
        /// </summary>
        /// <param name="window">The main window.</param>
        /// <param name="game">The current game.</param>
        public GameScene(MainScene window, Game game)
        {
            this.game = game;
            this.window = window;
            BackColor = Color.FloralWhite;
            ColumnCount = 4;
            RowCount = 4;
            AutoSize = true;
            Anchor = AnchorStyles.Top;
            Padding = new Padding(5);

            this.currentBoard = this.game.Get_current_level().Get_current_round().Get_Board();

            InitGameScene();

            Panel back_panel = new Panel()
            {
                Dock = DockStyle.Fill,
            };
            this.Left = (window.ClientSize.Width - (objectiveGrid.Width + game_grid.Width)) / 2;
            this.Top = (window.ClientSize.Height - game_grid.Height) / 2;
            timer = new TimerGame(total_time, window);
            this.Controls.Add(timer, 2, 1);
            timer.Start();
        }

        /// <summary>
        /// Initializes the game scene by adding the necessary components.
        /// </summary>
        public void InitGameScene()
        {
            this.game_grid = new GameGrid(game, window, this);

            this.objectiveGrid = new ObjectiveGrid(game, window);

            Label scoreLabel = new Label
            {
                Text = "Your score: " + game.Get_Player().GetScore() + " / " + game.Get_current_level().GetTargetScore().ToString(),
                AutoSize = true,
                Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold),
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                Margin = new Padding(30, 0, 0, 0),
            };

            total_time = game.Get_current_level().Get_Secs();
            this.Controls.Add(game_grid, 1, 1);
            this.Controls.Add(scoreLabel, 2, 2);
            this.Controls.Add(objectiveGrid.InitLifes(), 2, 3);
            this.Controls.Add(objectiveGrid, 2, 4);

            this.SetRowSpan(game_grid, 4);
        }

        /// <summary>
        /// Stops the timer of the game scene.
        /// </summary>
        public void stopTimer()
        {
            this.timer.Stop();
        }
    }
}
