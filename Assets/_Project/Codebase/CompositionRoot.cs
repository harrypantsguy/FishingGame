using FishingGame.ContentManagement;
using FishingGame.FactoryPattern;
using FishingGame.ModuleSystem;
using FishingGame.ModuleSystem.Modules;
using FishingGame.ServiceLayer;
using Svelto.Context;
using UnityEngine;

namespace FishingGame
{
    public sealed class CompositionRoot : ICompositionRoot
    {
        public void OnContextCreated<T>(T contextHolder)
        {
        }

        public async void OnContextInitialized<T>(T contextHolder)
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

        public void OnContextDestroyed(bool hasBeenInitialised)
        {
            ServiceLocator.ClearBindings();
            FactoryLocator.ClearBindings();
        }
    }
}