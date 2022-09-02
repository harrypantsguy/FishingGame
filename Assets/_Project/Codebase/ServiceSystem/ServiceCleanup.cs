using UnityEngine;

namespace FishingGame.ServiceSystem
{
    public sealed class ServiceCleanup : MonoBehaviour
    {
        private void OnDestroy()
        {
            IService.ClearServices();
        }
    }
}