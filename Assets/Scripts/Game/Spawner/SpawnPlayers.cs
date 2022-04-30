using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Spawner
{
    public class SpawnPlayers : Spawner
    {
        [SerializeField] private GameObject player;

        [SerializeField] protected float minX, minY, maxX, maxY;
        protected override GameObject ObjToSpawn
        {
            get => player;
            set => player = value;
        }
        protected override Vector2 randomPosition => new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));

        private void Start()
        {
            Spawn();
        }
    }
}