using HeroWars.HeroesLib.Data;

namespace HeroWars.HeroesLib.Utility
{
    public class Presentator
    {
        public void ShowTournamentStartInfo()
        {
            Console.WriteLine("Welcome to the Hero Arena!");
            Console.WriteLine();
        }

        public void ShowHeroesInfo(List<Hero> heroes)
        {
            Console.WriteLine("Hero list:");

            heroes.ForEach(hero =>
            {
                Console.WriteLine($"Hero id: {hero.Id}, type: {hero.Type}, health: {hero.CurrentHealth}");
            });

            Console.WriteLine();
        }

        public void ShowDuelInfoBeforeFight(int roundNumber, DuelData duelData)
        {
            Console.WriteLine($"{roundNumber}. round started:");
            Console.WriteLine($"Attacker is: id: {duelData.Attacker.Id}, type: {duelData.Attacker.Type}, health: {duelData.Attacker.CurrentHealth}");
            Console.WriteLine($"Defender is: id: {duelData.Defender.Id}, type: {duelData.Defender.Type}, health: {duelData.Defender.CurrentHealth}");
        }

        public void ShowDuelInfoAfterFight(int roundNumber, DuelData duelData)
        {
            Console.WriteLine($"Attacker's health after attack : {duelData.Attacker.CurrentHealth}");
            Console.WriteLine($"Defender's health after attack : {duelData.Defender.CurrentHealth}");

            if (duelData.Attacker.CurrentHealth == 0)
            {
                Console.WriteLine("Attacker died!");
            }

            if (duelData.Defender.CurrentHealth == 0)
            {
                Console.WriteLine("Defender died!");
            }

            //Console.WriteLine();
            Console.ReadLine();
        }

        public void ShowTournamentResultInfo(int roundNumber, List<Hero> heroes)
        {
            if (heroes.Count == 1)
            {
                Console.WriteLine($"The winner is: id: {heroes.First().Id}, type: {heroes.First().Type}, health: {heroes.First().CurrentHealth}");
            }
            else
            {
                Console.WriteLine("All heroes died in the arena!");
            }
        }
    }
}
