using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Taquin
{
    /// <summary>
    /// Represents a heart drawn as a custom control.
    /// </summary>
    public class LittleHeart : Control
    {
        /// <summary>
        /// Gets or sets the color of the heart.
        /// </summary>
        public Color HeartColor { get; set; } = Color.Red;

        /// <summary>
        /// Initializes a new instance of the <see cref="LittleHeart"/> class.
        /// </summary>
        public LittleHeart()
        {
            this.Size = new Size(30, 25);
            this.DoubleBuffered = true;
        }

        /// <summary>
        /// Handles the paint event to draw the heart.
        /// </summary>
        /// <param name="e">The paint event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddBezier(new Point(17, 20), new Point(2, 5), new Point(9, -3), new Point(17, 5));
                path.AddBezier(new Point(17, 5), new Point(25, -3), new Point(32, 5), new Point(17, 20));

                using (SolidBrush brush = new SolidBrush(HeartColor))
                {
                    g.FillPath(brush, path);
                }
            }
        }
    }
}