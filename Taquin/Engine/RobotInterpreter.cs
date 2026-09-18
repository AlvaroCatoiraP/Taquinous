using System;
using System.Collections.Generic;

namespace Taquin
{
    internal static class RobotInterpreter
    {
        private sealed class Instruction
        {
            public string Condition;
            public string Direction;
            public int Line;
            public int Row;
            public int Col;
            public bool IsSelection;
            public List<Instruction> Branches;
        }

        public static string Execute(string code,
            Func<string, bool> checkCondition, Func<string, bool> move,
            Func<int, int, bool> select = null)
        {
            // Analyser tout le programme avant la première action.
            string[] lines = (code ?? "").ToLowerInvariant().Split('\n');
            var program = new List<Instruction>();
            int i = 0;
            while (i < lines.Length)
            {
                string line = lines[i].Trim();
                if (line.Length == 0) { i++; continue; }
                if (line.StartsWith("selectioner ") || line.StartsWith("selectionner "))
                {
                    string[] parts = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                    int row, col;
                    if (parts.Length != 3 || !int.TryParse(parts[1], out row) ||
                        !int.TryParse(parts[2], out col) || row < 1 || col < 1)
                        throw Error(i, "utilise selectioner ligne colonne, par exemple selectioner 1 2.");
                    if (select == null)
                        throw Error(i, "la sélection doit être raccordée à SelectRobotTile dans RobotConsole.");
                    program.Add(new Instruction { IsSelection = true, Row = row, Col = col, Line = i + 1 });
                    i++;
                }
                else if (line.StartsWith("si "))
                {
                    var block = new Instruction { Branches = new List<Instruction>() };
                    string condition = line.Substring(3).Trim();
                    while (true)
                    {
                        CheckCondition(condition, i);
                        int lineNumber = i + 1;
                        i++;
                        SkipEmpty(lines, ref i);
                        string direction = ReadCommand(lines, i);
                        block.Branches.Add(new Instruction { Condition = condition, Direction = direction, Line = lineNumber });
                        i++;
                        SkipEmpty(lines, ref i);
                        if (i < lines.Length && lines[i].Trim().StartsWith("sinon si "))
                        {
                            condition = lines[i].Trim().Substring(9).Trim();
                            continue;
                        }
                        break;
                    }
                    if (i < lines.Length && lines[i].Trim() == "sinon")
                    {
                        i++;
                        SkipEmpty(lines, ref i);
                        block.Branches.Add(new Instruction { Direction = ReadCommand(lines, i), Line = i + 1 });
                        i++;
                        SkipEmpty(lines, ref i);
                    }
                    if (i >= lines.Length || lines[i].Trim() != "fin")
                        throw Error(i, "termine le bloc si avec fin.");
                    i++;
                    program.Add(block);
                }
                else
                {
                    program.Add(new Instruction { Direction = ReadCommand(lines, i), Line = i + 1 });
                    i++;
                }
            }
            if (program.Count == 0) throw new FormatException("Écris un programme.");

            var messages = new List<string>();
            foreach (Instruction instruction in program)
            {
                if (instruction.IsSelection)
                {
                    if (!select(instruction.Row, instruction.Col))
                    {
                        messages.Add("Ligne " + instruction.Line + ": sélection impossible (case vide, hors grille ou défi terminé). Programme arrêté.");
                        break;
                    }
                    messages.Add("Tuile sélectionnée : " + instruction.Row + " " + instruction.Col + ".");
                    continue;
                }
                Instruction action = instruction;
                if (instruction.Branches != null)
                {
                    action = null;
                    foreach (Instruction branch in instruction.Branches)
                    {
                        if (branch.Condition == null || checkCondition(branch.Condition))
                        { action = branch; break; }
                    }
                    if (action == null)
                    { messages.Add("Condition fausse : aucun mouvement."); continue; }
                }
                if (!move(action.Direction))
                {
                    messages.Add("Ligne " + action.Line + ": déplacement impossible, sélection absente ou défi terminé. Programme arrêté.");
                    break;
                }
                messages.Add("La tuile se déplace vers " + action.Direction + ".");
            }
            return string.Join(Environment.NewLine, messages);
        }

        private static void SkipEmpty(string[] lines, ref int i)
        { while (i < lines.Length && lines[i].Trim().Length == 0) i++; }

        private static string ReadCommand(string[] lines, int i)
        {
            string command = i < lines.Length ? lines[i].Trim() : "";
            switch (command)
            {
                case "aller haut": return "haut";
                case "aller bas": return "bas";
                case "aller gauche": return "gauche";
                case "aller droite": return "droite";
                default: throw Error(i, "utilise aller haut, aller bas, aller gauche ou aller droite.");
            }
        }

        private static void CheckCondition(string condition, int i)
        {
            switch (condition)
            {
                case "haut_possible":
                case "bas_possible":
                case "gauche_possible":
                case "droite_possible":
                case "vide_en_haut":
                case "vide_en_bas":
                case "vide_a_gauche":
                case "vide_a_droite": return;
                default: throw Error(i, "condition inconnue : " + condition + ".");
            }
        }

        private static FormatException Error(int i, string message)
        { return new FormatException("Ligne " + (i + 1) + " : " + message); }
    }
}

