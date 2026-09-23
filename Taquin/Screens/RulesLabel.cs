using System.Drawing;
using System.Windows.Forms;

namespace Taquin
{
    /// <summary>
    /// Represents a label displaying the rules of the Taquin game.
    /// </summary>
    internal class RulesLabel : Label
    {
        private string rools;

        /// <summary>
        /// Initializes a new instance of the <see cref="RulesLabel"/> class.
        /// </summary>
        public RulesLabel()
        {
            Text = "" +
            "Règles du jeu\n\n" +
            "    Objectif : Aligner les symboles en ligne ou en colonne.\n\n" +
            "    Début du jeu :\n\n" +
            "        Le joueur commence avec 0 point et 3 vies.\n" +
            "        Un compte à rebours indique le temps restant à chaque tour.\n\n" +
            "    Déplacement :\n\n" +
            "        Cliquer sur une case pour déplacer une case voisine vers une case vide.\n" +
            "        Si le déplacement est valide, la case bouge et une nouvelle case vide apparaît.\n\n" +
            "    Score :\n\n" +
            "        Le joueur gagne des points pour chaque tour réussi dans le temps.\n" +
            "        Une vie est perdue si le temps expire sans réussite.\n\n" +
            "    Niveaux de difficulté :\n\n" +
            "        Trois niveaux avec des tailles de grilles différentes.\n" +
            "        Plusieurs tours sont nécessaires pour passer au niveau suivant.\n\n" +
            "    Top 10 :\n\n" +
            "        Les meilleurs scores (date, pseudo, score) sont enregistrés et visibles.\n\n" +
            "    Fin du jeu :\n\n" +
            "        Le jeu se termine quand le joueur perd ses 3 vies.\n";

            ForeColor = Color.Black;
            AutoSize = true;
            Location = new Point(100, 100);
            Font = new Font("Arial", 10, FontStyle.Bold);
            TextAlign = ContentAlignment.MiddleCenter;
            Padding = new Padding(30);
        }
    }
}
