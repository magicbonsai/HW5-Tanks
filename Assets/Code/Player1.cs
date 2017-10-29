using System.Collections;
using Assets.Code.Structure;
using UnityEngine;

namespace Assets.Code
{
    public class Player1 : MonoBehaviour
    {
        public int playernum;
        private static string _fireaxis;
        private Rigidbody2D _rb;
        private Gun1 _gun;
        public string dHor;
        public string dVer;

        // ReSharper disable once UnusedMember.Global
        internal void Start()
        {

            _rb = GetComponent<Rigidbody2D>();
            _gun = GetComponent<Gun1>();
            _fireaxis = Platform.GetFireAxis() + playernum;
            Debug.Log(string.Format("Player " + playernum + " " + _fireaxis));


        }

        // ReSharper disable once UnusedMember.Global
        internal void Update()
        {
            HandleKInput(); 
           //HandleInput();
        }


        private void HandleKInput()
        {
            kTurn();
            kThrust();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log(string.Format("Fired"));
                Fire();
            }
        }
        /// <summary>
        /// Check the controller for player inputs and respond accordingly.
        /// </summary>
        private void HandleInput()
        {
            // TODO fill me in
            Turn(Input.GetAxis(dHor));
            //Turn(Input.GetAxis(dHor));
            Thrust(Input.GetAxis(dVer));
            if (Mathf.Abs(Input.GetAxis(_fireaxis)) == 1)
            {
                Debug.Log(string.Format("Fired"));
                Fire();
            }

        }

        private void Turn(float direction)
        {
            if (Mathf.Abs(direction) < 0.4f) { return; }
            _rb.AddTorque(direction * -0.07f);
        }

        private void Thrust(float intensity)
        {
            if (Mathf.Abs(intensity) < 0.4f) { return; }
            _rb.AddRelativeForce(Vector2.up * intensity);
        }

        private void kTurn()
        {
            if(Input.GetKeyDown(KeyCode.D))
            {
                _rb.AddTorque(-1 * 0.5f);
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                _rb.AddTorque(1 * 0.5f);
            }
        }
        private void kThrust() {
            if (Input.GetKeyDown(KeyCode.W))
            {
                _rb.AddRelativeForce(Vector2.up * 6);
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                _rb.AddRelativeForce(Vector2.up * -6);

            }
        }


        private void Fire()
        {
            _gun.Fire();
        }

        internal void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.GetComponent<Bullet>())
            {
                Color c = collision.gameObject.GetComponent<Renderer>().material.color;
                if (c == Color.red)
                {
                    Game.Score.AddScore(2);
                    Destroy(collision.gameObject);
                }
                if (c == Color.blue)
                {
                    Game.Score1.AddScore(-1);
                    Destroy(collision.gameObject);

                }
            }
        }

    }


}
