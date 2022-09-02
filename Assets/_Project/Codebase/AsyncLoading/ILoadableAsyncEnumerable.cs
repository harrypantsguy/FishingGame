using Cysharp.Threading.Tasks;

namespace FishingGame.AsyncLoading
{
    public interface ILoadableAsyncEnumerable<T> : ILoadable
    {
        public IUniTaskAsyncEnumerable<T> LoadAsyncEnumerable();
    }
}