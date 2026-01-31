using DinaCSharp.Services;
using DinaCSharp.Services.Scenes;

using Donjon_100_Pas.Core;
using Donjon_100_Pas.Core.Datas.Characters;
using Donjon_100_Pas.Core.Datas.Events;
using Donjon_100_Pas.Core.Keys;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;

namespace Donjon_100_Pas.GameMechanics.Scenes.Events
{
    // Note : Utilisez la propriété 'SceneManager' (héritée) pour accéder au moteur.
    // Ne capturez pas le paramètre 'sceneManager' dans les méthodes pour éviter l'erreur CS9107.
    public class NarrativeScene(SceneManager sceneManager) : Scene(sceneManager)
    {

        private Player _player;
        private NarrativeEvent _currentEvent;
        public override void Load()
        {
            _player = ServiceLocator.Get<Player>(ProjectServiceKeys.Player);
        }
        public override void Reset()
        {
            _currentEvent = ServiceLocator.Get<NarrativeEvent>(ProjectServiceKeys.CurrentEvent);
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
