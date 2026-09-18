using System;
using System.Drawing;
using System.Windows.Forms;

namespace Taquin
{
    internal class RobotConsole : GroupBox
    {
        private TextBox editor;
        private Label output;
        private GameGrid gameGrid;
        private GameScene scene;
        private Button run;
        private const string ChallengeExample = "selectioner ... ...\r\nsi ...\r\n    aller ...\r\nfin\r\n\r\nselectioner ... ...\r\nsi ...\r\n    aller ...\r\nfin\r\n\r\nselectioner ... ...\r\nsi ...\r\n    aller ...\r\nfin\r\n\r\nselectioner ... ...\r\nsi ...\r\n    aller ...\r\nfin\r\n\r\nselectioner ... ...\r\nsi ...\r\n    aller ...\r\nfin";
        private const string Example = "selectioner 2 3\r\nsi vide_en_bas\r\n    aller bas\r\nfin";

        public RobotConsole(GameGrid grid, GameScene gameScene)
        {
            gameGrid = grid;
            scene = gameScene;
            Text = "Programme ton robot";
            Size = new Size(270, 490);
            Margin = new Padding(0, 3, 12, 0);
            Label help = new Label { Text = "selectioner ligne colonne (à partir de 1).\r\nPuis si, aller et fin pour chaque bloc.", Location = new Point(10, 20), Size = new Size(250, 40) };
            var tabs = new TabControl { Location = new Point(10, 66), Size = new Size(250, 190) };
            var programTab = new TabPage("Programme");
            var commandsTab = new TabPage("Commandes");
            editor = new TextBox { Multiline = true, AcceptsTab = true, WordWrap = false, ScrollBars = ScrollBars.Both, Font = new Font("Consolas", 9), Text = Example, Dock = DockStyle.Fill };
            var commands = new TextBox
            {
                Multiline = true, ReadOnly = true, Dock = DockStyle.Fill,
                ScrollBars = ScrollBars.Vertical, BackColor = Color.White,
                Font = new Font("Consolas", 9),
                Text = "SÉLECTION\r\nselectioner ligne colonne\r\nExemple : selectioner 2 3\r\nLigne puis colonne, dès 1.\r\nselectionner est aussi accepté.\r\nDans le défi : clic sur une tuile.\r\n\r\nDÉPLACEMENTS\r\naller haut\r\naller bas\r\naller gauche\r\naller droite\r\n\r\nCONDITIONS\r\nvide_en_haut\r\nvide_en_bas\r\nvide_a_gauche\r\nvide_a_droite\r\n\r\nÉquivalents :\r\nhaut_possible\r\nbas_possible\r\ngauche_possible\r\ndroite_possible\r\n\r\nLa case voisine de la tuile\r\nsélectionnée doit être vide.\r\n\r\nBLOC SI\r\nsi vide_en_bas\r\n    aller bas\r\nfin\r\n\r\nTu peux écrire plusieurs blocs.\r\nChaque bloc se termine par fin.\r\n\r\nOPTIONS\r\nsinon si condition\r\n    aller direction\r\nsinon\r\n    aller direction\r\nÀ placer avant le fin du bloc."
            };
            programTab.Controls.Add(editor);
            commandsTab.Controls.Add(commands);
            tabs.TabPages.AddRange(new[] { programTab, commandsTab });
            run = new Button { Text = "Exécuter", Location = new Point(10, 262), Size = new Size(145, 30), BackColor = Color.LightGreen };
            Button reset = new Button { Text = "Remettre l'exemple", Location = new Point(10, 298), Size = new Size(155, 30) };
            output = new Label { Text = "Prêt ! Exécuter lance tous les blocs dans l'ordre.", Location = new Point(10, 334), Size = new Size(250, 65) };
            Button challenge = new Button { Text = scene.IsChallenge ? "Recommencer le défi" : "Démarrer le défi fixe", Location = new Point(10, 402), Size = new Size(245, 30) };
            challenge.Click += Challenge_Click;
            Controls.Add(challenge);
            if (scene.IsChallenge)
            {
                help.Text = "Écris 5 si. Sélection : ligne colonne\r\n(dès 1), ou clic sur une tuile.";
                editor.Text = ChallengeExample;
                reset.Text = "Remettre la consigne";
                Button back = new Button { Text = "Retour au jeu normal", Location = new Point(10, 438), Size = new Size(245, 30) };
                back.Click += Back_Click;
                Controls.Add(back);
            }
            run.Click += Run_Click;
            reset.Click += Reset_Click;
            Controls.AddRange(new Control[] { help, tabs, run, reset, output });
        }
        private void Challenge_Click(object sender, EventArgs e)
        {
            scene.StartChallenge();
        }

        private void Back_Click(object sender, EventArgs e)
        {
            scene.ReturnToGame();
        }

        public void ShowChallengeSuccess(int moves)
        {
            run.Enabled = false;
            output.ForeColor = Color.DarkGreen;
            output.Text = "Bravo ! Défi réussi en " + moves + " mouvements. Objectif : 5. Clique sur Recommencer pour rejouer.";
        }
        private void Run_Click(object sender, EventArgs e)
        {
            try
            {
                output.ForeColor = Color.DarkGreen;
                string message = RobotInterpreter.Execute(editor.Text, gameGrid.CheckRobotCondition, gameGrid.MoveRobot, gameGrid.SelectRobotTile);
                if (!scene.ChallengeFinished) output.Text = message;
            }
            catch (FormatException error)
            {
                output.ForeColor = Color.Firebrick;
                output.Text = error.Message;
            }
        }

        private void Reset_Click(object sender, EventArgs e)
        {
            editor.Text = scene.IsChallenge ? ChallengeExample : Example;
            output.ForeColor = Color.Black;
            output.Text = "Exemple rétabli. Complète les sélections et les si.";
        }
    }
}

