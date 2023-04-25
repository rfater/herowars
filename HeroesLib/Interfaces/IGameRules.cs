using HeroWars.HeroesLib.Data;
using HeroWars.HeroesLib.GameRules.Attack;

namespace HeroesLib.Interfaces
{
    public interface IGameRules
    {
        public AttackRule GetAttackRule(HeroType attacker, HeroType defender);
    }
}
