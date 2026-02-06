using DinaCSharp.Events;
using DinaCSharp.Graphics;
using DinaCSharp.Resources;
using DinaCSharp.Services;
using DinaCSharp.Services.Scenes;

using Dungeon100Steps.Core.Keys;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;

namespace Dungeon100Steps.GameMechanics.Scenes
{
    // Note : Utilisez la propriété 'SceneManager' (héritée) pour accéder au moteur.
    // Ne capturez pas le paramètre 'sceneManager' dans les méthodes pour éviter l'erreur CS9107.
    public class TestScene(SceneManager sceneManager) : Scene(sceneManager)
    {
        public override void Load()
        {
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

        public void Dispose()
        {
        }
    }
}
