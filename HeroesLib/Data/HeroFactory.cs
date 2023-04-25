using HeroWars.HeroesLib.Utility;

namespace HeroWars.HeroesLib.Data
{
    public class HeroFactory
    {
        private Randomizer _randomizer = new Randomizer();

        public static Hero CreateHero(int id, HeroType type)
        {
            switch (type)
            {
                case HeroType.Archer:
                    return new Archer() { Id = id };
                case HeroType.HorseMan:
                    return new HorseMan() { Id = id };
                case HeroType.SwordsMan:
                    return new SwordsMan() { Id = id };
            }

            throw new Exception("Invalid hero type!");
        }

        public Hero CreateRandomHero(int id)
        {
            List<HeroType> heroTypes = Enum.GetValues<HeroType>().ToList();

            return HeroFactory.CreateHero(id, _randomizer.GetRandomItem(heroTypes));
        }

        public List<Hero> CreateRandomHeroes(int count)
        {
            var heroes = new List<Hero>();

            for (int i = 1; i < count + 1; i++)
            {
                heroes.Add(CreateRandomHero(i));
            }

            return heroes;
        }
    }
}
