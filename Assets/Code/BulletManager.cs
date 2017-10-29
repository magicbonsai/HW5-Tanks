using System;
using System.Collections.Generic;
using Assets.Code.Structure;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Code
{
    /// <summary>
    /// Bullet manager for spawning and tracking all of the game's bullets
    /// </summary>
    public class BulletManager : ISaveLoad
    {
        private readonly Transform _holder;
        private Rigidbody2D _reggie;
        /// <summary>
        /// Bullet prefab. Use GameObject.Instantiate with this to make a new bullet.
        /// </summary>
        private readonly Object _bullet;

        public BulletManager (Transform holder) {
            _holder = holder;
            _bullet = Resources.Load("Bullet");
        }

        // TODO fill me in
        public void ForceSpawn (Vector2 pos, Quaternion rotation, Vector2 velocity, float deathtime) {
            //Object.Instantiate(_bullet, pos, rotation, _holder);
            GameObject _holder = (GameObject)Object.Instantiate(_bullet, pos, rotation);
            _holder.GetComponent<Bullet>().Initialize(velocity, deathtime);
        }

        #region saveload
        
        // TODO fill me in
        public GameData OnSave () {
            return null;
        }

        // TODO fill me in
        public void OnLoad (GameData data) {
            
        }

        #endregion

    }

    /// <summary>
    /// Save data for all bullets in game
    /// </summary>
    public class BulletsData : GameData
    {
        public List<BulletData> Bullets;
    }

    /// <summary>
    /// Save data for a single bullet
    /// </summary>
    public class BulletData
    {
        public Vector2 Pos;
        public Vector2 Velocity;
        public float Rotation;
    }
}