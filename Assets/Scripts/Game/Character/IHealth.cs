namespace Game.Character
{
    public interface IHealth
    {
        byte Health
        {
            get;
            set;
        }
        bool IsDead();
    }
}