using Cysharp.Threading.Tasks;
using FishingGame.ContentManagement;
using FishingGame.ServiceLayer;
using FishingGame.Utilities;
using UnityEngine;

namespace FishingGame.ModuleSystem.Modules
{
    public sealed class MainMenuModule : IModule
    {
        private const string _MAIN_MENU = "MainMenu";
        private const string _MAIN_MENU_CANVAS_ADDRESS = ContentAddresses.Prefabs.GUI.MAIN_MENU_CANVAS;
        
        public async UniTask LoadAsync()
        {
            SceneUtils.CreateScene(_MAIN_MENU);

            await SceneUtils.SetActiveScene(_MAIN_MENU);

            var contentLoader = ServiceLocator.Retrieve<IContentService>().ContentLoader;

            contentLoader.EnqueueLoadable(new ContentLoadPhase("GUI", 
                ContentAddresses.GetAssetAddressesInType(typeof(ContentAddresses.Prefabs.GUI)).ToAddressables()));

            await contentLoader.ProcessQueue();
            
            var mainMenuCanvasPrefab = contentLoader.GetCachedContent<GameObject>(_MAIN_MENU_CANVAS_ADDRESS);
            var mainMenuCanvasObj = Object.Instantiate(mainMenuCanvasPrefab);
        }

        public async UniTask UnloadAsync()
        {
            await SceneUtils.UnloadSceneAsync(_MAIN_MENU);
        }

        public async UniTask SetModuleActiveAsync()
        {
            await SceneUtils.SetActiveScene(_MAIN_MENU);
        }
    }
}