using System;
using System.Drawing;
using System.Windows.Forms;


namespace Taquin
{
    /// <summary>
    /// Represents the scene displayed when the player wins the game.
    /// </summary>
    internal class WinScene : TableLayoutPanel
    {
        private MainScene window;
        private Label message_label;
        private Button goBack_button;
        private Button continue_button;
        private Button next_level_button;
        private Game game;

        /// <summary>
        /// Initializes a new instance of the <see cref="WinScene"/> class.
        /// </summary>
        /// <param name="window">The main window of the game.</param>
        /// <param name="game">The current game.</param>
        public WinScene(MainScene window, Game game)
        {
            this.game = game;
            this.window = window;
            this.AutoSize = true;
            this.BackColor = Color.LightGreen;
            this.Padding = new Padding(20);

            message_label = new Label
            {
                Text = "Félicitations ! Vous avez gagné ce tour ! 🎉\n\n" +
                        "5 points seront ajoutés à votre score.",

                ForeColor = Color.Black,
                AutoSize = true,
                Location = new Point(100, 100),
                Font = new Font("Arial", 12, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter
            };

            continue_button = new Button()
            {
                Size = new Size(100, 30),
                AutoSize = true,
                Margin = new Padding(5),
                BackColor = SystemColors.Highlight,
                Font = new Font("Segoe Script", 20F, FontStyle.Bold),
                ForeColor = SystemColors.ActiveCaptionText,
                Text = "Continue",
            };
            continue_button.Click += new EventHandler(Continue_button_Click);


            next_level_button = new Button()
            {
                Size = new Size(100, 30),
                AutoSize = true,
                Margin = new Padding(5),
                BackColor = SystemColors.Highlight,
                Font = new Font("Segoe Script", 20F, FontStyle.Bold),
                ForeColor = SystemColors.ActiveCaptionText,
                Text = "Niveau Suivant"
            };

            next_level_button.Click += new EventHandler(Next_level_button_Click);

            goBack_button = new Button()
            {
                Size = new Size(50, 30),
                AutoSize = true,
                Margin = new Padding(5),
                BackColor = SystemColors.Highlight,
                Font = new Font("Segoe Script", 20F, FontStyle.Bold),
                ForeColor = SystemColors.ActiveCaptionText,
                Text = "Back to menu",
            };

            goBack_button.Click += new EventHandler(goBack_button_Click);

            int current_score = game.Get_Player().Get_points();
            if (game.Get_current_level().Confirm_score_success(current_score))
            {
                message_label.Text = "Félicitations !\n\n" +
                                    "Vous avez atteint le score requis pour terminer ce niveau ! 🎉\n\n" +
                                    "Continuez comme ça ! " +
                                    "Niveau terminé " +
                                    "🎉🎉🎉🎉🎉🎉";

                this.Controls.Add(message_label, 0, 0);
                this.Controls.Add(next_level_button, 0, 1);
                this.Controls.Add(goBack_button, 0, 2);
                this.Left = 90;
                this.Top = 120;
            }
            else
            {
                this.Controls.Add(message_label, 0, 0);
                this.Controls.Add(continue_button, 0, 1);
                this.Controls.Add(goBack_button, 0, 2);

                this.Left = 150;
                this.Top = 120;
            }

            this.SetColumnSpan(message_label, 2);
        }

        private void Next_level_button_Click(object sender, EventArgs e)
        {
            Level currentLevel = game.Get_current_level();
            game.Set_current_level(currentLevel.Get_stronger_level(1));
            int board_size = game.Get_current_board().Get_GridSize();
            game.Get_current_level().Add_new_board(new Board(board_size));
            this.window.Redraw_Game_Scene();
        }

        /// <summary>
        /// Handles the click event on the back button.
        /// </summary>
        /// <param name="sender">The object that triggered the event.</param>
        /// <param name="e">The event data.</param>
        private void goBack_button_Click(object sender, EventArgs e)
        {
            this.window.Controls.Clear();
            FinalWinScene finalScene = new FinalWinScene(this.window, game);
            this.window.Controls.Add(finalScene);
        }
        private void Continue_button_Click(object sender, EventArgs e)
        {
            int board_size = game.Get_current_board().Get_GridSize();
            game.Get_current_level().Add_new_board(new Board(board_size));
            this.window.Redraw_Game_Scene();
        }
    }
}
