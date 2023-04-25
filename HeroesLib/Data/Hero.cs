namespace HeroWars.HeroesLib.Data
{
    public abstract class Hero
    {
        public int Id { get; init; }
        public HeroType Type { get; init; }
        public int CurrentHealth { get; set; }
        public int MaxHealth { get; init; }

        protected void InitDefaultProperties()
        {
            CurrentHealth = MaxHealth;
        }
    }
}
