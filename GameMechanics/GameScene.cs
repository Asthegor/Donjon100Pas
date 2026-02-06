using DinaCSharp.Services.Scenes;

using Dungeon100Steps.Core;
using Dungeon100Steps.Core.Datas.Events;
using Dungeon100Steps.Core.Datas.Items;
using Dungeon100Steps.Core.Keys;

using Dungeon100Steps.GameMechanics.Scenes;
using Dungeon100Steps.GameMechanics.Scenes.City;
using Dungeon100Steps.GameMechanics.Scenes.Tutorial;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Dungeon100Steps.GameMechanics
{
    public class GameScene(SceneManager sceneManager) : Scene(sceneManager)
    {
        private SceneManager _subSceneManager;
        private bool _loadingFinished;


        public override void Load()
        {
            _subSceneManager = SceneManager.CreateNewInstance("AssetsContent");

            ResetLoadingScreen("LOADING_WEAPONS");
            WeaponFactory.OnWeaponLoaded += OnWeaponLoadProgress;
            WeaponFactory.Initialize();

            ResetLoadingScreen("LOADING_ARMORS");
            ArmorFactory.OnArmorLoaded += OnArmorLoadProgress;
            ArmorFactory.Initialize();

            ResetLoadingScreen("LOADING_POTIONS");
            PotionFactory.OnPotionLoaded += OnPotionLoadProgress;
            PotionFactory.Initialize();

            RegisterSubScenes();
        }
        public override void Reset()
        {
            WeaponFactory.ClearEventSubscribers();
            ArmorFactory.ClearEventSubscribers();
            PotionFactory.ClearEventSubscribers();

            _loadingFinished = true;

            _subSceneManager.SetCurrentScene(ProjectSceneKeys.TutorialSkipScene);

            //_subSceneManager.AddScene(ProjectSceneKeys.TestScene, () => new TestScene(_subSceneManager));
            //_subSceneManager.SetCurrentScene(ProjectSceneKeys.TestScene);
        }
        public override void Update(GameTime gametime)
        {
            if (!_loadingFinished)
                return;

            _subSceneManager.Update(gametime);
        }
        public override void Draw(SpriteBatch spritebatch)
        {
            if (!_loadingFinished)
                return;

            _subSceneManager.Draw(spritebatch);
        }

        public void RegisterSubScenes()
        {
            _subSceneManager.AddScene(ProjectSceneKeys.SelectPlayerScene, () => new SelectPlayerScene(_subSceneManager));
            _subSceneManager.AddScene(ProjectSceneKeys.TutorialSkipScene, () => new TutorialSkipScene(_subSceneManager));
            _subSceneManager.AddScene(ProjectSceneKeys.TutorialScene, () => new TutorialScene(_subSceneManager));
            _subSceneManager.AddScene(ProjectSceneKeys.InventoryScene, () => new InventoryScene(_subSceneManager));
            _subSceneManager.AddScene(ProjectSceneKeys.CityScene, () => new CityScene(_subSceneManager));
            _subSceneManager.AddScene(ProjectSceneKeys.BlacksmithScene, () => new BlacksmithScene(_subSceneManager));
            _subSceneManager.AddScene(ProjectSceneKeys.HerboristScene, () => new HerboristScene(_subSceneManager));
            _subSceneManager.AddScene(ProjectSceneKeys.TavernScene, () => new TavernScene(_subSceneManager));
            _subSceneManager.AddScene(ProjectSceneKeys.DungeonScene, () => new DungeonScene(_subSceneManager));
        }

        private void OnTutorialCompleted(object sender, TutorialEventArgs e)
        {
            if (e.Result == EventResult.Defeat)
            {
                // TODO: Gérer l'effacement des données sauvegardées si le joueur perd le tutoriel ?
                SetCurrentScene(ProjectSceneKeys.MainMenu);
                return;
            }
            _subSceneManager.SetCurrentScene(ProjectSceneKeys.CityScene);
        }

        #region Données pour l'écran de chargement
        private void OnWeaponLoadProgress(object sender, WeaponLoadProgressEventArgs e)
        {
            LoadingProgress = e.Progress;
        }
        private void OnArmorLoadProgress(object sender, ArmorLoadProgressEventArgs e)
        {
            LoadingProgress = e.Progress;
        }
        private void OnPotionLoadProgress(object sender, PotionLoadProgressEventArgs e)
        {
            LoadingProgress = e.Progress;
        }
        #endregion

    }
}
