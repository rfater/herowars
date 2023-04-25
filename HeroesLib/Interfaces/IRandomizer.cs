namespace HeroesLib.Interfaces
{
    public interface IRandomizer
    {
        public T GetRandomItem<T>(List<T> items);
        public int GetRandomPercentage();
    }
}
