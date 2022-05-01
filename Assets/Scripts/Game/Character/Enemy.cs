using System.Collections.Generic;
using DG.Tweening;
using Photon.Scripts.Managers;
using UnityEngine;

namespace Game.Character
{
    public class Enemy : MonoBehaviour, IHealth
    {
        [SerializeField] private float speed;
        [SerializeField] private byte health;
        private Dictionary<Player.Player, Transform> playersDict = new Dictionary<Player.Player, Transform>();
        private Player.Player nearestPlayer;
        
        public byte Health
        {
            get => health;
            set => health = value;
        }

        public void TakeDamage()
        {
            health--;
            if (IsDead())
            {
                ConnectPhotonManager.ME.DestroyObject(gameObject);
                Spawner.SpawnEnemy.DecreaseEnemyCount();
            }
        }

        public bool IsDead()
        {
            return Health <= 0;
        }


        private void Start()
        {
            var playerInGame = new List<Player.Player>();
            playerInGame.AddRange(new[] {FindObjectOfType<Player.Player>()});
            foreach (var player in playerInGame)
            {
                playersDict.Add(player, player.transform);
            }
            
            AnimateOnSpawn();
        }

        private void AnimateOnSpawn()
        {
            transform.localScale = Vector3.zero;
            transform.DOScale(new Vector3(2,2,1), 3f).SetEase(Ease.OutElastic);
        }
        
    }
}