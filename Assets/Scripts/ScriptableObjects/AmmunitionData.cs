using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Ammunition", menuName = "Data/AmmunitionData")]
    public class AmmunitionData : ScriptableObject
    {
        public byte Speed;
        public byte SpeedMultiplier;
        public byte Damage;
        public Color Color;
        public byte DestroyTime;
        public byte SizeMultiplier;
    }
}