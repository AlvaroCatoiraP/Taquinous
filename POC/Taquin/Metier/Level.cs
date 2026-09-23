using System;
using System.Collections.Generic;
using System.Linq;
using Taquin.Metier;

namespace Taquin
{

    /// <summary>
    /// Represents a level in the Taquin game.
    /// </summary>
    public class Level
    {
        private List<Round> rounds;
        private int dificulty;
        private int secs;
        private int targetScore;

        /// <summary>
        /// Initializes a new instance of the <see cref="Level"/> class.
        /// </summary>
        /// <param name="dificulty">The difficulty level of the game.</param>
        public Level(int dificulty)
        {
            this.targetScore = 20;
            rounds = new List<Round>();
            this.dificulty = dificulty;
            Round firs_round = new Round(dificulty);
            rounds.Add(firs_round);
            this.secs = 10;
        }

        /// <summary>
        /// Adds a new round to the level.
        /// </summary>
        /// <param name="round">The round to add.</param>
        public void Add_new_round(Round round)
        {
            rounds.Add(round);
        }

        /// <summary>
        /// Gets the current round of the level.
        /// </summary>
        /// <returns>The current round.</returns>
        public Round Get_current_round()
        {
            return rounds.Last();
        }

        /// <summary>
        /// Gets the number of seconds allocated for this level.
        /// </summary>
        /// <returns>The number of seconds.</returns>
        public int Get_Secs()
        {
            return secs;
        }

        internal bool ConfirmScoreSuccess(int current_score)
        {
            return current_score == targetScore;
        }

        internal int GetTargetScore()
        {
            return targetScore;
        }

    }
}
