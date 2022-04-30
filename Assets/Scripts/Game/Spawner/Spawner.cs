using Photon.Scripts.Managers;
using UnityEngine;
using Unity.Mathematics;


namespace Game.Spawner
{
    public abstract class Spawner : MonoBehaviour
    {
        protected virtual GameObject ObjToSpawn
        {
            get;
            set;
        }
        protected virtual Vector2 randomPosition
        {
            get;
            set;
        }

        protected void Spawn()
        {
            ConnectPhotonManager.ME.InstantiateObject(ObjToSpawn.name, randomPosition, quaternion.identity);
        }
        
    }
}