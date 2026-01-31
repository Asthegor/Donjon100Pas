using DinaCSharp.Services.Scenes;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Diagnostics;

namespace Donjon_100_Pas.GameMechanics.Scenes.City
{
    // Note : Utilisez la propriété 'SceneManager' (héritée) pour accéder au moteur.
    // Ne capturez pas le paramètre 'sceneManager' dans les méthodes pour éviter l'erreur CS9107.
    public class HerboristScene(SceneManager sceneManager) : Scene(sceneManager)
    {
        public override void Load()
        {
            Trace.WriteLine(GetType().Name);
        }
        public override void Reset()
        {
        }
        public override void Update(GameTime gametime)
        {
        }
        public override void Draw(SpriteBatch spritebatch)
        {
        }
    }
}
