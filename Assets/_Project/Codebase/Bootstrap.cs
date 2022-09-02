using Cysharp.Threading.Tasks;
using FishingGame.ContentManagement;
using FishingGame.FactoryPattern;
using FishingGame.ModuleSystem;
using FishingGame.ModuleSystem.Modules;
using FishingGame.ServiceLayer;
using JetBrains.Annotations;
using UnityEngine;

namespace FishingGame
{
    public sealed class Bootstrap : MonoBehaviour
    {
        [UsedImplicitly]
        private async UniTaskVoid Start()
        {
            Application.targetFrameRate = 60;
            
            ServiceLocator.Initialize();
            FactoryLocator.Initialize();
            
            ServiceLocator.Bind<IContentService, DefaultContentService>(
                new DefaultContentService(new DefaultContentLoader()));
            ServiceLocator.Bind<IModuleService, DefaultModuleService>(
                new DefaultModuleService(new DefaultModuleLoader()));

            var moduleLoader = ServiceLocator.Retrieve<IModuleService>().ModuleLoader;
            
            await moduleLoader.LoadModuleAsync<MainMenuModule>();
        }

        [UsedImplicitly]
        private void OnDestroy()
        {
            ServiceLocator.ClearBindings();
            FactoryLocator.ClearBindings();
        }
    }
}