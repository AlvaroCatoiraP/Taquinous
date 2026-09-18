using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Taquin.Metier;

namespace Taquin
{
    /// <summary>
    /// Represents the objective grid in the Taquin game.
    /// </summary>
    internal class ObjectiveGrid : TableLayoutPanel
    {
        private int current_objective;
        private int current_Size;
        private int current_target_position;
        private int current_Target_Directon;
        private List<LittleHeart> hearts;
        private Game game;
        private MainScene window;

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectiveGrid"/> class.
        /// </summary>
        /// <param name="game">The current game.</param>
        /// <param name="window">The main window.</param>
        public ObjectiveGrid(Game game, MainScene window)
        {
            AutoSize = true;
            BorderStyle = BorderStyle.Fixed3D;
            Margin = new Padding(30, 5, 10, 0);
            this.game = game;
            this.window = window;

            Board current_board = game.Get_current_round().Get_Board();
            this.current_objective = current_board.Get_Target();
            this.current_Size = current_board.Get_GridSize();
            this.current_target_position = current_board.Get_Target_position();
            this.current_Target_Directon = current_board.Get_Target_direction();

            Label objectiveLabel = new Label
            {
                Text = "Objectif à réaliser : ",
                AutoSize = true,
                Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold),
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            };

            this.Controls.Add(objectiveLabel);
            Draw_Grid();
            this.SetColumnSpan(objectiveLabel, current_Size);
        }

        /// <summary>
        /// Draws the objective grid.
        /// </summary>
        public void Draw_Grid()
        {
            for (int i = 0; i < current_Size; i++)
            {
                for (int j = 0; j < current_Size; j++)
                {
                    Button objective_Button = new Button()
                    {
                        Enabled = false,
                        Size = new Size(50, 50),
                        BackColor = Color.LightBlue,
                    };

                    if (current_Target_Directon == 1 && current_target_position == j)
                    {
                        objective_Button.BackColor = Color.LightPink;
                        objective_Button.Text = current_objective.ToString();
                    }
                    else if (current_Target_Directon == 2 && current_target_position == i)
                    {
                        objective_Button.BackColor = Color.LightPink;
                        objective_Button.Text = current_objective.ToString();
                    }
                    this.Controls.Add(objective_Button, j, i + 1);
                }
            }
        }

        /// <summary>
        /// Initializes and returns the lives panel.
        /// </summary>
        /// <returns>A panel displaying the player's remaining lives.</returns>
        public FlowLayoutPanel InitLifes()
        {
            int init_lifes = game.Get_Player().GetInitLifes();
            int lost_lifes = init_lifes - game.Get_Player().Get_Lifes();
            int rest_lifes = init_lifes - lost_lifes;

            hearts = new List<LittleHeart>();
            FlowLayoutPanel heartPanel = new FlowLayoutPanel
            {
                AutoSize = true,
                FlowDirection = FlowDirection.LeftToRight,
                Margin = new Padding(25, 2, 0, 0),
            };

            int indexRedLifes = 0;
            while (indexRedLifes < init_lifes)
            {
                hearts.Add(new LittleHeart());
                heartPanel.Controls.Add(hearts.Last());
                indexRedLifes++;
            }

            int indexBlackLifes = rest_lifes;
            while (indexBlackLifes < init_lifes)
            {
                hearts[indexBlackLifes].HeartColor = Color.Black;
                indexBlackLifes++;
            }
            return heartPanel;
        }
    }
}
