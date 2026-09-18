using System;
using System.Collections.Generic;
using System.Linq;


namespace Taquin
{

    /// <summary>
    /// Represents a level in the Taquin game.
    /// </summary>
    public class Level
    {
        private List<Board> boards;
        private int dificulty;
        private int secs;
        private int targetScore;
        private int id;
        private bool specificOrder;

        /// <summary>
        /// Initializes a new instance of the <see cref="Level"/> class with a specified difficulty.
        /// Sets default values for score target, duration, ID, and initializes the first game board.
        /// </summary>
        /// <param name="difficulty">The difficulty level of the game. Default is 3.</param>
        /// <remarks>
        /// - Initializes the target score to 15.
        /// - Creates a list of game boards and adds the first board using the given difficulty.
        /// - Sets the default time limit to 60 seconds.
        /// - Sets the level ID to 1.
        /// </remarks>
        public Level(int dificulty = 3)
        {
            this.targetScore = 15;
            boards = new List<Board>();
            this.dificulty = dificulty;
            Board firs_board = new Board(dificulty);
            boards.Add(firs_board);
            this.secs = 120;
            this.id = 1;
            this.specificOrder = false;
        }

        /// <summary>
        /// Generates a stronger level by increasing the difficulty and adjusting other parameters accordingly.
        /// </summary>
        /// <returns>
        /// A new <see cref="Level"/> instance with:
        /// - Increased difficulty by <paramref name="step"/>
        /// - Increased ID by <paramref name="step"/>
        /// - Reduced time limit (seconds), calculated as half the original seconds multiplied by <paramref name="step"/>
        /// </returns>
        public Level Get_stronger_level(int step)
        {
            Level levelDst = new Level(this.dificulty + step);
            levelDst.id = this.id + step;
            levelDst.targetScore = this.targetScore *2;
            levelDst.specificOrder = true;
            return levelDst;
        }


        /// <summary>
        /// Adds a new round to the level.
        /// </summary>
        /// <param name="round">The round to add.</param>
        public void PrepareChallenge()
        {
            boards.Clear();
            boards.Add(Board.CreateChallenge());
            specificOrder = true;
        }

        public void Add_new_board(Board board)
        {
            boards.Add(board);
        }

        /// <summary>
        /// Gets the current round of the level.
        /// </summary>
        /// <returns>The current round.</returns>
        public Board Get_current_board()
        {
            return boards.Last();
        }

        /// <summary>
        /// Gets the number of seconds allocated for this level.
        /// </summary>
        /// <returns>The number of seconds.</returns>
        public int Get_secs()
        {
            return secs;
        }

        /// <summary>
        /// Gets the specificOrder for this level.
        /// </summary>
        /// <returns>true if specificOrder is activated , fals if not </returns>
        public bool Get_specificOrder()
        {
            return specificOrder;
        }


        /// <summary>
        /// Gets the id of this level.
        /// </summary>
        /// <returns>The number of the id.</returns>
        public int Get_id()
        {
            return id;
        }


        /// <summary>
        /// Sets the number of seconds allocated for this level.
        /// </summary>
        public void Set_secs(int value)
        {
            secs = value;
        }

        internal bool Confirm_score_success(int current_score)
        {
            return current_score == targetScore;
        }

        internal int Get_target_score()
        {
            return targetScore;
        }

    }
}
