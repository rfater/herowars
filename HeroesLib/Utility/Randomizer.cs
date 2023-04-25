using HeroesLib.Interfaces;

namespace HeroWars.HeroesLib.Utility
{
    public class Randomizer : IRandomizer
    {
        private Random _random = new Random();


        public T GetRandomItem<T>(List<T> items)
        {
            return items[_random.Next(items.Count)];
        }

        public int GetRandomPercentage()
        {
            return _random.Next(1, 101);
        }
    }
}
