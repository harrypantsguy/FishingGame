using Cysharp.Threading.Tasks;
using FishingGame.ContentManagement;
using FishingGame.FactoryPattern;
using FishingGame.ModuleSystem;
using FishingGame.ModuleSystem.Modules;
using FishingGame.ServiceLayer;
using Svelto.Context;
using UnityEngine;

namespace FishingGame.ECS.CompositionRoots
{
    public sealed class EntryPointCompositionRoot : ICompositionRoot
    {
        public void OnContextCreated<T>(T contextHolder)
        {
            
        }

        public void OnContextInitialized<T>(T contextHolder)
        {
            Application.targetFrameRate = 60;
            
            ServiceLocator.Initialize();
            FactoryLocator.Initialize();
            
            ServiceLocator.Bind<IContentService, DefaultContentService>(
                new DefaultContentService(new DefaultContentLoader()));
            ServiceLocator.Bind<IModuleService, DefaultModuleService>(
                new DefaultModuleService(new DefaultModuleLoader()));

            var moduleLoader = ServiceLocator.Retrieve<IModuleService>().ModuleLoader;
            
            moduleLoader.LoadModuleAsync<MainMenuModule>().Forget();
        }

        public void OnContextDestroyed(bool hasBeenInitialised)
        {
            ServiceLocator.ClearBindings();
            FactoryLocator.ClearBindings();
        }
    }
}