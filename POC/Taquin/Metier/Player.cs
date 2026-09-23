using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taquin
{

    /// <summary>
    /// Represents a player in the Taquin game.
    /// </summary>
    public class Player
    {
        private string name;
        private int score;
        private int lifes;
        private int initLifes;

        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        /// <param name="name">The name of the player.</param>
        public Player(string name)
        {
            this.name = name;
            this.score = 0;
            this.lifes = 3;
            this.initLifes = lifes;
        }

        /// <summary>
        /// Decreases the player's number of lives.
        /// </summary>
        public void Decrement_lifes() { if (lifes > 0) lifes--; }

        /// <summary>
        /// Gets the current score of the player.
        /// </summary>
        public int GetScore() { return score; }

        /// <summary>
        /// Gets the remaining number of lives of the player.
        /// </summary>
        public int Get_Lifes() { return lifes; }

        /// <summary>
        /// Decreases the player's score.
        /// </summary>
        public void Decrement_Score() { if (score > 0) score--; }

        /// <summary>
        /// Increases the player's score.
        /// </summary>
        public void Increment_Score() { score += 5; }

        /// <summary>
        /// Resets the player's lives.
        /// </summary>
        public void reset_lifes() { lifes = initLifes; }

        /// <summary>
        /// Gets the initial number of lives of the player.
        /// </summary>
        public int GetInitLifes() { return initLifes; }

        /// <summary>
        /// Resets the player's score and lives.
        /// </summary>
        public void reset()
        {
            this.score = 0;
            this.reset_lifes();
        }
    }
}
