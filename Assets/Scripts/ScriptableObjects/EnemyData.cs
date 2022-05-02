using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Enemy", menuName = "Data/EnemyData")]
    public class EnemyData : CharacterData
    {
        public byte score;
    }
}