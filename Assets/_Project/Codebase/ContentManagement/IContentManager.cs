using Cysharp.Threading.Tasks;
using FishingGame.AsyncLoading;
using UnityEngine;

namespace FishingGame.ContentManagement
{
    public interface IContentManager : ILoaderAsync<ContentLoadPhase>
    {
        public UniTask<T> LoadContent<T>(string address) where T : Object;
        public T GetCachedContent<T>(in string address) where T : Object;
    }
}