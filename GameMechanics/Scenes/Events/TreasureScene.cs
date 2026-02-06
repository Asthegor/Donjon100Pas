using DinaCSharp.Services;
using DinaCSharp.Services.Scenes;

using Dungeon100Steps.Core;
using Dungeon100Steps.Core.Datas.Characters;
using Dungeon100Steps.Core.Datas.Events;
using Dungeon100Steps.Core.Keys;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;

namespace Dungeon100Steps.GameMechanics.Scenes.Events
{
    // Note : Utilisez la propriété 'SceneManager' (héritée) pour accéder au moteur.
    // Ne capturez pas le paramètre 'sceneManager' dans les méthodes pour éviter l'erreur CS9107.
    public class TreasureScene(SceneManager sceneManager) : Scene(sceneManager)
    {
        private Player _player;
        private TreasureEvent _currentEvent;
        public override void Load()
        {
            _player = ServiceLocator.Get<Player>(ProjectServiceKeys.Player);
        }
        public override void Reset()
        {
            _currentEvent = ServiceLocator.Get<TreasureEvent>(ProjectServiceKeys.CurrentEvent);
            if (_currentEvent == null)
                throw new InvalidOperationException($"CurrentEvent n'est pas de type '{_currentEvent.GetType().Name}' dans le ServiceLocator.");
        }
        public override void Update(GameTime gametime)
        {
            if (_player.IsDead)
            {
                GoToWaitingScene(EventResult.Defeat);
                return;
            }
        }
        public override void Draw(SpriteBatch spritebatch)
        {
        }
        private void GoToWaitingScene(EventResult result)
        {
            _currentEvent.Result = result;
            SetCurrentScene(ProjectSceneKeys.WaitingScene);
        }
    }
}
