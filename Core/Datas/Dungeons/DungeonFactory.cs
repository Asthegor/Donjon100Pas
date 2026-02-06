using DinaCSharp.Resources;
using DinaCSharp.Services;

using Dungeon100Steps.Core.Datas.Characters;
using Dungeon100Steps.Core.Datas.Enemies;
using Dungeon100Steps.Core.Datas.Events;
using Dungeon100Steps.Core.Datas.Items;
using Dungeon100Steps.Core.Keys;

namespace Dungeon100Steps.Core.Datas.Dungeons
{
    public static class DungeonFactory
    {
        private static readonly List<(int Weight, Func<int, Event>)> Events =
            [
                (40, (level) => GenerateCombatEvent(level)),
                (25, (level) => GenerateTreasureEvent(level)),
                (20, (level) => GenerateTrapEvent(level)),
                (15, (level) => GenerateNarrativeEvent(level)),
            ];
        private static Random? _random = new Random();
        public static Dungeon Generate(int maxLevel)
        {
            int currentSeed = new Guid().GetHashCode();
            _random = new Random(currentSeed);

            WeaponFactory.Initialize();
            ArmorFactory.Initialize();
            PotionFactory.Initialize();


            Event[] events = new Event[maxLevel];
            for (int index = 0; index < maxLevel; index++)
                events[index] = GenerateEvent(index, index == maxLevel)!;

            Dungeon level = new Dungeon(events);

            return level;
        }

        private static Event? GenerateEvent(int level, bool isLastLevel)
        {
            // Dernière salle = toujours Boss
            if (isLastLevel)
                return GenerateCombatEvent(level, true);


            int maxweight = 0;
            foreach ((var weight, var fct) in Events)
                maxweight += weight;

            int roll = _random!.Next(0, maxweight);
            int count = 0;
            foreach ((var weight, var fct) in Events)
            {
                count += weight;
                if (roll < count)
                    return fct(level);
            }
            return null;
        }

        private static CombatEvent GenerateCombatEvent(int level, bool isBoss = false)
        {
            ResourceManager resourceManager = ServiceLocator.Get<ResourceManager>(ProjectServiceKeys.AssetsResourceManager)
                ?? throw new InvalidOperationException("AssetsResourceManager not found");

            int roll = _random!.Next(0, 100);

            Enemy enemy;
            int gold = roll < 65 ? level : 0;
            Item? loot = roll >= 65 ? ItemFactory.CreateEquipment(level) : null;
            int exp = 5 * level;
            if (!isBoss)
                enemy = EnemyFactory.Generate(level, resourceManager);
            else
                enemy = EnemyFactory.GenerateBoss(level, resourceManager);


            CombatEvent combatEvent = new CombatEvent(enemy, gold, exp, loot);
            return combatEvent;
        }

        private static TreasureEvent GenerateTreasureEvent(int level)
        {
            int roll = _random!.Next(0, 100);
            return roll switch
            {
                <= (int)TreasureType.Normal => GenerateNormalTreasureEvent(level),
                <= (int)TreasureType.Trapped => GenerateTrappedTreasureEvent(level),
                <= (int)TreasureType.Rare => GenerateRareTreasureEvent(level),
                _ => throw new InvalidDataException("TreasureType not found")
            };
        }
        private static TreasureEvent GenerateNormalTreasureEvent(int level)
        {
            int roll = _random!.Next(0, 100);
            Item? loot = roll switch
            {
                < 10 => ItemFactory.CreateEquipment(level),
                < 40 => PotionFactory.Get(),
                _ => null
            };
            int gold = _random!.Next(50, 151);
            return new TreasureEvent(loot, gold);
        }
        private static TreasureEvent GenerateTrappedTreasureEvent(int level)
        {
            int roll = _random!.Next(0, 100);
            Item? loot = roll switch
            {
                < 10 => ItemFactory.CreateEquipment(level),
                < 40 => PotionFactory.Get(),
                _ => null
            };
            int gold = _random!.Next(50, 151);
            int trapPercentage = _random!.Next(10, 21);
            TrapEvent trap = new TrapEvent(trapPercentage);
            return new TreasureEvent(loot, gold, trap);
        }
        private static TreasureEvent GenerateRareTreasureEvent(int level)
        {
            Item? loot = ItemFactory.CreateEquipment(level, Rarity.Rare);
            return new TreasureEvent(loot);
        }

        private static TrapEvent GenerateTrapEvent(int level)
        {
            int trapPercentage = _random!.Next(10, 21);
            return new TrapEvent(trapPercentage);
        }

        private static NarrativeEvent GenerateNarrativeEvent(int level)
        {
            NarrativeEvent narrativeEvent = new NarrativeEvent();

            return narrativeEvent;
        }

        public static Dungeon GenerateTutorial()
        {
            ResourceManager resourceManager = ServiceLocator.Get<ResourceManager>(ProjectServiceKeys.AssetsResourceManager)
                    ?? throw new InvalidOperationException("AssetsResourceManager not found");

            return new Dungeon(CreateTutorialEvents(resourceManager));
        }
        private static Event[] CreateTutorialEvents(ResourceManager resourceManager)
        {
            var weaponLoot = ItemFactory.GenerateTutorialWepon(resourceManager);
            var ratEnemy = EnemyFactory.GenerateTutorialRat(resourceManager);
            var bossEnemy = EnemyFactory.GenerateTutorialBoss(resourceManager);

            return
            [
                new TreasureEvent(weaponLoot),
                new CombatEvent(ratEnemy, rewardGold: 0, rewardXP: 10, loot: null),
                new TrapEvent(percentage: 15),
                new NarrativeEvent(),
                new CombatEvent(bossEnemy, rewardGold: 50, rewardXP: 90, loot: null)
            ];
        }
    }
}
