using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Taquin
{
    public class Display_top_10 : TableLayoutPanel
    {
        private Label lblScores;
        private Button goBack;
        private GameData gameData;
        private MainScene window;

        public Display_top_10(MainScene window)
        {
            this.window = window;
            this.Dock = DockStyle.Fill;
            this.ColumnCount = 1;
            this.RowCount = 2;
            this.RowStyles.Add(new RowStyle(SizeType.Percent, 85));
            this.RowStyles.Add(new RowStyle(SizeType.Percent, 15));
            this.Padding = new Padding(20);

            lblScores = new Label()
            {
                AutoSize = false,
                TextAlign = ContentAlignment.TopCenter,
                Dock = DockStyle.Fill,
                Font = Font = new Font("Arial", 12, FontStyle.Bold)
            };
            this.Controls.Add(lblScores, 0, 0);

            goBack = new Button();
            goBack.Text = "Retour";
            goBack.Location = new Point(30, 70);
            goBack.Size = new Size(100, 35);
            goBack.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            goBack.BackColor = Color.LightSteelBlue;
            goBack.FlatStyle = FlatStyle.Flat;
            goBack.Click += GoBack_Click;
            this.Controls.Add(goBack, 0, 1);

            gameData = new GameData();
            LoadScores();
        }

        private void LoadScores()
        {
            List<GameData> points = gameData.LoadAll();

            // Tri par points en ordre décroissant
            for (int i = 0; i < points.Count - 1; i++)
            {
                int maxIndex = i;
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

            if (points.Count == 0)
            {
                lblScores.Text = "Aucun score enregistré.";
                return;
            }

            string display = "";
            int maxToDisplay = Math.Min(1, points.Count);
            for (int i = 0; i < maxToDisplay; i++)
            {
                display += $"{i + 1}. Pseudo: {points[i].GetLogin()}, Points: {points[i].GetPoints()}, Niveau: {points[i].GetLevel()}, Vies: {points[i].GetLifes()}, Date: {points[i].GateDate()}\n";
            }

            lblScores.Text = display;
        }


        private void GoBack_Click(object sender, EventArgs e)
        {
            this.window.Controls.Clear();
            this.window.resetGame();
            this.window.Init_Form();
        }
    }
}
