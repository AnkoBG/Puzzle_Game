using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace PuzzleGameLibrary
{
    [Serializable]
    public class Level
    {
        internal List<Figure> placedFigures = new List<Figure>(0);
        internal List<Figure> storedFigures = new List<Figure>(0);
        public Grid Grid { get; private set; }
        public Grid RightGrid { get; private set; }
        public string Name { get; private set;}

        public Level(string name, Grid grid, Grid rightGrid, List<Figure> _placedFigures, List<Figure> _storedFigures)
        {
            placedFigures = _placedFigures;
            storedFigures = _storedFigures;
            Grid = grid;
            RightGrid = rightGrid;
            Name = name;
        }

        public Level(string levelPath)
        {
            Level newLevel = DeserializeLevel(levelPath);
            placedFigures = newLevel.placedFigures;
            storedFigures = newLevel.storedFigures;
            Grid = newLevel.Grid;
            RightGrid = newLevel.RightGrid;
            Name = newLevel.Name;
        }

        public void SerializeLevel()
        {
            using (Stream stream = File.Open(Name + ".bin", FileMode.Create))
            {
                BinaryFormatter bin = new BinaryFormatter();
                bin.Serialize(stream, this);
            }
        }

        public Level DeserializeLevel(string fileName)
        {
            using (Stream stream = File.Open(fileName, FileMode.Open))
            {
                BinaryFormatter bin = new BinaryFormatter();

                Level newLevel = (Level)bin.Deserialize(stream);
                return newLevel;
            }
        }
    }
}
