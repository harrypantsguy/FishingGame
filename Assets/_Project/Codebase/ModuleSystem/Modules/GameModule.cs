using Cysharp.Threading.Tasks;
using FishingGame.Utilities;

namespace FishingGame.ModuleSystem.Modules
{
    public sealed class GameModule : IModule
    {
        private const string _GAME = "Game";
        
        public async UniTask LoadAsync()
        {
            SceneUtils.CreateScene(_GAME);

            await SceneUtils.SetActiveScene(_GAME);
            
            
        }

        public UniTask UnloadAsync()
        {
            return default;
        }

        public UniTask SetModuleActiveAsync()
        {
            return default;
        }
    }
}