using UnityEngine;

namespace Assets.Code.Structure
{
    public class Game : MonoBehaviour
    {
        /// <summary>
        /// The game context.
        /// A pointer to the currently active game (so that we don't have to use something slow like "Find").
        /// </summary>
        public static Game Ctx;

        /// <summary>
        /// The class that handles serialization/deserialization
        /// </summary>
        public static SaveLoadManager Saveload;

        // 
        // all of the things that we can about saving/loading
        public static ScoreManager Score;
        public static Player Player;
        public static BulletManager Bullets;


        internal void Start () {
            Ctx = this;
            Score = GameObject.Find("ScoreText").GetComponent<ScoreManager>();
            Player = GameObject.Find("Player").GetComponent<Player>();
           
            Bullets = new BulletManager(GameObject.Find("Bullets").transform);

        }

        public void LoadData (SaveLoadManager.SaveData data) {
            Score.OnLoad(data.Score);
            Player.OnLoad(data.Player);
            Bullets.OnLoad(data.Bullets);
        }

        private static bool IsMac () {
            return Application.platform == RuntimePlatform.OSXEditor ||
                   Application.platform == RuntimePlatform.OSXPlayer;
        }
    }
}