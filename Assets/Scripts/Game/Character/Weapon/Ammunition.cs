using System.Collections;
using Photon.Scripts.Managers;
using ScriptableObjects;
using UnityEngine;

namespace Game.Character.Weapon
{
    public class Ammunition : MonoBehaviour
    {
        public AmmunitionData ammunitionData;
        [SerializeField] private Rigidbody2D rigidbody2D;
        [SerializeField] private SpriteRenderer spriteRenderer;        
        [SerializeField] private byte speed;
        [SerializeField] private byte speedMultiplier;
        [SerializeField]  private byte damage;
        [SerializeField]  private byte destroyTime;
        [SerializeField]  private byte sizeMultiplier;
        private static int _enemyLayerMask;

        private void OnEnable()
        {
            GetData();
            _enemyLayerMask = LayerMask.NameToLayer("Enemy");
        }

        private void GetData()
        {
            speed = ammunitionData.Speed;
            speedMultiplier = ammunitionData.SpeedMultiplier;
            damage = ammunitionData.Damage;
            spriteRenderer.color = ammunitionData.Color;
            destroyTime = ammunitionData.DestroyTime;
            sizeMultiplier = ammunitionData.SizeMultiplier;
        }

        public void Move(Vector2 direction)
        {
            int currentSpeed = speed * speedMultiplier;
            rigidbody2D.velocity = direction * currentSpeed * Time.deltaTime;
            StartCoroutine(DestroyUnused());
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.layer != _enemyLayerMask) return;
            col.GetComponent<Enemy>().TakeDamage(damage);
        }

        private IEnumerator DestroyUnused()
        {
            yield return new WaitForSeconds(destroyTime);
            ConnectPhotonManager.ME.DestroyObject(gameObject);
        }
    }
}