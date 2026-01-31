using Microsoft.Xna.Framework.Graphics;

namespace Donjon_100_Pas.Core.Datas.Characters
{
    public static class PlayerFactory
    {
        public static Player CreatePlayer(HeroClass heroclass, Genre genre, Texture2D texture, Texture2D bagtexture)
        {
            return heroclass switch
            {
                HeroClass.Warrior => CreateWarrior(genre, texture, bagtexture),
                HeroClass.Mage => CreateMage(genre, texture, bagtexture),
                HeroClass.Thief => CreateThief(genre, texture, bagtexture),
                _ => throw new ArgumentException("Invalid class type"),
            };
        }

        // TODO: lors de l'implémentation de la sauvegarde,
        // charger les données du joueur depuis le fichier de sauvegarde
        //public static Player CreatePlayerFromSave()
        //{
        //    return new Player();
        //}

        private static Player CreateWarrior(Genre genre, Texture2D texture, Texture2D bagtexture)
        {
            return new Player(name: "WARRIOR_" + genre.ToString().ToUpperInvariant(),
                              texture: texture,
                              attack: 15, defense: 10,
                              health: 120, mana: 30,
                              bagtexture: bagtexture)
            {
                Class = HeroClass.Warrior,
                Dexterity = 5,
                Constitution = 8
            };
        }
        private static Player CreateMage(Genre genre, Texture2D texture, Texture2D bagtexture)
        {
            return new Player(name: "MAGE_" + genre.ToString().ToUpperInvariant(),
                              texture: texture,
                              attack: 8, defense: 4,
                              health: 80, mana: 60,
                              bagtexture: bagtexture)
            {
                Class = HeroClass.Mage,
                Dexterity = 6,
                Constitution = 5
            };
        }
        private static Player CreateThief(Genre genre, Texture2D texture, Texture2D bagtexture)
        {
            return new Player(name: "THIEF_" + genre.ToString().ToUpperInvariant(),
                              texture: texture,
                              attack: 12, defense: 6,
                              health: 100, mana: 40,
                              bagtexture: bagtexture)
            {
                Class = HeroClass.Thief,
                Dexterity = 12,
                Constitution = 6
            };
        }
    }
}
