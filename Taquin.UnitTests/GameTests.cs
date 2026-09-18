using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Taquin.UnitTests

{
    [TestClass]
    public class GameTests
    {
        public void ArrangeGameGrid(Game game)
        {

            Level current_Level = game.Get_current_level();

            Board current_board = current_Level.Get_current_board();
            List<List<Cell>> current_Grid = current_board.Get_Board_Grid();
            List<Tile> current_Target = current_board.Get_Target();
            int current_direction = current_board.Get_Target_direction();
            int current_Size = current_board.Get_GridSize();
            int current_position = current_board.Get_Target_position();

            //Vertical
            if (current_direction == 1)
            {
                for (int i = 0; i < current_Size; i++)
                {
                    Tile tile = current_Target[i];
                    current_Grid[i][current_position].Set_Tile(tile);
                }
            }
            //horizontal
            if (current_direction == 2)
            {
                for (int i = 0; i < current_Size; i++)
                {
                    Tile tile = current_Target[i];
                    current_Grid[current_position][i].Set_Tile(tile);
                }
            }
        }

        [TestMethod]
        public void IsCorrect_Target_NominalCase_True()
        {
            // Arrange
            Game game = new Game("toto");

            // Act

            ArrangeGameGrid(game);
            bool resultVertical = game.Is_correct_target();


            // Assert
            Assert.AreEqual(true,resultVertical);


        }
    }
}
