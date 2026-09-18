using Taquin.Metier;

namespace Taquin
{

    /// <summary>
    /// Represents a game round in the Taquin game.
    /// </summary>
    public class Round
    {
        private int board_Size;
        private Board board;

        /// <summary>
        /// Initializes a new instance of the <see cref="Round"/> class.
        /// </summary>
        /// <param name="board_Size">Size of the game board.</param>
        public Round(int board_Size)
        {
            this.board_Size = board_Size;
            if (board_Size >= 3)
            {
                this.board = new Board(this.board_Size);
            }
        }

        /// <summary>
        /// Gets the game board associated with the round.
        /// </summary>
        /// <returns>The game board of the round.</returns>
        public Board Get_Board()
        {
            return this.board;
        }
    }
}
