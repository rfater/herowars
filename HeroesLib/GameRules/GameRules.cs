using HeroesLib.Interfaces;
using HeroWars.HeroesLib.Data;
using HeroWars.HeroesLib.GameRules.Attack;

namespace HeroWars.HeroesLib.GameRules
{
    public class GameRules : IGameRules
    {
        private List<AttackRule> _attackRules = new List<AttackRule>();

        public GameRules()
        {
            _attackRules.Add(new AttackRule
            {
                Attacker = HeroType.Archer,
                Defender = HeroType.HorseMan,
                AttackResult = new DefenderDiesInPercent { Percentage = 40 }
            });

            _attackRules.Add(new AttackRule
            {
                Attacker = HeroType.Archer,
                Defender = HeroType.SwordsMan,
                AttackResult = new DefenderDies()
            });

            _attackRules.Add(new AttackRule
            {
                Attacker = HeroType.Archer,
                Defender = HeroType.Archer,
                AttackResult = new DefenderDies()
            });

            _attackRules.Add(new AttackRule
            {
                Attacker = HeroType.SwordsMan,
                Defender = HeroType.HorseMan,
                AttackResult = new NothingHappens()
            });

            _attackRules.Add(new AttackRule
            {
                Attacker = HeroType.SwordsMan,
                Defender = HeroType.SwordsMan,
                AttackResult = new DefenderDies()
            });

            _attackRules.Add(new AttackRule
            {
                Attacker = HeroType.SwordsMan,
                Defender = HeroType.Archer,
                AttackResult = new DefenderDies()
            });

            _attackRules.Add(new AttackRule
            {
                Attacker = HeroType.HorseMan,
                Defender = HeroType.HorseMan,
                AttackResult = new DefenderDies()
            });

            _attackRules.Add(new AttackRule
            {
                Attacker = HeroType.HorseMan,
                Defender = HeroType.SwordsMan,
                AttackResult = new AttackerDies()
            });

            _attackRules.Add(new AttackRule
            {
                Attacker = HeroType.HorseMan,
                Defender = HeroType.Archer,
                AttackResult = new DefenderDies()
            });
        }

        public AttackRule GetAttackRule(HeroType attacker, HeroType defender)
        {
            var rule = _attackRules.FirstOrDefault(rule => rule.Attacker == attacker && rule.Defender == defender);

            if(rule == null)
            {
                throw new Exception($"AttackRule not found! (attacker: {attacker}, defender: {defender}");
            }

            return rule;
        }
    }
}
