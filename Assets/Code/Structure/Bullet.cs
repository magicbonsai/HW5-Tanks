using System;
using UnityEngine;

namespace Assets.Code.Structure
{
    public class Bullet : MonoBehaviour
    {
        public const float Lifetime = 7.5f; // bullets last this long
        private float _deathtime;

        public void Initialize (Vector2 velocity, float deathtime) {
            GetComponent<Rigidbody2D>().velocity = velocity;
            _deathtime = deathtime;
        }

        internal void Update () {
            if (Time.time > _deathtime) { Die(); }
        }

        internal void OnCollisionEnter2D(Collision2D other) {
            Die(); // we die no matter what :(
        }

        private void Die () {
            Destroy(gameObject);
        }

    }
}
