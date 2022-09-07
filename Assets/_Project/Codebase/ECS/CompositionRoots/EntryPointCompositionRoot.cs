using Cysharp.Threading.Tasks;
using FishingGame.ContentManagement;
using FishingGame.FactoryPattern;
using FishingGame.ModuleSystem;
using FishingGame.ModuleSystem.Modules;
using FishingGame.ServiceLayer;
using Svelto.Context;
using Svelto.ECS;
using Svelto.ECS.Schedulers;
using UnityEngine;

namespace FishingGame.ECS.CompositionRoots
{
    public sealed class EntryPointCompositionRoot : ICompositionRoot
    {
        private EnginesRoot _enginesRoot;
        
        public void OnContextCreated<T>(T contextHolder)
        {
            Application.targetFrameRate = 60;
            
            _enginesRoot = new EnginesRoot(new SimpleEntitiesSubmissionScheduler());
            
            ServiceLocator.Initialize();
            FactoryLocator.Initialize();
            
            BindServices();
            BindFactories();
            AddEngines();
        }

        public void OnContextInitialized<T>(T contextHolder)
        {
            var moduleLoader = ServiceLocator.Retrieve<IModuleService>().ModuleLoader;
            
            moduleLoader.LoadModuleAsync<MainMenuModule>().Forget();
        }

        public void OnContextDestroyed(bool hasBeenInitialised)
        {
            ServiceLocator.ClearBindings();
            FactoryLocator.ClearBindings();
        }

        private void BindServices()
        {
            ServiceLocator.Bind<IContentService, DefaultContentService>(
                new DefaultContentService(new DefaultContentLoader()));
            ServiceLocator.Bind<IModuleService, DefaultModuleService>(
                new DefaultModuleService(new DefaultModuleLoader()));
        }

        private void BindFactories()
        {
            ServiceLocator.Bind<IEntityFactory, IEntityFactory>(_enginesRoot.GenerateEntityFactory());
        }

        private void AddEngines()
        {
            
        }
    }
}