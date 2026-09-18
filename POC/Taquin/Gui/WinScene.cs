using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Taquin
{
    /// <summary>
    /// Represents the scene displayed when the player wins the game.
    /// </summary>
    internal class WinScene : TableLayoutPanel
    {
        private MainScene window;
        private Button goBack;
        private Label messageLabel;
        private Button goBack_button;
        private Button continueButton;
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

            messageLabel = new Label
            {
                Text = "Congratulations! You have won! 🎉\n\n" +
                "5 points will be added to your score.",
                ForeColor = Color.Black,
                AutoSize = true,
                Location = new Point(100, 100),
                Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold),
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            };

            continueButton = new Button()
            {
                Size = new Size(100, 30),
                AutoSize = true,
                Margin = new Padding(5),
                BackColor = SystemColors.Highlight,
                Font = new Font("Segoe Script", 20F, FontStyle.Bold),
                ForeColor = SystemColors.ActiveCaptionText,
                Text = "Continue",
            };

            continueButton.Click += new EventHandler(continueButton_Click);

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

            int current_score = game.Get_Player().GetScore();
            if (game.Get_current_level().ConfirmScoreSuccess(current_score))
            {
                messageLabel.Text = "Congratulations! \n\n" +
                                    "You have reached the required score to complete this level! 🎉\n\n" +
                                    "Keep it up! " +
                                    "Level Finished" +
                                    "🎉🎉🎉🎉🎉🎉";

                this.Controls.Add(messageLabel, 0, 0);
                this.Controls.Add(goBack_button, 0, 1);
                this.Left = 90;
                this.Top = 120;
            }
            else
            {
                this.Controls.Add(messageLabel, 0, 0);
                this.Controls.Add(continueButton, 0, 1);
                this.Controls.Add(goBack_button, 0, 2);

                this.Left = 150;
                this.Top = 120;
            }

            this.SetColumnSpan(messageLabel, 2);
        }

        /// <summary>
        /// Handles the click event on the back button.
        /// </summary>
        /// <param name="sender">The object that triggered the event.</param>
        /// <param name="e">The event data.</param>
        private void goBack_button_Click(object sender, EventArgs e)
        {
            this.window.Controls.Clear();
            this.window.resetGame();
            this.window.Init_Form();
        }

        /// <summary>
        /// Handles the click event on the continue button.
        /// </summary>
        /// <param name="sender">The object that triggered the event.</param>
        /// <param name="e">The event data.</param>
        private void continueButton_Click(object sender, EventArgs e)
        {
            Game game = window.Get_game();
            int board_size = game.Get_current_round().Get_Board().Get_GridSize();
            game.Get_current_level().Add_new_round(new Round(board_size));
            this.window.Redraw_Game_Scene();
        }
    }
}
