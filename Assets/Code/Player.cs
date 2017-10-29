﻿using System;
using Assets.Code.Structure;
using UnityEngine;


namespace Assets.Code
{

   
    /// <summary>
    /// Player controller class
    /// </summary>
    // ReSharper disable once ClassNeverInstantiated.Global
    public class Player : MonoBehaviour, ISaveLoad
    {
        public int playernum;
        private static string _fireaxis;
        private Rigidbody2D _rb;
        private Gun _gun;
        public string dHor;
        public string dVer;
        
        // ReSharper disable once UnusedMember.Global
        internal void Start () {

            _rb = GetComponent<Rigidbody2D>();
            _gun = GetComponent<Gun>();
            _fireaxis = Platform.GetFireAxis() + playernum;
            Debug.Log(string.Format("Player " + playernum + " " + _fireaxis));
           
            
        }

        // ReSharper disable once UnusedMember.Global
        internal void Update () {
            if (Input.GetButton("StartWindows1"))
            {
                dHor = "Horizontal1";
                dVer = "Vertical1";
            }
            else if (Input.GetButton("StartWindows2"))
            {
                dHor = "Horizontal2";
                dVer = "Vertical2";
            }
            HandleInput();
        }

        /// <summary>
        /// Check the controller for player inputs and respond accordingly.
        /// </summary>
        private void HandleInput () {
            // TODO fill me in
            Turn(Input.GetAxis(dHor));
            Thrust(Input.GetAxis(dVer));
            if (Mathf.Abs(Input.GetAxis(_fireaxis)) == 1){
                Debug.Log(string.Format("Fired"));
                Fire();
            }
        }

        private void Turn (float direction) {
            if (Mathf.Abs(direction) < 0.4f) { return; }
            _rb.AddTorque(direction * -0.07f);
        }

        private void Thrust (float intensity) {
            if (Mathf.Abs(intensity) < 0.4f) { return; }
            _rb.AddRelativeForce(Vector2.up * intensity);
        }

        private void Fire () {
            _gun.Fire();
        }

        internal void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.GetComponent<Bullet>())
            {
                Color c = collision.gameObject.GetComponent<Renderer>().material.color;
                if(c == Color.red)
                {
                    Game.Score.AddScore(-1);
                    Destroy(collision.gameObject);

                }
                if (c == Color.blue)
                {
                    Game.Score1.AddScore(2);
                    Destroy(collision.gameObject);

                }
            }
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

    public class PlayerGameData : GameData
    {
        public Vector2 Pos;
        public Vector2 Velocity;
        public float Rotation;
        public float AngularVelocity; // reaed as DEGREES but stored as RADIANS; COME ON UNITY
    }
}
