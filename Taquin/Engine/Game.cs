using System;
using System.Collections.Generic;


namespace Taquin
{
    /// <summary>
    /// Represents a game of Taquin.
    /// </summary>
    public class Game
    {
        private Level current_Level;
        private Player player;
        private int nb_levels;

        /// <summary>
        /// Initializes a new instance of the <see cref="Game"/> class.
        /// </summary>
        /// <param name="login">The name of the player.</param>
        public Game(string login)
        {
            this.player = new Player();
            current_Level = new Level();
            nb_levels = 3;
        }

        /// <summary>
        /// Checks if the level's objective is correctly achieved.
        /// </summary>
        /// <returns>True if the objective is achieved, otherwise false.</returns>
        public bool Is_correct_target()
        {

            Board current_board = Get_current_board();
            bool isCorrect_Target = true;
            List<Tile> current_Target = current_board.Get_Target();
            List<List<Cell>> current_Grid = current_board.Get_Board_Grid();
            int current_Size = current_board.Get_GridSize();

            int current_position = current_board.Get_Target_position();
            if (current_Level.Get_specificOrder())
            {

                if (current_board.Get_Target_direction() == 1)
                {
                    for (int i = 0; i < current_Size && isCorrect_Target; i++)
                    {
                        if (current_Grid[i][current_position].Is_occupied())
                        {
                            if (current_Grid[i][current_position].Get_tile().Get_symbol_index() != current_Target[i].Get_symbol_index())
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
                        if (current_Grid[current_position][i].Is_occupied())
                        {
                            if (current_Grid[current_position][i].Get_tile().Get_symbol_index() != current_Target[i].Get_symbol_index())
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
            else
            {
                return is_correct_easy_target();
            }
        }


        public bool is_correct_easy_target()
        {
            Board current_board = Get_current_board();
            bool isCorrect_Target = true;
            List<Tile> current_Target = current_board.Get_Target();
            List<List<Cell>> current_Grid = current_board.Get_Board_Grid();
            int current_Size = current_board.Get_GridSize();

            int current_position = current_board.Get_Target_position();
            int nb_target_found = 0;
            if (current_board.Get_Target_direction() == 2)
            {
                for (int row = 0; row < current_Size && nb_target_found < 3; row++)
                {
                    isCorrect_Target = true;
                    for (int col = 0; col < current_Size && isCorrect_Target; col++)
                    {
                        if (current_Grid[row][col].Is_occupied())
                        {
                            if (current_Grid[row][col].Get_tile().Get_symbol_index() != current_Target[col].Get_symbol_index())
                            {
                                isCorrect_Target = false;
                            }
                            else
                            {
                                nb_target_found++;
                            }
                        }
                        else
                        {
                            isCorrect_Target = false;
                        }
                    }
                }
            }
            else if (current_board.Get_Target_direction() == 1)
            {
                for (int col = 0; col < current_Size && nb_target_found < 3; col++)
                {
                    isCorrect_Target = true;
                    for (int row = 0; row < current_Size && isCorrect_Target; row++)
                    {
                        if (current_Grid[row][col].Is_occupied())
                        {
                            if (current_Grid[row][col].Get_tile().Get_symbol_index() != current_Target[row].Get_symbol_index())
                            {
                                isCorrect_Target = false;
                            }
                            else
                            {
                                nb_target_found++;
                            }
                        }
                        else
                        {
                            isCorrect_Target = false;
                        }
                    }
                }
            }
            return isCorrect_Target;

        }

        public int Get_nb_levels()
        {
            return nb_levels;
        }
        public Board Get_current_board()
        {
            return current_Level.Get_current_board();
        }

        public Player Get_Player()
        {
            return player;
        }

        public Level Get_current_level()
        {
            return this.current_Level;
        }

        public Player Get_player()
        {
            return player;
        }

        public void Set_current_level(Level level)
        {
            this.current_Level = level;
        }


        /// <summary>
        /// Resets the game by starting over from the first level.
        /// </summary>
        public void reset()
        {
            player.reset();
            current_Level = new Level();
        }
    }
}
