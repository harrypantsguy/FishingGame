using Cysharp.Threading.Tasks;

namespace FishingGame.AsyncLoading
{
    public interface ILoadableAsync<T> : ILoadable
    {
        public UniTask<T> LoadAsync();
    }
}