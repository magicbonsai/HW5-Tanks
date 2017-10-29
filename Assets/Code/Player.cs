using System;
using Assets.Code.Structure;
using UnityEngine;
//using XInputDotNetPure;

namespace Assets.Code
{

   
    /// <summary>
    /// Player controller class
    /// </summary>
    // ReSharper disable once ClassNeverInstantiated.Global
    public class Player : MonoBehaviour, ISaveLoad
    {
        /*
        PlayerIndex playerIndex;
        bool indexSet = false;
        public GamePadState state;
        public GamePadState prevState;
        public int playerNum;
        public bool isActive;
        */
        private static string _fireaxis;
        private Rigidbody2D _rb;
        private Gun _gun;

        // ReSharper disable once UnusedMember.Global
        internal void Start () {
            /*
            PlayerIndex testIndex = (PlayerIndex)(playerNum - 1);
            GamePadState testState = GamePad.GetState(testIndex);
            if (testState.IsConnected)
            {
                Debug.Log(string.Format("GamePad found {0}", testIndex));
                playerIndex = testIndex;
                indexSet = true;
            }
            */
            _rb = GetComponent<Rigidbody2D>();
            _gun = GetComponent<Gun>();

            _fireaxis = Platform.GetFireAxis();
        }

        // ReSharper disable once UnusedMember.Global
        internal void Update () {
            HandleInput();
        }

        /// <summary>
        /// Check the controller for player inputs and respond accordingly.
        /// </summary>
        private void HandleInput () {
            // TODO fill me in
            Turn(Input.GetAxis("Horizontal"));
            Thrust(Input.GetAxis("Vertical"));
            if (Mathf.Abs(Input.GetAxis(_fireaxis)) > 0){
                Debug.Log(string.Format("Fired"));
                Fire();
            }
        }

        private void Turn (float direction) {
            if (Mathf.Abs(direction) < 0.2f) { return; }
            _rb.AddTorque(direction * -0.07f);
        }

        private void Thrust (float intensity) {
            if (Mathf.Abs(intensity) < 0.2f) { return; }
            _rb.AddRelativeForce(Vector2.up * intensity);
        }

        private void Fire () {
            _gun.Fire();
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
