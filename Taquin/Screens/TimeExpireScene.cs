using System;
using System.Drawing;
using System.Windows.Forms;

namespace Taquin
{
    /// <summary>
    /// Represents the scene displayed when time runs out in the game.
    /// </summary>
    internal class TimeExpireScene : TableLayoutPanel
    {
        private Label messageLabel;
        private Button yes_button;
        private Button no_button;
        private MainScene window;
        private Game game;

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeExpireScene"/> class.
        /// </summary>
        /// <param name="window">The main window of the game.</param>
        public TimeExpireScene(MainScene window)
        {
            this.window = window;
            this.AutoSize = true;
            this.game = window.Get_game();
            this.BackColor = Color.LightCoral;
            this.Padding = new Padding(50);

            messageLabel = new Label
            {
                Text = "Le temps est écoulé. Voulez-vous réessayer le niveau ?",
                ForeColor = Color.Black,
                AutoSize = true,
                Location = new Point(100, 100),
                Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold),
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            };

            yes_button = new Button()
            {
                Size = new Size(100, 30),
                AutoSize = true,
                Margin = new Padding(5),
            };
            yes_button.Click += new EventHandler(Button_yes_Click);

            if (game.Get_Player().Get_Lifes() > 1)
            {
                yes_button.Text = "Oui";
            }
            else
            {
                yes_button.Enabled = false;
                yes_button.Text = "Vous n'avez plus de vies.";
            }

            no_button = new Button
            {
                Text = "No",
                Size = new Size(100, 30),
                Margin = new Padding(5),
            };

            no_button.Click += new EventHandler(Button_no_Click);

            this.Controls.Add(messageLabel, 0, 0);
            this.Controls.Add(yes_button, 0, 1);
            this.Controls.Add(no_button, 1, 1);
            this.SetColumnSpan(messageLabel, 2);

            this.Left = 110;
            this.Top = 110;
        }

        /// <summary>
        /// Handles the click event on the "No" button.
        /// </summary>
        /// <param name="sender">The object that triggered the event.</param>
        /// <param name="e">The event data.</param>
        private void Button_no_Click(object sender, EventArgs e)
        {
            this.window.Controls.Clear();
            FinalWinScene finalScene = new FinalWinScene(this.window, game);
            this.window.Controls.Add(finalScene);
        }

        /// <summary>
        /// Handles the click event on the "Yes" button.
        /// </summary>
        /// <param name="sender">The object that triggered the event.</param>
        /// <param name="e">The event data.</param>
        private void Button_yes_Click(object sender, EventArgs e)
        {
            
            game.Get_Player().Decrement_lifes();
            int board_size = this.game.Get_current_board().Get_GridSize();
            this.game.Get_current_level().Add_new_board(new Board(board_size));
            this.window.Redraw_Game_Scene();
            
        }
    }
}
