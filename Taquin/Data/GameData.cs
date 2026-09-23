using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;


namespace Taquin
{

public class GameData
    {
        private string login;
        private int points;
        private int level;
        private int lifes;
        private string date;
        private string file_path;

        public GameData() {
            this.file_path = "taquin_data.bin";
        }
        public GameData(string login, int points, int level, int lifes, string date)
        {
            this.file_path = "taquin_data.bin";
            this.login = login;
            this.points = points;
            this.level = level;
            this.lifes = lifes;
            this.date = date;
        }

        public void SaveToFile()
        {

            using (FileStream fs = new FileStream(file_path, FileMode.Append))
            using (BinaryWriter writer = new BinaryWriter(fs))
            {
                DateTime date = DateTime.Now;
                string date_fr_format = date.ToString("dd MMMM yyyy", new CultureInfo("fr-FR"));
                writer.Write(login);
                writer.Write(points);
                writer.Write(level);
                writer.Write(lifes);
                writer.Write(date_fr_format);

            }
        }

        public int CompareTo(GameData other)
        {
            return other.points.CompareTo(this.points); 
        }

        public List<GameData> LoadAll()
        {

            var gameList = new List<GameData>();

            if (!File.Exists(this.file_path)) return gameList;

            using (FileStream fs = new FileStream(this.file_path, FileMode.Open))
            using (BinaryReader reader = new BinaryReader(fs))
            {
                while (fs.Position < fs.Length)
                {
                    try
                    {
                        string login = reader.ReadString();
                        int points = reader.ReadInt32();
                        int level = reader.ReadInt32();
                        int lifes = reader.ReadInt32();
                        string date = reader.ReadString();
                        gameList.Add(new GameData(login, points, level, lifes, date));
                    }
                    catch (EndOfStreamException)
                    {
                        break;
                    }
                }
            }

            return gameList;
        }
        public string GetLogin() => login;
        public int GetPoints() => points;
        public int GetLevel() => level;
        public int GetLifes() => lifes;

        internal string GateDate() => date;
    }


}
