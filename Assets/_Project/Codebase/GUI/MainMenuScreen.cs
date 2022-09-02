using Cysharp.Threading.Tasks;
using FishingGame.ModuleSystem;
using FishingGame.ModuleSystem.Modules;
using FishingGame.ServiceLayer;
using UnityEngine;
using UnityEngine.UI;

namespace FishingGame.GUI
{
    public sealed class MainMenuScreen : MonoBehaviour
    {
        [SerializeField] private Button _playButton;

        private void Start()
        {
            _playButton.onClick.AddListener(() => OnPlayButtonClicked());
        }

        private async UniTaskVoid OnPlayButtonClicked()
        {
            var moduleLoader = ServiceLocator.Retrieve<IModuleService>().ModuleLoader;

            await moduleLoader.UnloadModuleAsync<MainMenuModule>();
            await moduleLoader.LoadModuleAsync<GameModule>();
        }
    }
}