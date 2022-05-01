using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Character", menuName = "Data/CharacterData")]
    public class CharacterData : ScriptableObject
    {
    public float Speed;
    public byte Health;
    }
}