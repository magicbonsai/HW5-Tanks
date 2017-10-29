using System;
using UnityEngine;

namespace Assets.Code.Structure
{
    public class Bullet : MonoBehaviour
    {
        public const float Lifetime = 7.5f; // bullets last this long
        private float _deathtime;

        public void Initialize (Vector2 velocity, float deathtime, int color) {
            GetComponent<Rigidbody2D>().velocity = velocity;
            Renderer r = GetComponent<Renderer>();
            if(color == 1)
            {
                r.material.color = Color.red;
            }
            else
            {
                r.material.color = Color.blue;
            }
            _deathtime = deathtime;
        }

        internal void Update () {
            if (Time.time > _deathtime) { Die(); }
        }

        internal void OnCollisionEnter2D(Collision2D other) {
            Color c = GetComponent<Renderer>().material.color;
            if (other.gameObject.GetComponent<Bullet>())
            {

            }
            else
            {
                Die();
            }
            ;
        }

        private void Die () {
            Destroy(gameObject);
        }

    }
}
