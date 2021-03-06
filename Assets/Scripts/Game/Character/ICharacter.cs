using UnityEngine;

namespace Game.Character
{
    public interface ICharacter
    {
        float Speed
        {
            get;
            set;
        }
        void Update();

        void Move(Vector2 translation);
    }
}