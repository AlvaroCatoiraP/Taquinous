using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Taquin.Gui;

namespace Taquin
{
    /// <summary>
    /// Represents the main scene of the Taquin game.
    /// </summary>
    internal class MainScene : Form
    {
        private Button goback;
        private Button play;
        private Button showRools;
        private Button showTop10;
        private Panel main_panel;
        private Game game;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainScene"/> class.
        /// </summary>
        public MainScene()
        {
            string player = "Alvaro";
            game = new Game(player);
            this.BackgroundImage = Taquin.Properties.Resources.fond_plat;
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.ResumeLayout(false);
            this.PerformLayout();
            this.ClientSize = new Size(700, 500);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Taquinou";
            Init_Form();
        }

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Cleans up the resources used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Initializes the main form with the necessary controls.
        /// </summary>
        public void Init_Form()
        {
            this.play = new Button();
            this.showRools = new Button();
            this.showTop10 = new Button();
            this.goback = new Button();

            this.main_panel = new TableLayoutPanel();
            this.main_panel.SuspendLayout();

            // Play Button
            this.play.Dock = DockStyle.Fill;
            this.play.BackColor = SystemColors.Highlight;
            this.play.Font = new Font("Segoe Script", 20F, FontStyle.Bold);
            this.play.Text = "Jouer";
            this.play.Size = new Size(100, 70);
            this.play.ForeColor = Color.White;
            this.play.Click += new System.EventHandler(this.play_click);

            // Rules Button
            this.showRools.Dock = DockStyle.Fill;
            this.showRools.BackColor = SystemColors.Highlight;
            this.showRools.Font = new Font("Segoe Script", 20F, FontStyle.Bold);
            this.showRools.ForeColor = SystemColors.ActiveCaptionText;
            this.showRools.Text = "Règles du jeu.";
            this.showRools.Size = new Size(100, 50);
            this.showRools.ForeColor = Color.White;
            this.showRools.Click += new System.EventHandler(this.ShowRools_Click);

            // Top 10 Button
            this.showTop10.Dock = DockStyle.Fill;
            this.showTop10.BackColor = SystemColors.Highlight;
            this.showTop10.Font = new Font("Segoe Script", 20F, FontStyle.Bold);
            this.showTop10.ForeColor = SystemColors.ActiveCaptionText;
            this.showTop10.Text = "Top 10";
            this.showTop10.Size = new Size(100, 50);
            this.showTop10.ForeColor = Color.White;
            this.showTop10.Click += new System.EventHandler(this.ShowTop10_Click);

            // Back to Menu Button
            this.goback.BackColor = SystemColors.Highlight;
            this.goback.AutoSize = true;
            this.goback.Font = new Font("Segoe Script", 20F, FontStyle.Bold);
            this.goback.ForeColor = SystemColors.ActiveCaptionText;
            this.goback.Text = "Retourner au menu";
            this.goback.Margin = new Padding(left: 150, top: 10, right: 0, bottom: 5);
            this.goback.Click += new System.EventHandler(this.goBack_Click);

            // Main Panel
            this.main_panel.Size = new Size(400, 300);
            this.main_panel.Controls.Add(this.play);
            this.main_panel.Controls.Add(this.showTop10);
            this.main_panel.Controls.Add(this.showRools);

            this.Controls.Add(this.main_panel);

            centerPanel();
        }

        /// <summary>
        /// Centers the main panel in the window.
        /// </summary>
        private void centerPanel()
        {
            main_panel.Left = (this.ClientSize.Width - main_panel.Width) / 2;
            main_panel.Top = (this.ClientSize.Height - main_panel.Height) / 2;
            this.main_panel.ResumeLayout(false);
            this.main_panel.PerformLayout();
        }

        /// <summary>
        /// Handles the click event on the Back to Menu button.
        /// </summary>
        public void goBack_Click(object sender, EventArgs e)
        {
            this.Controls.Clear();
            Init_Form();
        }

        /// <summary>
        /// Handles the click event on the Play button.
        /// </summary>
        private void play_click(object sender, EventArgs e)
        {
            this.Controls.Clear();
            this.game.Get_Player().reset_lifes();
            GameScene gameScene = new GameScene(this, game);
            this.Controls.Add(gameScene);
        }

        /// <summary>
        /// Handles the click event on the Game Rules button.
        /// </summary>
        private void ShowRools_Click(object sender, EventArgs e)
        {
            main_panel.Controls.Clear();
            RoolsLabel rools = new RoolsLabel();
            main_panel.Controls.Add(rools);
            rools.Width = main_panel.Width;
            rools.MaximumSize = new Size(main_panel.Width, 0);
            rools.Margin = new Padding(left: 100, top: 10, right: 0, bottom: 0);
            main_panel.AutoScroll = true;
            main_panel.Dock = DockStyle.Fill;
            main_panel.Controls.Add(goback);
        }

        /// <summary>
        /// Handles the click event on the Top 10 button.
        /// </summary>
        private void ShowTop10_Click(object sender, EventArgs e)
        {
            main_panel.Controls.Clear();
            Label playLabel = new Label()
            {
                Text = "Bientôt disponible",
                ForeColor = Color.Black,
                AutoSize = true,
                Location = new Point(100, 100),
                Font = new Font("Arial", 12, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Padding = new Padding(60)
            };
            this.goback.Margin = new Padding(left: 60, top: 10, right: 0, bottom: 5);
            main_panel.Controls.Add(playLabel);
            main_panel.Controls.Add(goback);
        }

        /// <summary>
        /// Redraws the game scene.
        /// </summary>
        public void Redraw_Game_Scene()
        {
            this.Controls.Clear();
            this.Controls.Add(new GameScene(this, this.game));
        }

        /// <summary>
        /// Gets the current game.
        /// </summary>
        /// <returns>The current game.</returns>
        public Game Get_game()
        {
            return this.game;
        }

        /// <summary>
        /// Resets the game.
        /// </summary>
        public void resetGame()
        {
            this.game.reset();
        }

    }
}