using System.Collections.Generic;
using DG.Tweening;
using Photon.Scripts.Managers;
using ScriptableObjects;
using UnityEngine;

namespace Game.Character
{
    public class Enemy : MonoBehaviour, IHealth
    {
        [SerializeField] private CharacterData characterData;
        [SerializeField] private float speed;
        [SerializeField] private byte health;
        private Dictionary<Player.Player, Transform> _playersDict = new Dictionary<Player.Player, Transform>();
        private Player.Player _nearestPlayer;
        private byte startHealth;
        public byte Health
        {
            get => health;
            set => health = value;
        }

        private void Start()
        {
            startHealth = health;
            var playerInGame = new List<Player.Player>();
            playerInGame.AddRange(new[] {FindObjectOfType<Player.Player>()});
            foreach (var player in playerInGame)
            {
                _playersDict.Add(player, player.transform);
            }
            GetData();
            AnimateOnSpawn();
        }
        
        private void GetData()
        {
            speed = characterData.Speed;
            health = characterData.Health;
        }

        private void AnimateOnSpawn()
        {
            transform.localScale = Vector3.zero;
            transform.DOScale(new Vector3(2,2,1), 3f).SetEase(Ease.OutElastic);
        }
        
        public void TakeDamage(byte damage)
        {
            health -= damage;
            if (health > startHealth)
            {
                health = 0;
            }
            if (IsDead())
            {
                ConnectPhotonManager.ME.DestroyObject(gameObject);
                Spawner.SpawnEnemy.DecreaseEnemyCount();
            }
        }

        public bool IsDead()
        {
            return health <= 0;
        }
        
    }
}