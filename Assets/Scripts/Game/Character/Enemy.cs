using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Game.Spawner;
using Photon.Pun;
using Photon.Scripts.Managers;
using ScriptableObjects;
using UnityEngine;

namespace Game.Character
{
    public class Enemy : MonoBehaviourPun, IHealth
    {
        public float speed;
        [SerializeField] private byte health;
        [SerializeField] private Animator animator;
        [SerializeField] private EnemyData enemyData;
        [SerializeField] private Collectible.Collectible collectible;
        private Dictionary<Player.Player, Transform> _playersDict = new Dictionary<Player.Player, Transform>();
        private Player.Player _nearestPlayer;
        private byte _startHealth;
        private byte _score;
        private static readonly int IsDamaging = Animator.StringToHash("isDamaging");

        public byte Health
        {
            get => health;
            set => health = value;
        }

        private void Start()
        {
            _startHealth = health;
            
            var playerInGame = new List<Player.Player>();
            playerInGame.AddRange(new[] {FindObjectOfType<Player.Player>()});
            foreach (var player in playerInGame.Where(player => player))
            {
                _playersDict.Add(player, player.transform);
            }
            GetData();
            SetData();
        }

        private void GetData()
        {
            speed = enemyData.Speed;
            health = enemyData.Health;
            _score = enemyData.score;
        }

        private void SetData()
        {
            collectible.score = _score;
        }
        
        public void TakeDamage(byte damage, Photon.Realtime.Player player)
        {
            health -= damage;
            StartCoroutine(PlayDamageAnimation());
            if (health > _startHealth)
            {
                health = 0;
            }
            if (!IsDead()) return;
            collectible.player = player;
            ConnectPhotonManager.ME.DestroyObject(gameObject);
            SpawnEnemy.DecreaseEnemyCount();
        }

        private IEnumerator PlayDamageAnimation()
        {
            animator.SetBool(IsDamaging, true);
            yield return new WaitForSeconds(0.2f);
            animator.SetBool(IsDamaging, false);
        }

        public bool IsDead()
        {
            return health <= 0;
        }
        
    }
}