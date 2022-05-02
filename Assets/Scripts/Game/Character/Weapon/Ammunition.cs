using System.Collections;
using Photon.Scripts.Managers;
using ScriptableObjects;
using UnityEngine;

namespace Game.Character.Weapon
{
    public class Ammunition : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        public Rigidbody2D rigidbody2D;
        public AmmunitionData ammunitionData;
        [HideInInspector]public byte damage;
        private byte _speed;
        private byte _speedMultiplier;
        private byte _destroyTime;
        private byte _sizeMultiplier;
        private static int _enemyLayerMask;
        private Photon.Realtime.Player _playerWhoShot;

        private void OnEnable()
        {
            _enemyLayerMask = LayerMask.NameToLayer("Enemy");

            GetData();
            transform.localScale = _sizeMultiplier * transform.localScale;
        }

        private void GetData()
        {
            _speed = ammunitionData.Speed;
            _speedMultiplier = ammunitionData.SpeedMultiplier;
            damage = ammunitionData.Damage;
            spriteRenderer.color = ammunitionData.Color;
            _destroyTime = ammunitionData.DestroyTime;
            _sizeMultiplier = ammunitionData.SizeMultiplier;
        }

        public void Move(Vector2 direction, Photon.Realtime.Player player)
        {
            _playerWhoShot = player;
            int currentSpeed = _speed * _speedMultiplier;
            rigidbody2D.velocity = direction * currentSpeed * Time.deltaTime;
            StartCoroutine(DestroyUnused());
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.layer != _enemyLayerMask) return;
            
            if (col.gameObject.layer == _enemyLayerMask)
            {
                col.GetComponent<Enemy>().TakeDamage(damage, _playerWhoShot);
                ConnectPhotonManager.ME.DestroyObject(gameObject);
            }
        }

        private IEnumerator DestroyUnused()
        {
            yield return new WaitForSeconds(_destroyTime);
            ConnectPhotonManager.ME.DestroyObject(gameObject);
        }
    }
}