using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Taquin.Gui
{
    /// <summary>
    /// Represents a label displaying the rules of the Taquin game.
    /// </summary>
    internal class RoolsLabel : Label
    {
        private string rools;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoolsLabel"/> class.
        /// </summary>
        public RoolsLabel()
        {
            Text = "" +
                "Game Rules\r\n\r\n    " +
                "Objective: Align the symbols in a line or column from multiple consecutive grids.\r\n\r\n    " +
                "Start of the Game:\r\n\r\n        The player starts with a score of 0 and three lives.\r\n\r\n        " +
                "A countdown indicates the remaining time for each turn.\r\n\r\n    " +
                "Movement:\r\n\r\n        " +
                "The player clicks on a tile to attempt to move an adjacent tile to an empty location.\r\n\r\n        " +
                "If the move is valid, the tile moves and a new empty location is created.\r\n\r\n    " +
                "Scoring:\r\n\r\n        " +
                "The player earns points for each successful turn before the countdown ends.\r\n\r\n        " +
                "Losing a life if the countdown reaches zero without success.\r\n\r\n    " +
                "Difficulty Levels:\r\n\r\n        " +
                "Three difficulty levels with varying grid sizes.\r\n\r\n        " +
                "The player must complete multiple turns to advance to the next level.\r\n\r\n    " +
                "Top 10:\r\n\r\n        " +
                "Scores are saved in a visible Top 10, including the date, username, and score.\r\n\r\n    " +
                "End of Game:\r\n\r\n       " +
                "After each game, the player receives feedback on their results and can return to the main menu.\r\n\r\n    " +
                "End of the Game:\r\n\r\n        " +
                "The game ends when the player loses all their lives.";

            ForeColor = Color.Black;
            AutoSize = true;
            Location = new Point(100, 100);
            Font = new Font("Arial", 10, FontStyle.Bold);
            TextAlign = ContentAlignment.MiddleCenter;
            Padding = new Padding(30);
        }
    }
}
