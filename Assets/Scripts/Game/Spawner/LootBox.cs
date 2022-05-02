using System.Collections;
using Game.Character;
using Photon.Pun;
using Photon.Scripts.Managers;
using ScriptableObjects;
using UnityEngine;

namespace Game.Spawner
{
    public class LootBox : Spawner, IHealth
    {
        public Collectible.Collectible collectible;
        [SerializeField] private byte health;
        [SerializeField] private PhotonView view;
        [SerializeField] private LootBoxData lootBoxData;
        private byte _startHealth;
        private byte score;
        private GameObject _lootToSpawn;

        public byte Health
        {
            get => health;
            set => health = value;
        }
        
        protected override GameObject ObjToSpawn
        {
            get => _lootToSpawn;
            set => _lootToSpawn = value;
        }

        private void Start()
        {
            GetData();
            _startHealth = health;
            SetData();
        }
        
        private void GetData()
        {
            health = lootBoxData.health;
            score = lootBoxData.score;
        }
        
        private void SetData()
        {
            collectible.score = score;
        }

        public void TakeDamage(byte amount = 1)
        {
            health -= amount;
            if (health > _startHealth)
            {
                health = 0;
            }
            if (IsDead())
            {
                StartCoroutine(DestroyCurrentGO());
                SpawnLootBox.DecreaseBoxCount();
            }
        }

        public bool IsDead()
        {
            return health <= 0;
        }

        private IEnumerator DestroyCurrentGO()
        {
            yield return new WaitForFixedUpdate();
            ConnectPhotonManager.ME.DestroyObject(gameObject);
        }
    }
}