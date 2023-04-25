using HeroWars.HeroesLib.Utility;
using HeroWars.HeroesLib.Data;
using HeroWars.HeroesLib.GameRules;

namespace HeroWars.HeroesLib
{
    public class ArenaManager
    {
        private Presentator _presentator;
        private Logger _logger;
        private GameRuleManager _gameRuleManager;

        public List<Hero> HeroPool;
        public DuelData DuelData;
        public int RoundNumber = 0;

        public ArenaManager(int heroCount) : this(heroCount, new Presentator(), new Logger(), new GameRuleManager())
        {
        }

        public ArenaManager(int heroCount, Presentator presentator, Logger logger, GameRuleManager gameRuleManager): this(heroCount, presentator, logger, gameRuleManager, new HeroFactory() )
        { 
        }

        public ArenaManager(int heroCount, Presentator presentator, Logger logger, GameRuleManager gameRuleManager, HeroFactory heroFactory)
        {
            HeroPool = heroFactory.CreateRandomHeroes(heroCount);
            _presentator = presentator;
            _logger = logger;
            _gameRuleManager = gameRuleManager;
        }

        public void StartTournament()
        {
            _presentator.ShowTournamentStartInfo();

            do
            {
                RoundNumber += 1;

                _presentator.ShowHeroesInfo(HeroPool);

                DuelData = _gameRuleManager.SelectAttackerAndDefender(HeroPool);

                _presentator.ShowDuelInfoBeforeFight(RoundNumber, DuelData);

                _gameRuleManager.RestNotFightingHeroes(HeroPool);
                _gameRuleManager.StartDuel(DuelData);
                _gameRuleManager.ExhaustDuelists(DuelData);

                _presentator.ShowDuelInfoAfterFight(RoundNumber, DuelData);

                _logger.LogDuelDataAfterFight(RoundNumber, DuelData);

                _gameRuleManager.AddAliveDuelistsToHeroPool(DuelData, HeroPool);
            } while (_gameRuleManager.HeroesCanFightEachOther(HeroPool));

            _presentator.ShowTournamentResultInfo(RoundNumber, HeroPool);
        }
    }
}
