using Photon.Scripts.Managers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Spawner
{
    public class SpawnLootBox : Spawner
    {
        [SerializeField] private GameObject box;
        [SerializeField] protected float minX, minY, maxX, maxY;
        [SerializeField] protected float startTimeBtwSpawns;
        private float _timeBtwSpawns;
        private int _maxBox;
        private static int _boxCount;
        private int _maxBoxMultiplier;
        protected override Vector2 randomPosition => new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        protected override GameObject ObjToSpawn
        {
            get => box;
            set => box = value;
        }

        private void Update()
        {
            if (!ConnectPhotonManager.ME.AllPlayerJoined()) return;
            if (ReachedMaxBoxCount()) return;
            
            if (_timeBtwSpawns <= 0)
            {
                Spawn();
                _timeBtwSpawns = startTimeBtwSpawns;
                _boxCount++;
            }
            else
            {
                _timeBtwSpawns -= Time.deltaTime;
            }
        }

        private void Start()
        {
            _maxBoxMultiplier = ConnectPhotonManager.ME.PlayersInRoomCount();
            _timeBtwSpawns = startTimeBtwSpawns;
            _maxBox = _maxBoxMultiplier * 15;
            Spawn();
        }
        
        private bool ReachedMaxBoxCount()
        {
            return _boxCount >= _maxBox;
        }
        
        public static void DecreaseBoxCount()
        {
            _boxCount--;
        }
    }
}