using Game.Character.EnemyAI;
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
        private int _maxEnemies;
        private readonly int _maxEnemiesMultiplier = ConnectPhotonManager.ME.PlayersInRoom();
        private static int _enemyCount;
        private float _timeBtwSpawns;
        private int _randomIndex;

        protected override GameObject ObjToSpawn
        {
            get => enemy;
            set => enemy = value;
        }


        protected override Vector2 randomPosition => spawnPoints[_randomIndex].position;

        private void Start()
        {
        _randomIndex = Random.Range(0, spawnPoints.Length);
        _timeBtwSpawns = startTimeBtwSpawns;
        _maxEnemies = spawnPoints.Length * _maxEnemiesMultiplier;
        ObjToSpawn.GetComponent<EnemyBT>().waypoints =
            spawnPoints[_randomIndex].GetComponent<SpawnerWayPoints>().waypoints;
        }

        private void Update()
        {
            if (!ConnectPhotonManager.ME.AllPlayerJoined()) return;
            if (ReachedMaxEnemies()) return;
            
            if (_timeBtwSpawns <= 0)
            {
                _randomIndex = Random.Range(0, spawnPoints.Length);
                ObjToSpawn.GetComponent<EnemyBT>().waypoints =
                    spawnPoints[_randomIndex].GetComponent<SpawnerWayPoints>().waypoints;
                Spawn();
                _timeBtwSpawns = startTimeBtwSpawns;
                _enemyCount++;
            }
            else
            {
                _timeBtwSpawns -= Time.deltaTime;
            }
        }

        private bool ReachedMaxEnemies()
        {
            return _enemyCount >= _maxEnemies;
        }

        public static void DecreaseEnemyCount()
        {
            _enemyCount--;
        }

    }
}