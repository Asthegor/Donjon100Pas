using DinaCSharp.Services.Scenes;

using Donjon_100_Pas.Core.Keys;
using Donjon_100_Pas.GameMechanics.Scenes;
using Donjon_100_Pas.UI.Scenes;

namespace Donjon_100_Pas.UI
{
    public static class UISceneRegistry
    {
        public static void RegisterScenes(SceneManager sceneManager)
        {
            sceneManager.AddScene(ProjectSceneKeys.MainMenu, () => new MainMenuScene(sceneManager));
            sceneManager.AddScene(ProjectSceneKeys.OptionsMenu, () => new OptionsMenuScene(sceneManager));
            sceneManager.AddScene(ProjectSceneKeys.SelectPlayerScene, () => new SelectPlayerScene(sceneManager));
        }
    }
}
