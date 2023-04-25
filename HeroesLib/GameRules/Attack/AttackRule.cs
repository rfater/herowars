using HeroWars.HeroesLib.Data;

namespace HeroWars.HeroesLib.GameRules.Attack
{
    public record AttackRule
    {
        public HeroType Attacker { get; set; }
        public HeroType Defender { get; set; }

        public AttackResult AttackResult { get; set; }
    }
}