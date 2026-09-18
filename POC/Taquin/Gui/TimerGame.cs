using System;
using System.Drawing;
using System.Windows.Forms;

namespace Taquin
{
    /// <summary>
    /// Represents a game timer for the Taquin game.
    /// </summary>
    internal class TimerGame : TableLayoutPanel
    {
        private Timer timer;
        private Label timer_label;
        private MainScene window;

        private int minutes;
        private int seconds;
        private int total_time;
        private int countDown;

        /// <summary>
        /// Initializes a new instance of the <see cref="TimerGame"/> class.
        /// </summary>
        /// <param name="total_time">The total time in seconds.</param>
        /// <param name="window">The main window of the game.</param>
        public TimerGame(int total_time, MainScene window)
        {
            this.window = window;
            this.total_time = total_time;
            this.countDown = total_time;
            minutes = countDown / 60;
            seconds = countDown % 60;

            AutoSize = true;
            Margin = new Padding(25, 0, 0, 0);

            Label label1 = new System.Windows.Forms.Label()
            {
                Text = "🚀 Time remaining: 🚀",
                AutoSize = true,
                Font = new Font("Arial", 12F, FontStyle.Bold),
            };
            this.Controls.Add(label1, 0, 0);
            timer_label = new System.Windows.Forms.Label()
            {
                AutoSize = true,
                Text = String.Format("{0:D2}:{1:D2}", minutes, seconds),
                Font = new Font("Arial", 10F, FontStyle.Bold),
            };
            this.Controls.Add(timer_label, 0, 1);

            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += OnTimerTick;
        }

        /// <summary>
        /// Starts the timer.
        /// </summary>
        public void Start()
        {
            timer.Start();
        }

        /// <summary>
        /// Stops the timer.
        /// </summary>
        public void Stop()
        {
            timer.Stop();
        }

        /// <summary>
        /// Handles the timer tick event.
        /// </summary>
        /// <param name="sender">The object that triggered the event.</param>
        /// <param name="e">The event data.</param>
        private void OnTimerTick(object sender, EventArgs e)
        {
            countDown--;
            minutes = countDown / 60;
            seconds = countDown % 60;
            timer_label.Text = String.Format("{0:D2}:{1:D2}", minutes, seconds);

            if (countDown == 0)
            {
                timer.Stop();
                this.window.Controls.Clear();
                TimeExpireScene scene = new TimeExpireScene(window);
                window.Controls.Add(scene);
            }
        }
    }
}
