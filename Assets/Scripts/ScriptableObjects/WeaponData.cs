using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Data/WeaponData")]
    public class WeaponData : ScriptableObject
    {
        public byte maxAmmunition;
        public AmmunitionData AmmunitionData;
    }
}