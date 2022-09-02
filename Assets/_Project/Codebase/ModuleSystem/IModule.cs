using Cysharp.Threading.Tasks;

namespace FishingGame.ModuleSystem
{
    public interface IModule
    {
        public UniTask LoadAsync();
        public UniTask UnloadAsync();
        public UniTask SetModuleActiveAsync();
    }
}