using HeroesLib.Interfaces;
using HeroWars.HeroesLib.Data;
using HeroWars.HeroesLib.GameRules.Attack;
using HeroWars.HeroesLib.Utility;

namespace HeroWars.HeroesLib.GameRules
{
    public class GameRuleManager
    {
        private IGameRules _gameRules = new GameRules();
        private IRandomizer _randomizer = new Randomizer();

        public GameRuleManager()
        {
            
        }

        public GameRuleManager(IGameRules gameRules, IRandomizer randomizer)
        {
            _gameRules = gameRules;
            _randomizer = randomizer;
        }

        public DuelData SelectAttackerAndDefender(List<Hero> heroPool)
        {
            var duelData = new DuelData();

            duelData.Attacker = _randomizer.GetRandomItem(heroPool);
            duelData.AttackerHealth = duelData.Attacker.CurrentHealth;
            heroPool.Remove(duelData.Attacker);

            duelData.Defender = _randomizer.GetRandomItem(heroPool);
            duelData.DefenderHealth = duelData.Defender.CurrentHealth;
            heroPool.Remove(duelData.Defender);

            return duelData;
        }

        public void RestNotFightingHeroes(List<Hero> heroPool)
        {
            const int restingHealthIncrease = 10;

            heroPool.ForEach(hero =>
            {
                hero.CurrentHealth = (hero.CurrentHealth + restingHealthIncrease < hero.MaxHealth ? hero.CurrentHealth + restingHealthIncrease : hero.MaxHealth);
            });
        }

        public void StartDuel(DuelData duelData)
        {
            var attackRule = _gameRules.GetAttackRule(duelData.Attacker.Type, duelData.Defender.Type);

            switch(attackRule.AttackResult) 
            {
                case AttackerDies attackerDies:
                    duelData.Attacker.CurrentHealth = 0;
                    return;
                case DefenderDies defenderDies:
                    duelData.Defender.CurrentHealth = 0;
                    return;
                case DefenderDiesInPercent defenderDiesInPercent:
                    if (_randomizer.GetRandomPercentage() <= defenderDiesInPercent.Percentage)
                    {
                        duelData.Defender.CurrentHealth = 0;
                        return;
                    }
                    return;
                case NothingHappens nothingHappens:
                    return;
            }
        }

        public void ExhaustDuelists(DuelData duelData)
        {
            duelData.Attacker.CurrentHealth /= 2;
            duelData.Defender.CurrentHealth /= 2;

            if (duelData.Attacker.CurrentHealth < duelData.Attacker.MaxHealth / 4)
            {
                duelData.Attacker.CurrentHealth = 0;
            }

            if (duelData.Defender.CurrentHealth < duelData.Defender.MaxHealth / 4)
            {
                duelData.Defender.CurrentHealth = 0;
            }
        }

        public void AddAliveDuelistsToHeroPool(DuelData duelData, List<Hero> heroPool)
        {
            AddLiveHeroToPool(duelData.Attacker, heroPool);
            AddLiveHeroToPool(duelData.Defender, heroPool);
        }

        public void AddLiveHeroToPool(Hero hero, List<Hero> heroPool)
        {
            if(hero.CurrentHealth > 0)
            {
                heroPool.Add(hero);
            }
        }

        public bool HeroesCanFightEachOther(List<Hero> heroPool)
        {
            return heroPool.Count > 1;
        }
    }
}
