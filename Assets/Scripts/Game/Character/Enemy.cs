using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Character
{
    public class Enemy : MonoBehaviour, ICharacter, IHealth
    {
        [SerializeField] private float speed;
        [SerializeField] private byte health;
        private Dictionary<Player, Transform> playersDict = new Dictionary<Player, Transform>();
        private Player nearestPlayer;

        public float Speed
        {
            get => speed;
            set => speed = value;
        }
        
        public byte Health
        {
            get => health;
            set => health = value;
        }

        private void Start()
        {
            var playerInGame = new List<Player>();
            playerInGame.AddRange(new[] {FindObjectOfType<Player>()});
            foreach (var player in playerInGame)
            {
                playersDict.Add(player, player.transform);
            }
        }

        public void Update()
        {
            var currentPosition = transform.position;
            var nClosest = playersDict.OrderBy(t => (t.Value.position - currentPosition).sqrMagnitude)
                .FirstOrDefault();
            nearestPlayer = nClosest.Key;
            if (nearestPlayer)
            {
                Move(nearestPlayer.transform.position);
            }
        }

        public void Move(Vector2 translation)
        {
            transform.position = Vector2.MoveTowards(transform.position, translation,
                Speed * Time.deltaTime);
        }

        public void Shoot()
        {
            throw new System.NotImplementedException();
        }

        public bool TakeDamage()
        {
            throw new System.NotImplementedException();
        }

        public void DoDamage()
        {
            throw new System.NotImplementedException();
        }
    }
}