using FluentAssertions;
using HeroesLib.Interfaces;
using HeroWars.HeroesLib.Data;
using HeroWars.HeroesLib.GameRules;
using HeroWars.HeroesLib.GameRules.Attack;
using HeroWars.HeroesLib.Utility;
using Moq;

namespace HeroWars.HeroesLibTests.GameRules
{
    public class GameRuleManagerTests
    {
        private readonly Mock<IGameRules> _gameRulesMock;
        private readonly Mock<IRandomizer> _randomizerMock;

        public GameRuleManagerTests()
        {
            _gameRulesMock = new();
            _randomizerMock = new();
        }

        [Fact]
        public void SelectAttackerAndDefender_Test()
        {
            // Arrange
            var heroPool = new List<Hero>()
            {
                new Archer() { Id = 1 },
                new SwordsMan() { Id= 2, CurrentHealth = 100 },
                new HorseMan() { Id= 3 },
                new SwordsMan() { Id= 4, CurrentHealth = 120 },
                new Archer() { Id= 5 }
            };

            _randomizerMock.SetupSequence(
                x => x.GetRandomItem<Hero>(It.IsAny<List<Hero>>()))
                .Returns(heroPool[1])
                .Returns(heroPool[3]);

            var gameRuleManager = new GameRuleManager(_gameRulesMock.Object, _randomizerMock.Object);


            // Act
            var duelData = gameRuleManager.SelectAttackerAndDefender(heroPool);

            // Assert
            duelData.Attacker.Id.Should().Be(2);
            duelData.Attacker.CurrentHealth.Should().Be(100);
            duelData.Defender.Id.Should().Be(4);
            duelData.Defender.CurrentHealth.Should().Be(120);

            heroPool.Count.Should().Be(3);
            heroPool[1].Id.Should().Be(3);
            heroPool[2].Id.Should().Be(5);
        }

        [Fact]
        public void RestNotFightingHeroes_Test()
        {
            // Arrange
            var gameRuleManager = new GameRuleManager();
            var heroPool = new List<Hero>()
            {
                new Archer()
                {
                    Id = 1,
                    CurrentHealth = 40
                },
                new SwordsMan()
                {
                    Id= 2,
                    CurrentHealth = 60
                },
                new HorseMan()
                {
                    Id= 3,
                    CurrentHealth = 150
                }
            };

            // Act
            gameRuleManager.RestNotFightingHeroes(heroPool);

            // Assert
            heroPool[0].CurrentHealth.Should().Be(50);
            heroPool[1].CurrentHealth.Should().Be(70);
            heroPool[2].CurrentHealth.Should().Be(150);
        }

        [Fact]
        public void StartDuel_Should_AttackerDies()
        {
            // Arrange
            _gameRulesMock.Setup(
                x => x.GetAttackRule(It.IsAny<HeroType>(), It.IsAny<HeroType>()))
                .Returns(new AttackRule
                {
                    Attacker = HeroType.HorseMan,
                    Defender = HeroType.SwordsMan,
                    AttackResult = new AttackerDies()
                });

            var duelData = new DuelData()
            {
                Attacker = new HorseMan() { CurrentHealth = 150 },
                Defender = new SwordsMan() { CurrentHealth = 120 }
            };

            var gameRuleManager = new GameRuleManager(_gameRulesMock.Object, new Randomizer());

            // Act
            gameRuleManager.StartDuel(duelData);

            // Assert
            duelData.Attacker.CurrentHealth.Should().Be(0);
            duelData.Defender.CurrentHealth.Should().Be(120);
        }

        [Fact]
        public void StartDuel_Should_DefenderDies()
        {
            // Arrange
            _gameRulesMock.Setup(
                x => x.GetAttackRule(It.IsAny<HeroType>(), It.IsAny<HeroType>()))
                .Returns(new AttackRule
                {
                    Attacker = HeroType.SwordsMan,
                    Defender = HeroType.SwordsMan,
                    AttackResult = new DefenderDies()
                });

            var duelData = new DuelData()
            {
                Attacker = new HorseMan() { CurrentHealth = 120 },
                Defender = new SwordsMan() { CurrentHealth = 120 }
            };

            var gameRuleManager = new GameRuleManager(_gameRulesMock.Object, new Randomizer());

            // Act
            gameRuleManager.StartDuel(duelData);

            // Assert
            duelData.Attacker.CurrentHealth.Should().Be(120);
            duelData.Defender.CurrentHealth.Should().Be(0);
        }

        [Fact]
        public void StartDuel_Should_DefenderDiesInPercent_Died()
        {
            // Arrange
            _gameRulesMock.Setup(
                x => x.GetAttackRule(It.IsAny<HeroType>(), It.IsAny<HeroType>()))
                .Returns(new AttackRule
                {
                    Attacker = HeroType.Archer,
                    Defender = HeroType.HorseMan,
                    AttackResult = new DefenderDiesInPercent() { Percentage = 40 }
                });

            _randomizerMock.Setup(
                x => x.GetRandomPercentage())
                .Returns(40);

            var duelData = new DuelData()
            {
                Attacker = new Archer() { CurrentHealth = 100 },
                Defender = new HorseMan() { CurrentHealth = 150 }
            };

            var gameRuleManager = new GameRuleManager(_gameRulesMock.Object, _randomizerMock.Object);

            // Act
            gameRuleManager.StartDuel(duelData);

            // Assert
            duelData.Attacker.CurrentHealth.Should().Be(100);
            duelData.Defender.CurrentHealth.Should().Be(0);
        }

        [Fact]
        public void StartDuel_Should_DefenderDiesInPercent_Survived()
        {
            // Arrange
            _gameRulesMock.Setup(
                x => x.GetAttackRule(It.IsAny<HeroType>(), It.IsAny<HeroType>()))
                .Returns(new AttackRule
                {
                    Attacker = HeroType.Archer,
                    Defender = HeroType.HorseMan,
                    AttackResult = new DefenderDiesInPercent() { Percentage = 40 }
                });

            _randomizerMock.Setup(
                x => x.GetRandomPercentage())
                .Returns(80);

            var duelData = new DuelData()
            {
                Attacker = new Archer() { CurrentHealth = 100 },
                Defender = new HorseMan() { CurrentHealth = 150 }
            };

            var gameRuleManager = new GameRuleManager(_gameRulesMock.Object, _randomizerMock.Object);

            // Act
            gameRuleManager.StartDuel(duelData);

            // Assert
            duelData.Attacker.CurrentHealth.Should().Be(100);
            duelData.Defender.CurrentHealth.Should().Be(150);
        }

        [Fact]
        public void StartDuel_Should_NothingHappens()
        {
            // Arrange
            _gameRulesMock.Setup(
                x => x.GetAttackRule(It.IsAny<HeroType>(), It.IsAny<HeroType>()))
                .Returns(new AttackRule
                {
                    Attacker = HeroType.SwordsMan,
                    Defender = HeroType.HorseMan,
                    AttackResult = new NothingHappens()
                });

            var duelData = new DuelData()
            {
                Attacker = new SwordsMan() { CurrentHealth = 120 },
                Defender = new HorseMan() { CurrentHealth = 150 }
            };

            var gameRuleManager = new GameRuleManager(_gameRulesMock.Object, new Randomizer());

            // Act
            gameRuleManager.StartDuel(duelData);

            // Assert
            duelData.Attacker.CurrentHealth.Should().Be(120);
            duelData.Defender.CurrentHealth.Should().Be(150);
        }
    }
}
