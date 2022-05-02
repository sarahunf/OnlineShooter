using UnityEngine;

namespace ScriptableObjects
{
    
    [CreateAssetMenu(fileName = "LootBox", menuName = "Data/LootBoxData")]
    public class LootBoxData : ScriptableObject
    {
        public byte health;
        public byte score;
    }
}