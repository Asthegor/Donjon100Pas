using Donjon_100_Pas.Core.Datas.Items;

using Microsoft.Xna.Framework.Graphics;

namespace Donjon_100_Pas.Core.Datas.Characters
{
    public class Player(string name, Texture2D texture, int attack, int defense, int health, int mana, Texture2D bagtexture)
        : Character(name, texture, attack, defense, health, mana)
    {
        private const int BASE_EXPERIENCE = 100;
        private const int LEVEL_PROGRESS_EXPERIENCE = 50;
        private const int MAX_HEALTH_LEVEL_UP_PERCENTAGE = 10;
        private const int MAX_MANA_LEVEL_UP_PERCENTAGE = 5;

        // Événements pour notifier les changements
        public event Action? OnLevelUp;

        private int _gold;

        public HeroClass Class { get; set; }
        public int Level { get; set; }
        public int TotalExperience { get; set; }
        public int LevelExperience { get; set; }

        public int Dexterity { get; set; }
        public int Constitution { get; set; }
        public int Gold
        {
            get => _gold;
            set
            {
                if (_gold != value)
                {
                    _gold = value;
                    RaiseStatsChanged();
                }
            }
        }

        public Inventory Inventory { get; set; } = new Inventory(bagtexture);
        public override void EquipWeapon(Weapon weapon)
        {
            if (Weapon != null)
            {
                Inventory?.Remove(weapon);
                Inventory?.Add(Weapon);
            }
            Weapon = weapon;
        }
        public override void EquipArmor(Armor armor)
        {
            if (Armor != null)
            {
                Inventory?.Remove(armor);
                Inventory?.Add(Armor);
            }
            Armor = armor;
        }
        public void GetExperience(int xp)
        {
            TotalExperience += xp;
            LevelExperience += xp;
            var nextLevelExperience = BASE_EXPERIENCE + LEVEL_PROGRESS_EXPERIENCE * (Level + 1);
            if (LevelExperience > nextLevelExperience)
            {
                LevelExperience -= nextLevelExperience;
                GetLevelUp();
            }
        }
        private void GetLevelUp()
        {
            Level++;
            MaxHealth += MaxHealth * MAX_HEALTH_LEVEL_UP_PERCENTAGE / 100;
            BaseAttack += 2;
            BaseDefense++;
            MaxMana += MaxHealth * MAX_MANA_LEVEL_UP_PERCENTAGE / 100;

            OnLevelUp?.Invoke();
            RaiseStatsChanged(); // Toutes les stats changent au level up
        }
        public void DrinkPotion(Potion potion)
        {
            Inventory.Remove(potion);
            potion.Drink(this);
        }

    }
}
