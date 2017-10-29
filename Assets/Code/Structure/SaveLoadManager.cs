using System.IO;
using System.Xml.Serialization;
using UnityEngine;

namespace Assets.Code.Structure
{
    /// <inheritdoc><cref></cref>
    /// </inheritdoc>
    /// <summary>
    /// Save/Load manager. Handles serialization/deserialization of all of the game's "stuff."
    /// </summary>
    public class SaveLoadManager
    {
        private const string PathExt = "/save";
        private readonly string _path;

        public SaveLoadManager () {
            _path = Application.persistentDataPath + PathExt;
            
        }

        /// <summary>
        /// Manually serialize out all of the GameData class and its information
        /// </summary>
        public void Save () {
            Debug.Log("Saving file: " + _path);

            using (var file = File.Create(_path)) {
                var data = new SaveData();
                var writer = new XmlSerializer(typeof(SaveData));
                writer.Serialize(file, data);
                file.Close();
            }
        }

        public void Load () {
            Debug.Log("Loading file: " + _path);
            if (!File.Exists(_path)) { return; } // how can our load be real if our file isn't real  --I hate you so much

            using (var file = File.Open(_path, FileMode.Open)) {
                var reader = new XmlSerializer(typeof(SaveData));
                var data = reader.Deserialize(file) as SaveData;
                file.Close();
                Game.Ctx.LoadData(data);
            }
        }

        public class SaveData
        {
            public ScoreData Score;
            public PlayerGameData Player;
            public BulletsData Bullets;

            public SaveData () {
                Score = Game.Score.OnSave() as ScoreData;
                Player = Game.Player.OnSave() as PlayerGameData;
                Bullets = Game.Bullets.OnSave() as BulletsData;
            }
        }
    }

    public abstract class GameData { }

    public interface ISaveLoad
    {
        GameData OnSave ();
        void OnLoad (GameData data);
    }
}