using Microsoft.Xna.Framework.Graphics;

namespace Donjon_100_Pas.Core.Datas.Items
{
    public class Armor(string name, Texture2D texture, List<Bonus> bonuses)
        : Item(name, texture, bonuses, stackLimit: 1)
    {
    }
}
