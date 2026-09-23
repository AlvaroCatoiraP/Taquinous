using System;
using System.Collections.Generic;
using Taquin.Metier;

namespace Taquin
{
    /// <summary>
    /// Represents a game of Taquin.
    /// </summary>
    public class Game
    {
        private Level current_Level;
        private Player player;
        private Round current_round;

        /// <summary>
        /// Initializes a new instance of the <see cref="Game"/> class.
        /// </summary>
        /// <param name="login">The name of the player.</param>
        public Game(string login)
        {
            this.player = new Player(login);
            current_Level = new Level(3);
            current_round = current_Level.Get_current_round();
        }

        /// <summary>
        /// Checks if the level's objective is correctly achieved.
        /// </summary>
        /// <returns>True if the objective is achieved, otherwise false.</returns>
        public bool IsCorrect_Target()
        {
            bool isCorrect_Target = true;
            this.current_round = this.current_Level.Get_current_round();
            Board current_board = current_round.Get_Board();
            int current_Target = current_board.Get_Target();
            List<List<Cell>> current_Grid = current_round.Get_Board().Get_Board_Grid();
            int current_Size = current_round.Get_Board().Get_GridSize();
            int current_position = current_round.Get_Board().Get_Target_position();

            if (current_board.Get_Target_direction() == 1)
            {
                for (int i = 0; i < current_Size && isCorrect_Target; i++)
                {
                    if (current_Grid[i][current_position].IsOccupied())
                    {
                        if (current_Grid[i][current_position].Get_tile().Get_Symbol() != current_Target)
                        {
                            isCorrect_Target = false;
                        }
                    }
                    else
                    {
                        isCorrect_Target = false;
                    }
                }
            }
            else if (current_board.Get_Target_direction() == 2)
            {
                for (int i = 0; i < current_Size; i++)
                {
                    if (current_Grid[current_position][i].IsOccupied())
                    {
                        if (current_Grid[current_position][i].Get_tile().Get_Symbol() != current_Target)
                        {
                            isCorrect_Target = false;
                        }
                    }
                    else
                    {
                        isCorrect_Target = false;
                    }
                }
            }
            return isCorrect_Target;
        }

        /// <summary>
        /// Gets the current round of the game.
        /// </summary>
        /// <returns>The current round.</returns>
        public Round Get_current_round()
        {
            return current_Level.Get_current_round();
        }

        /// <summary>
        /// Gets the current player of the game.
        /// </summary>
        /// <returns>The current player.</returns>
        public Player Get_Player()
        {
            return player;
        }

        /// <summary>
        /// Gets the current level of the game.
        /// </summary>
        /// <returns>The current level.</returns>
        public Level Get_current_level()
        {
            return this.current_Level;
        }

        /// <summary>
        /// Resets the game by starting over from the first level.
        /// </summary>
        public void reset()
        {
            player.reset();
            current_Level = new Level(3);
            current_round = current_Level.Get_current_round();
        }
    }
}
