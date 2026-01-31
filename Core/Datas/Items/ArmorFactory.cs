using DinaCSharp.Resources;
using DinaCSharp.Services;

using Donjon_100_Pas.Core.Keys;

using Microsoft.Xna.Framework.Graphics;

namespace Donjon_100_Pas.Core.Datas.Items
{
    public static class ArmorFactory
    {
        private static Random _random = new();
        private static ResourceManager? _resourceManager;
        private static readonly Dictionary<Rarity, List<(int Weight, Armor)>> _allArmors = [];
        private static bool _initialized;

        // Événement pour notifier la progression
        public static event EventHandler<ArmorLoadProgressEventArgs>? OnArmorLoaded;

        // Propriétés pour suivre la progression
        public static int TotalArmorsToLoad { get; private set; }
        public static int ArmorsLoaded { get; private set; }

        public static void Initialize()
        {
            if (_initialized)
                return;

            ArmorsLoaded = 0;
            TotalArmorsToLoad = KeyCounter.Count(typeof(ArmorKeys)) - 1;

            _resourceManager = ServiceLocator.Get<ResourceManager>(ProjectServiceKeys.GameResourceManager)
                ?? throw new NullReferenceException("GameResourceManager not found");
            AddJunkArmors();
            AddCommonArmors();
            AddUncommonArmors();
            AddRareArmors();
            AddEliteArmors();

            _initialized = true;
        }

        public static Armor Get(Rarity rarity)
        {
            if (!_initialized)
                throw new InvalidOperationException("ArmorFactory not initialized");

            List<(int Weight, Armor Armor)> armors = _allArmors[rarity];

            int totalWeigth = armors.Sum(w => w.Weight);

            int roll = _random.Next(0, totalWeigth);
            int cursor = 0;
            foreach (var armor in armors)
            {
                cursor += armor.Weight;
                if (roll < cursor)
                    return armor.Armor;
            }
            return armors.First().Armor;
        }


        private static void AddJunkArmors()
        {
            var rarity = Rarity.Junk;
            List<(int Weight, Armor Armor)> armors =
            [
                (9, CreateArmor(rarity, ArmorKeys.Junk_BrokenChainmail,
                                $"{rarity.ToString().ToUpperInvariant()}_{nameof(ArmorKeys.Junk_BrokenChainmail).ToUpperInvariant()}",
                             [
                                new Bonus(BonusType.Defense, "BONUS_DEFENSE", 4)
                             ])),
                (10, CreateArmor(rarity, ArmorKeys.Junk_CrackedBuckler,
                                 $"{rarity.ToString().ToUpperInvariant()}_{nameof(ArmorKeys.Junk_CrackedBuckler).ToUpperInvariant()}",
                             [
                                new Bonus(BonusType.Defense, "BONUS_DEFENSE", 3)
                             ])),
                (20, CreateArmor(rarity, ArmorKeys.Junk_DirtyCloak,
                                 $"{rarity.ToString().ToUpperInvariant()}_{nameof(ArmorKeys.Junk_DirtyCloak).ToUpperInvariant()}",
                             [
                                new Bonus(BonusType.Defense, "BONUS_DEFENSE", 1)
                             ])),
                (16, CreateArmor(rarity, ArmorKeys.Junk_ImprovisedHelmet,
                                 $"{rarity.ToString().ToUpperInvariant()}_{nameof(ArmorKeys.Junk_ImprovisedHelmet).ToUpperInvariant()}",
                             [
                                new Bonus(BonusType.Defense, "BONUS_DEFENSE", 1)
                             ])),
                (8, CreateArmor(rarity, ArmorKeys.Junk_PatchedPants,
                                $"{rarity.ToString().ToUpperInvariant()}_{nameof(ArmorKeys.Junk_PatchedPants).ToUpperInvariant()}",
                             [
                                new Bonus(BonusType.Defense, "BONUS_DEFENSE", 1)
                             ])),
                (15, CreateArmor(rarity, ArmorKeys.Junk_RottenWoodShield,
                                 $"{rarity.ToString().ToUpperInvariant()}_{nameof(ArmorKeys.Junk_RottenWoodShield).ToUpperInvariant()}",
                             [
                                new Bonus(BonusType.Defense, "BONUS_DEFENSE", 2)
                             ])),
                (6, CreateArmor(rarity, ArmorKeys.Junk_RustyChestplate,
                                 $"{rarity.ToString().ToUpperInvariant()}_{nameof(ArmorKeys.Junk_RustyChestplate).ToUpperInvariant()}",
                             [
                                new Bonus(BonusType.Defense, "BONUS_DEFENSE", 4)
                             ])),
            ];
            _allArmors[Rarity.Junk] = armors;
        }
        private static void AddCommonArmors()
        {
            var rarity = Rarity.Common;
            List<(int Weight, Armor armor)> armors =
            [
                (6, CreateArmor(rarity, ArmorKeys.Common_BronzeBracers,
                                 $"{rarity.ToString().ToUpperInvariant()}_{nameof(ArmorKeys.Common_BronzeBracers).ToUpperInvariant()}",
                                 [
                                     new Bonus(BonusType.Defense, "BONUS_DEFENSE", 5)
                                 ])),
                (7, CreateArmor(rarity, ArmorKeys.Common_ChainmailCoif,
                                 $"{rarity.ToString().ToUpperInvariant()}_{nameof(ArmorKeys.Common_ChainmailCoif).ToUpperInvariant()}",
                             [
                                new Bonus(BonusType.Defense, "BONUS_DEFENSE", 6)
                             ])),
                (5, CreateArmor(rarity, ArmorKeys.Common_GuardArmour,
                                $"{rarity.ToString().ToUpperInvariant()}_{nameof(ArmorKeys.Common_GuardArmour).ToUpperInvariant()}",
                             [
                                new Bonus(BonusType.Defense, "BONUS_DEFENSE", 4)
                             ])),
                (6, CreateArmor(rarity, ArmorKeys.Common_IronHelmet,
                                $"{rarity.ToString().ToUpperInvariant()}_{nameof(ArmorKeys.Common_IronHelmet).ToUpperInvariant()}",
                             [
                                new Bonus(BonusType.Defense, "BONUS_DEFENSE", 5)
                             ])),
                (2, CreateArmor(rarity, ArmorKeys.Common_LeatherVest,
                                 $"{rarity.ToString().ToUpperInvariant()}_{nameof(ArmorKeys.Common_LeatherVest).ToUpperInvariant()}",
                             [
                                new Bonus(BonusType.Defense, "BONUS_DEFENSE", 4)
                             ])),
                (6, CreateArmor(rarity, ArmorKeys.Common_LightShield,
                                 $"{rarity.ToString().ToUpperInvariant()}_{nameof(ArmorKeys.Common_LightShield).ToUpperInvariant()}",
                             [
                                new Bonus(BonusType.Defense, "BONUS_DEFENSE", 6)
                             ])),
                (5, CreateArmor(rarity, ArmorKeys.Common_PaddedJacket,
                                 $"{rarity.ToString().ToUpperInvariant()}_{nameof(ArmorKeys.Common_PaddedJacket).ToUpperInvariant()}",
                             [
                                new Bonus(BonusType.Defense, "BONUS_DEFENSE", 5)
                             ])),
                (3, CreateArmor(rarity, ArmorKeys.Common_ReinforcedGloves,
                                 $"{rarity.ToString().ToUpperInvariant()}_{nameof(ArmorKeys.Common_ReinforcedGloves).ToUpperInvariant()}",
                             [
                                new Bonus(BonusType.Defense, "BONUS_DEFENSE", 7)
                             ])),
                (4, CreateArmor(rarity, ArmorKeys.Common_SoldierBoots,
                                 $"{rarity.ToString().ToUpperInvariant()}_{nameof(ArmorKeys.Common_SoldierBoots).ToUpperInvariant()}",
                             [
                                new Bonus(BonusType.Defense, "BONUS_DEFENSE", 7)
                             ])),
            ];
            _allArmors[Rarity.Common] = armors;
        }
        private static void AddUncommonArmors()
        {
            var rarity = Rarity.Uncommon;
            List<(int Weight, Armor)> armors =
            [
                (10, CreateArmor(rarity, ArmorKeys.Uncommon_KnightGauntlets,
                                 $"{rarity.ToString().ToUpperInvariant()}_{nameof(ArmorKeys.Uncommon_KnightGauntlets).ToUpperInvariant()}",
                             [
                                new Bonus(BonusType.Defense, "BONUS_DEFENSE", 10)
                             ])),
                (2, CreateArmor(rarity, ArmorKeys.Uncommon_LeatherArmor,
                                 $"{rarity.ToString().ToUpperInvariant()}_{nameof(ArmorKeys.Uncommon_LeatherArmor).ToUpperInvariant()}",
                             [
                                new Bonus(BonusType.Defense, "BONUS_DEFENSE", 14)
                             ])),
                (6, CreateArmor(rarity, ArmorKeys.Uncommon_ScoutCloak,
                                 $"{rarity.ToString().ToUpperInvariant()}_{nameof(ArmorKeys.Uncommon_ScoutCloak).ToUpperInvariant()}",
                             [
                                new Bonus(BonusType.Defense, "BONUS_DEFENSE", 9)
                             ])),
                (9, CreateArmor(rarity, ArmorKeys.Uncommon_SteelBreastplate,
                                 $"{rarity.ToString().ToUpperInvariant()}_{nameof(ArmorKeys.Uncommon_SteelBreastplate).ToUpperInvariant()}",
                             [
                                new Bonus(BonusType.Defense, "BONUS_DEFENSE", 18)
                             ])),
                (7, CreateArmor(rarity, ArmorKeys.Uncommon_SteelHelmet,
                                 $"{rarity.ToString().ToUpperInvariant()}_{nameof(ArmorKeys.Uncommon_SteelHelmet).ToUpperInvariant()}",
                             [
                                new Bonus(BonusType.Defense, "BONUS_DEFENSE", 10)
                             ])),
            ];
            _allArmors[Rarity.Uncommon] = armors;
        }
        private static void AddRareArmors()
        {
            var rarity = Rarity.Rare;
            List<(int Weight, Armor)> armors =
            [
                (15, CreateArmor(rarity, ArmorKeys.Rare_DragonScaleShield,
                                 $"{rarity.ToString().ToUpperInvariant()}_{nameof(ArmorKeys.Rare_DragonScaleShield).ToUpperInvariant()}",
                             [
                                new Bonus(BonusType.Attack, "BONUS_ATTACK", 19),
                                new Bonus(BonusType.Fire, "BONUS_RESISTFIRE", percentage: 8)
                             ])),
                (10, CreateArmor(rarity, ArmorKeys.Rare_EnchantedRobe,
                                 $"{rarity.ToString().ToUpperInvariant()}_{nameof(ArmorKeys.Rare_EnchantedRobe).ToUpperInvariant()}",
                             [
                                new Bonus(BonusType.Defense, "BONUS_DEFENSE", 14),
                                new Bonus(BonusType.Mana, "BONUS_MANA", 5)
                             ])),
                (10, CreateArmor(rarity, ArmorKeys.Rare_MithrilChainmail,
                                 $"{rarity.ToString().ToUpperInvariant()}_{nameof(ArmorKeys.Rare_MithrilChainmail).ToUpperInvariant()}",
                             [
                                new Bonus(BonusType.Attack, "BONUS_ATTACK", 16),
                                new Bonus(BonusType.Ice, "BONUS_ICE", 5)
                             ])),
            ];
            _allArmors[Rarity.Rare] = armors;
        }
        private static void AddEliteArmors()
        {
            var rarity = Rarity.Elite;
            List<(int Weight, Armor)> armors =
            [
                (20, CreateArmor(rarity, ArmorKeys.Elite_AetherPlate,
                                  $"{rarity.ToString().ToUpperInvariant()}_{nameof(ArmorKeys.Elite_AetherPlate).ToUpperInvariant()}",
                                 [
                                    new Bonus(BonusType.Defense, "BONUS_DEFENSE", 50),
                                    new Bonus(BonusType.ResistBleed, "BONUS_RESISTBLEED", percentage: 50),
                                    new Bonus(BonusType.ResistPoison, "BONUS_RESISTPOISON", percentage: 50),
                                 ])),
                (20, CreateArmor(rarity, ArmorKeys.Elite_PhoenixEmbrace,
                                  $"{rarity.ToString().ToUpperInvariant()}_{nameof(ArmorKeys.Elite_PhoenixEmbrace).ToUpperInvariant()}",
                                 [
                                    new Bonus(BonusType.Defense, "BONUS_DEFENSE", 40),
                                    new Bonus(BonusType.ResistFire, "BONUS_RESISTFIRE", percentage : 75)
                                 ])),
                (20, CreateArmor(rarity, ArmorKeys.Elite_VoidStalkerGarb,
                                  $"{rarity.ToString().ToUpperInvariant()}_{nameof(ArmorKeys.Elite_VoidStalkerGarb).ToUpperInvariant()}",
                                 [
                                    new Bonus(BonusType.Defense, "BONUS_DEFENSE", 31),
                                    new Bonus(BonusType.Mana, "BONUS_MANA", 50),
                                    new Bonus(BonusType.ResistIce, "BONUS_RESISTICE", percentage: 25),
                                 ])),
            ];
            _allArmors[Rarity.Elite] = armors;
        }
        private static Armor CreateArmor(Rarity rarity, Key<ResourceTag> key, string name, List<Bonus> bonuses)
        {
            Armor armor = new Armor(name, _resourceManager!.Load<Texture2D>(key)!, bonuses)
            {
                Rarity = rarity
            };
            NotifyProgress();
            return armor;
        }
        private static void NotifyProgress()
        {
            ArmorsLoaded++;
            OnArmorLoaded?.Invoke(null, new ArmorLoadProgressEventArgs(ArmorsLoaded, TotalArmorsToLoad));
        }
        public static void ClearEventSubscribers()
        {
            OnArmorLoaded = null;
        }
    }
    // Classe EventArgs pour l'événement de progression
    public class ArmorLoadProgressEventArgs(int armorsLoaded, int totalArmors) : EventArgs
    {
        public int ArmorsLoaded { get; } = armorsLoaded;
        public int TotalArmors { get; } = totalArmors;
        public float Progress => TotalArmors > 0 ? (float)ArmorsLoaded / TotalArmors : 0f;
    }
}
