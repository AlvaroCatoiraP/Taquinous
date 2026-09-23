using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Forms;


namespace Taquin
{
    /// <summary>
    /// Represents the scene displayed when the player wins the game.
    /// </summary>
    internal class FinalWinScene : TableLayoutPanel
    {
        private MainScene window;
        private Game game;
        private TextBox inputBox;
        private Button submitButton;

        /// <summary>
        /// Initializes a new instance of the <see cref="FinalWinScene"/> class.
        /// </summary>
        /// <param name="window">The main window of the game.</param>
        /// <param name="game">The current game.</param>
        public FinalWinScene(MainScene window, Game game)
        {
            this.window = window;
            this.game = game;

            this.AutoSize = true;
            this.BackColor = Color.LightGreen;
            this.Padding = new Padding(20);

            Label message_label = new Label();
            message_label.Text = "Félicitations !\n\n" +
                                "🎉 Vous avez atteint le score requis pour terminer le dernier niveau ! 🎉\n\n";
            message_label.ForeColor = Color.Black;
            message_label.AutoSize = true;
            message_label.Location = new Point(100, 100);
            message_label.Font = new Font("Arial", 12, FontStyle.Bold);
            message_label.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(message_label, 0, 0);
            this.Left = 90;
            this.Top = 120;

            GameData gameData = new GameData();
            List<GameData> points = gameData.LoadAll();
            int maxIndex = 0;
            // Tri par points en ordre décroissant
            for (int i = 0; i < points.Count - 1; i++)
            {
                maxIndex = i;
                for (int j = i + 1; j < points.Count; j++)
                {
                    if (points[j].GetPoints() > points[maxIndex].GetPoints())
                    {
                        maxIndex = j;
                    }
                }

                if (maxIndex != i)
                {
                    GameData temp = points[i];
                    points[i] = points[maxIndex];
                    points[maxIndex] = temp;
                }
            }
            Player current_player = game.Get_Player();
            // Tester le nombre de scores avant de lire le dixième élément.
            if (points.Count < 10 || current_player.Get_points() > points[9].GetPoints()) {
                Label message_top_10 = new Label();
                message_top_10.Text = "\n\n Félicitations !\n\n" +
                                    "🎉 Vous avez atteint le score requis pour rentrer dans le top 10 ! 🎉\n\n";
                this.Controls.Add(message_top_10, 0, 1);

            }
            this.SetColumnSpan(message_label, 2);

            this.Text = "Text Input Example";
            this.Size = new Size(400, 200);
            this.BackColor = Color.White;

            inputBox = new TextBox();
            inputBox.Location = new Point(30, 30);
            inputBox.Size = new Size(300, 30);
            inputBox.Font = new Font("Segoe UI", 12);
            inputBox.ForeColor = Color.Black;
            inputBox.BackColor = Color.LightYellow;
            inputBox.BorderStyle = BorderStyle.FixedSingle;

            submitButton = new Button();
            submitButton.Text = "Enregistrer et retourner au menu principal";
            submitButton.Location = new Point(30, 70);
            submitButton.Size = new Size(100, 35);
            submitButton.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            submitButton.BackColor = Color.LightSteelBlue;
            submitButton.FlatStyle = FlatStyle.Flat;
            submitButton.Click += SubmitButton_Click;

            this.Controls.Add(inputBox);
            this.Controls.Add(submitButton);
        }

        private void SubmitButton_Click(object sender, EventArgs e)
        {
            string login = inputBox.Text;
            Player current_player = game.Get_Player();
            DateTime date = DateTime.Now;
            string dateFormatee = date.ToString("dd MMMM yyyy", new CultureInfo("fr-FR"));
            GameData game_data_to_save = new GameData(Regex.Replace(login, " ", ""), current_player.Get_points(),game.Get_current_level().Get_id(), current_player.Get_Lifes(), dateFormatee);
            game_data_to_save.SaveToFile();
            this.window.Controls.Clear();
            this.window.resetGame();
            this.window.Init_Form();
        }

    }
}



