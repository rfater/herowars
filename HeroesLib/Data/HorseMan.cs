namespace HeroWars.HeroesLib.Data
{
    public class HorseMan : Hero
    {
        public HorseMan()
        {
            Type = HeroType.HorseMan;
            MaxHealth = 150;

            InitDefaultProperties();
        }
    }
}
