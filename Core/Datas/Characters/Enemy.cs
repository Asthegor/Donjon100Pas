using DinaCSharp.Events;

using Donjon_100_Pas.Core.Datas.Characters;
using Donjon_100_Pas.Core.Datas.Items;

using Microsoft.Xna.Framework.Graphics;

namespace Donjon_100_Pas.Core.Datas.Enemies
{
    public class Enemy(string name, Texture2D texture, int attack, int defense, int health, int mana = 0)
        : Character(name, texture, attack, defense, health, mana)
    {
        public override void EquipArmor(Armor armor)
        {
            Armor = armor;
        }

        public override void EquipWeapon(Weapon weapon)
        {
            Weapon = weapon;
        }
    }
}
