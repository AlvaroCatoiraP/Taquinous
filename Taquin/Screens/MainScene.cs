using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

namespace Taquin
{
    /// <summary>
    /// Represents the main scene of the Taquin game.
    /// </summary>
        public class MainScene : Form
    {
        private Button goback;
        private Button play;
        private Button showRools;
        private Button showTop10;
        private Panel main_panel;
        private Game game;
        private Button quitButton;
        internal bool DeveloperPanelVisible { get; set; } = true;
        private Control courseBanner;
        private bool centeringLayout;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainScene"/> class.
        /// </summary>
        public MainScene()
        {
            this.DoubleBuffered = true;
            this.AutoScroll = true;

            string player = "Alvaro";
            game = new Game(player);
            Assembly myAssembly = Assembly.GetExecutingAssembly();
            Stream myStream = myAssembly.GetManifestResourceStream("Taquin.Resources.fond.jpg");
            Bitmap bmp = new Bitmap(myStream);
            this.BackgroundImage = bmp;
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.ClientSize = new Size(1000, 800);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Taquinou";
            Init_Form();
        }

        internal static Control CreateCourseBanner()
        {
            var banner = new TableLayoutPanel
            {
                AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink,
                ColumnCount = 2, RowCount = 2, BackColor = Color.White,
                Padding = new Padding(12), Margin = new Padding(0, 0, 0, 12)
            };
            var logo = new PictureBox
            {
                Size = new Size(220, 55), SizeMode = PictureBoxSizeMode.Zoom,
                Margin = new Padding(0, 0, 20, 0),
                AccessibleName = "Haute École Robert Schuman"
            };
            using (Stream stream = Assembly.GetExecutingAssembly()
                .GetManifestResourceStream("Taquin.Resources.hers-logo.png"))
            {
                if (stream == null) throw new FileNotFoundException("Logo HERS introuvable.");
                using (Image source = Image.FromStream(stream)) logo.Image = new Bitmap(source);
            }
            logo.Disposed += (sender, e) => logo.Image.Dispose();
            var caption = new Label
            {
                AutoSize = true, Text = "Projet réalisé dans le cadre du cours",
                Font = new Font("Segoe UI", 10), ForeColor = Color.DimGray,
                Margin = new Padding(0, 2, 0, 4)
            };
            var course = new Label
            {
                AutoSize = true, Text = "2Q2 - Langages et développement 4",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 110, 150), Margin = Padding.Empty
            };
            
            banner.Controls.Add(logo, 0, 0);
            banner.SetRowSpan(logo, 2);
            banner.Controls.Add(caption, 1, 0);
            banner.Controls.Add(course, 1, 1);
            return banner;
        }

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
            courseBanner = CreateCourseBanner();
            courseBanner.Location = new Point(16, 16);
            this.Controls.Add(courseBanner);
            this.play = new Button();
            this.showRools = new Button();
            this.showTop10 = new Button();
            this.goback = new Button();
            this.quitButton = new Button();

            this.main_panel = new TableLayoutPanel();

            // Play Button
            this.play.Dock = DockStyle.Fill;
            this.play.BackColor = Color.Green;
            this.play.Font = new Font("Segoe Script", 20F, FontStyle.Bold);
            this.play.Text = "Jouer";
            this.play.Size = new Size(80, 75);
            this.play.ForeColor = Color.Black;
            this.play.Click += new System.EventHandler(this.play_click);
            // Rules Button
            this.showRools.Dock = DockStyle.Fill;
            this.showRools.BackColor = Color.LightGreen;
            this.showRools.Font = new Font("Segoe Script", 20F, FontStyle.Bold);
            this.showRools.ForeColor = SystemColors.ActiveCaptionText;
            this.showRools.Text = "Règles du jeu.";
            this.showRools.Size = new Size(80, 60);
            this.showRools.ForeColor = Color.Black;
            this.showRools.Click += new System.EventHandler(this.ShowRools_Click);
            // Top 10 Button
            this.showTop10.Dock = DockStyle.Fill;
            this.showTop10.BackColor = Color.LightGreen;
            this.showTop10.Font = new Font("Segoe Script", 20F, FontStyle.Bold);
            this.showTop10.ForeColor = SystemColors.ActiveCaptionText;
            this.showTop10.Text = "Top 10";
            this.showTop10.Size = new Size(80, 60);
            this.showTop10.ForeColor = Color.Black;
            this.showTop10.Click += new System.EventHandler(this.ShowTop10_Click);
            // Back to Menu Button
            this.goback.BackColor = Color.LightGreen;
            this.goback.AutoSize = true;
            this.goback.Font = new Font("Segoe Script", 20F, FontStyle.Bold);
            this.goback.ForeColor = SystemColors.ActiveCaptionText;
            this.goback.Text = "Retourner au menu";
            this.goback.Margin = new Padding(left: 150, top: 10, right: 0, bottom: 5);
            this.goback.Click += new System.EventHandler(this.goBack_Click);
            // Quit Button
            this.quitButton.Dock = DockStyle.Fill;
            this.quitButton.BackColor = Color.LightPink;
            this.quitButton.Font = new Font("Segoe Script", 20F, FontStyle.Bold);
            this.quitButton.ForeColor = SystemColors.ActiveCaptionText;
            this.quitButton.Text = "Quiter";
            this.quitButton.Size = new Size(80, 60);
            this.quitButton.ForeColor = Color.Black;
            this.quitButton.Click += new System.EventHandler(this.ShowTop10_Click);
            this.quitButton.Click += (sender, e) => this.Close();

            // Main Panel
            this.main_panel.Size = new Size(500, 300);
            this.main_panel.Controls.Add(this.play);
            this.main_panel.Controls.Add(this.showTop10);
            this.main_panel.Controls.Add(this.showRools);
            this.main_panel.Controls.Add(this.quitButton);

            this.Controls.Add(this.main_panel);

            centerPanel();
            this.ResumeLayout();
        }

        /// <summary>
        /// Centers the main panel in the window.
        /// </summary>
        private void centerPanel()
        {
            PerformLayout();

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
            RulesLabel rules = new RulesLabel();
            main_panel.Controls.Add(rules);
            main_panel.Controls.Add(goback);
            main_panel.Dock = DockStyle.None;
            main_panel.Size = new Size(650, 700);
            centerPanel();
        }

        /// <summary>
        /// Handles the click event on the Top 10 button.
        /// </summary>
        private void ShowTop10_Click(object sender, EventArgs e)
        {
            this.Controls.Clear();
            Display_top_10 top_10_scene = new Display_top_10(this);
            this.Controls.Add(top_10_scene);
        }

        /// <summary>
        /// Redraws the game scene.
        /// </summary>
        public void Redraw_Game_Scene()
        {
            this.Controls.Clear();
            this.Controls.Add(new GameScene(this, this.game));
        }

        // Recenter after resizing, content changes and navigation; retain scrolling.
        protected override void OnLayout(LayoutEventArgs e)
        {
            base.OnLayout(e);
            if (centeringLayout) return;
            centeringLayout = true;
            try
            {
                int headerHeight = courseBanner != null && Controls.Contains(courseBanner)
                    ? courseBanner.Height + 32 : 0;
                Point scroll = AutoScrollPosition;
                foreach (Control scene in Controls)
                {
                    if (scene.Dock != DockStyle.None) continue;
                    scene.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    int top = scene == courseBanner ? 16 : headerHeight;
                    int x = Math.Max(0, (ClientSize.Width - scene.Width) / 2);
                    int y = scene == courseBanner ? top
                        : top + Math.Max(0, (ClientSize.Height - top - scene.Height) / 2);
                    scene.Location = new Point(x + scroll.X, y + scroll.Y);
                }
            }
            finally { centeringLayout = false; }
        }
        public Game Get_game()
        {
            return this.game;
        }

        public void resetGame()
        {
            this.game.reset();
        }
    }
}
