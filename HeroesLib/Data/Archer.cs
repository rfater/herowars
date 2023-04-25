namespace HeroWars.HeroesLib.Data
{
    public class Archer : Hero
    {
        public Archer()
        {
            Type = HeroType.Archer;
            MaxHealth = 100;

            InitDefaultProperties();
        }
    }
}
