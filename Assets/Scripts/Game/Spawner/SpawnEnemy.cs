using Photon.Scripts.Managers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Spawner
{
    public class SpawnEnemy : Spawner
    {
        [SerializeField] private GameObject enemy;
        [SerializeField] private Transform[] spawnPoints;
        [SerializeField] protected float startTimeBtwSpawns;
        private float timeBtwSpawns;

        protected override GameObject ObjToSpawn
        {
            get => enemy;
            set => enemy = value;
        }

        protected override Vector2 randomPosition => spawnPoints[Random.Range(0, spawnPoints.Length)].position;

        private void Start()
        {
            timeBtwSpawns = startTimeBtwSpawns;
        }

        private void Update()
        {
            if (!ConnectPhotonManager.ME.AllPlayerJoined()) return;

            if (timeBtwSpawns <= 0)
            {
                Spawn();
                timeBtwSpawns = startTimeBtwSpawns;
            }
            else
            {
                timeBtwSpawns -= Time.deltaTime;
            }
        }
    }
}