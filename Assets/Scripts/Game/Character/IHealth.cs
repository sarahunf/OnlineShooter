﻿namespace Game.Character
{
    public interface IHealth
    {
        byte Health
        {
            get;
            set;
        }
        void TakeDamage();

        bool IsDead();
    }
}