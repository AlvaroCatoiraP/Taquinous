using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Taquin
{
    /// <summary>
    /// Represents the objective grid in the Taquin game.
    /// </summary>
    internal class ObjectiveGrid : TableLayoutPanel
    {
        private List<Tile> current_objective;
        private int current_Size;
        private int current_target_position;
        private int current_Target_Directon;
        private List<HealthPoint> hearts;
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
            this.game = game;
            this.window = window;

            Board current_board = game.Get_current_board();
            this.current_objective = current_board.Get_Target();
            this.current_Size = current_board.Get_GridSize();
            this.current_target_position = current_board.Get_Target_position();
            this.current_Target_Directon = current_board.Get_Target_direction();

            Label objectiveLabel = new Label
            {
                Text = "Objectif à réaliser : ",
                AutoSize = true,
                Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold),
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                Margin = new Padding(0, 10, 0,10)

            };
            
            this.Controls.Add(objectiveLabel,0,0);
            this.SetColumnSpan(objectiveLabel, current_Size+1);
            if (game.Get_current_level().Get_specificOrder())
            {
                Draw_grid();
            }
            else
            {
                Draw_simple_grid();
            }

        }

        /// <summary>
        /// Draws the objective grid.
        /// </summary>
        public void Draw_grid()
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
                        
                        objective_Button.FlatAppearance.BorderColor = Color.Black;
                        objective_Button.FlatAppearance.BorderSize = 3;

                        objective_Button.BackColor = Color.LightPink;
                        Assembly myAssembly = Assembly.GetExecutingAssembly();
                        string image_path = "Taquin.Resources." + current_objective[i].Get_symbol_index() + ".jpg";
                        Stream myStream = myAssembly.GetManifestResourceStream(image_path);
                        objective_Button.Enabled = true;

                        if (myStream == null)
                            throw new FileNotFoundException($"Ressource introuvable : {image_path}");

                        Bitmap bmp = new Bitmap(myStream);

                        Bitmap resizedBmp = new Bitmap(bmp, objective_Button.Width, objective_Button.Height);

                        objective_Button.Image = resizedBmp;
                        objective_Button.ImageAlign = ContentAlignment.MiddleCenter;
                    }
                    else if (current_Target_Directon == 2 && current_target_position == i)
                    {
                        objective_Button.FlatAppearance.BorderColor = Color.Black;
                        objective_Button.FlatAppearance.BorderSize = 3;
                        objective_Button.Size = new Size(50, 50);
                        Assembly myAssembly = Assembly.GetExecutingAssembly();
                        string image_path = "Taquin.Resources." + current_objective[j].Get_symbol_index() + ".jpg";
                        Stream myStream = myAssembly.GetManifestResourceStream(image_path);
                        objective_Button.Enabled = true;

                        if (myStream == null)
                            throw new FileNotFoundException($"Ressource introuvable : {image_path}");

                        Bitmap bmp = new Bitmap(myStream);

                        Bitmap resizedBmp = new Bitmap(bmp, objective_Button.Width, objective_Button.Height);

                        objective_Button.Image = resizedBmp;
                        objective_Button.ImageAlign = ContentAlignment.MiddleCenter;
                    }
                    this.Controls.Add(objective_Button, j, i + 1);
                }

            }
        }

        public void Draw_simple_grid()
        {
           
            for (int i = 0; i < current_Size; i++)
            {
                Button objective_Button = new Button();
                objective_Button.Size = new Size(50, 50);
                objective_Button.FlatAppearance.BorderColor = Color.Black;
                objective_Button.FlatAppearance.BorderSize = 3;
                Assembly myAssembly = Assembly.GetExecutingAssembly();
                string image_path = "Taquin.Resources." + current_objective[i].Get_symbol_index() + ".jpg";
                Stream myStream = myAssembly.GetManifestResourceStream(image_path);
                objective_Button.Enabled = true;
                Bitmap bmp = new Bitmap(myStream);
                Bitmap resizedBmp = new Bitmap(bmp, objective_Button.Width, objective_Button.Height);
                objective_Button.Image = resizedBmp;
                objective_Button.ImageAlign = ContentAlignment.MiddleCenter;
                if (current_Target_Directon == 1)
                {
                    objective_Button.Margin = new Padding(50 * (current_Size / 2), 0,50 *(current_Size/2),0);
                    this.Controls.Add(objective_Button, 0, i+1);
                }
                else
                {
                    this.Controls.Add(objective_Button, i, 1);
                }

            }
            Panel emptyPanel = new Panel();
            emptyPanel.BackColor = Color.Transparent; 
            emptyPanel.Size = new Size(20+(current_Size *50), 20);
            this.Controls.Add(emptyPanel,0,current_Size+2);
            this.SetColumnSpan(emptyPanel, current_Size);
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

            hearts = new List<HealthPoint>();
            FlowLayoutPanel heartPanel = new FlowLayoutPanel
            {
                AutoSize = true,
                FlowDirection = FlowDirection.LeftToRight,
                Margin = new Padding(25, 2, 0, 0),
            };

            int indexRedLifes = 0;
            while (indexRedLifes < init_lifes)
            {
                hearts.Add(new HealthPoint());
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
