using DinaCSharp.Graphics;
using DinaCSharp.Resources;
using DinaCSharp.Services;
using DinaCSharp.Services.Scenes;

using Dungeon100Steps.Core;
using Dungeon100Steps.Core.Datas.Dungeons;
using Dungeon100Steps.Core.Datas.Events;
using Dungeon100Steps.Core.Keys;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;

namespace Dungeon100Steps.GameMechanics.Scenes
{
    // Note : Utilisez la propriété 'SceneManager' (héritée) pour accéder au moteur.
    // Ne capturez pas le paramètre 'sceneManager' dans les méthodes pour éviter l'erreur CS9107.
    public class WaitingScene(SceneManager sceneManager) : Scene(sceneManager)
    {
        public event EventHandler<DungeonEndedEventArgs> OnDungeonEnded;

        private ResourceManager _resourceManager = ServiceLocator.Get<ResourceManager>(ProjectServiceKeys.AssetsResourceManager);
        private Dungeon _dungeon;
        private Button _goToNextEvent;
        public override void Load()
        {
            _dungeon = ServiceLocator.Get<Dungeon>(ProjectServiceKeys.CurrentDungeon);
        }
        public override void Reset()
        {
            var pos = new Vector2(10, 10);
            var buttonImage = _resourceManager.Load<Texture2D>(GameResourceKeys.Button_Next);
            _goToNextEvent = new Button(position: pos, backgroundImage: buttonImage, onClick: GoToNextEvent);
        }
        public override void Update(GameTime gametime)
        {
            _goToNextEvent?.Update(gametime);
        }
        public override void Draw(SpriteBatch spritebatch)
        {
            _goToNextEvent?.Draw(spritebatch);
        }

        private void GoToNextEvent(Button button)
        {
            var nextEvent = _dungeon.NextEvent();
            ServiceLocator.Register(ProjectServiceKeys.CurrentEvent, nextEvent);

            if (nextEvent == null)
            {
                OnDungeonEnded?.Invoke(this, new DungeonEndedEventArgs(EventResult.Victory));
                return;
            }

            if (nextEvent is CombatEvent)
                SetCurrentScene(ProjectSceneKeys.CombatScene);
            else if (nextEvent is TreasureEvent)
                SetCurrentScene(ProjectSceneKeys.TreasureScene);
            else if (nextEvent is TrapEvent)
                SetCurrentScene(ProjectSceneKeys.TrapScene);
            else if (nextEvent is NarrativeEvent)
                SetCurrentScene(ProjectSceneKeys.NarrativeScene);
            else
                throw new InvalidOperationException("Événement inconnu dans le tutoriel.");
        }

        public override void ClearEventSubscribers()
        {
            
        }
    }
}
