using UnityEngine;

namespace Game.Character.Weapon
{
    public class Ammunition : MonoBehaviour
    {
        [SerializeField] private int speed;
        [SerializeField] private new Rigidbody2D rigidbody2D;
                
        private static int _enemyLayerMask;

        public void Move(Vector2 direction)
        {
            rigidbody2D.velocity = direction * speed * Time.deltaTime;
            _enemyLayerMask = LayerMask.NameToLayer("Enemy");
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.layer != _enemyLayerMask) return;
            col.GetComponent<Enemy>().TakeDamage();
        }

    }
}