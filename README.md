#  Projet - Jeu du Taquin (version revisitée)

##  Auteur
**Alvaro CATOIRA**
**22-04-2025**

##  Principe du jeu
Dans cette version revisitée du célèbre **Taquin**, l’objectif **n’est pas de réordonner tous les carreaux**, mais de réussir à **aligner certains symboles** (dans une ligne ou une colonne) selon une **consigne donnée**.

Le joueur enchaîne plusieurs grilles au cours d’une même partie, chacune apportant un défi unique.

---

##  Fonctionnalités

### Interface et ergonomie
- Application **graphique unique** réalisée en **Windows Forms**.
- **Interface fluide** : aucune latence ni gel de la fenêtre pendant le jeu.
- **Menu d'accueil** avec les choix :
  - Jouer une partie
  - Lire les règles du jeu
  - Consulter le **Top 10**
  - Quitter le jeu

### Partie et gameplay
- Affichage des éléments essentiels :
  - **Score** (début à 0)
  - **Nombre de vies** (3 au départ)
  - **Compte à rebours** pour chaque niveau
- Chaque **tour** de jeu comprend :
  - Une **grille carrée** (N x N) contenant N² - 1 carreaux (1 vide)
  - Une **consigne aléatoire** du type :
    - Aligner les symboles donnés sur une ligne ou colonne
    - Avec un **ordre précis** et un **sens d’alignement**
  - Tirage pseudo-aléatoire des symboles, de leur position initiale et de la consigne

### Top 10
- **Classement persistant**, même après la fermeture du jeu
- Contient : **pseudo, score, date**, triés par **ordre décroissant**
- dans un fichier binaire à la racine du jeu "taquin_data.bin"

---

##  Lancement du projet

1. Extraire l’archive `.zip` fournie (`CATOIRA_Alvaro.zip`)
2. Ouvrir le fichier `*.sln` avec **Visual Studio 2022** (ou version compatible)
3. Compiler le projet et exécuter en mode **Debug** ou **Release**

---
##  Lancement du jeu

1. Utiliser l'executable (.exe) dans le dossier executable

---

## Contenu de l’archive

L’archive contient :
- Le code source complet du jeu (`.cs`, `.Designer.cs`, `.sln`, etc.)
- Les ressources éventuelles (images ou autres fichiers)
- Les fichiers de configuration nécessaires à l’exécution

---

## Envoi du projet

Le projet a été envoyé **via Moodle** au professeur, mais le dossier .git  a été envoyé **via Discord**  car l’archive était **trop volumineus pour être téléversée sur Moodle**.

---

## Remarques

- Ce projet a été développé et testé sous **Windows 10** avec **Visual Studio 2022**
- Veuillez vous assurer d’avoir le **.NET Framework** nécessaire installé

---

Bon jeu !