using HeroWars.HeroesLib.Data;

namespace HeroWars.HeroesLib.Utility
{
    public class Logger
    {
        private readonly List<string> _logItems = new List<string>();

        public Logger() { }

        public void LogDuelDataAfterFight(int roundNumber, DuelData duelData)
        {
            _logItems.Add($"Round: {roundNumber}.");
            _logItems.Add($"Hero (id: {duelData.Attacker.Id}) attacked another hero (id: {duelData.Defender.Id})");
            _logItems.Add($"Attacker's health changed from {duelData.AttackerHealth} to {duelData.Attacker.CurrentHealth}");
            _logItems.Add($"Defender's health changed from {duelData.DefenderHealth} to {duelData.Defender.CurrentHealth}");
        }
    }
}
