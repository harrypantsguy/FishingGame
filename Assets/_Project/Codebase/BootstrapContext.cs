using FishingGame.ECS.CompositionRoots;
using Svelto.Context;

namespace FishingGame
{
    // Look at generic parameter to see composition implementation
    public sealed class BootstrapContext : UnityContext<EntryPointCompositionRoot> { }
}