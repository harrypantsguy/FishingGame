using FishingGame.DependencyInjection;

namespace FishingGame.FactoryPattern
{
    public static class FactoryLocator
    {
        private static DependencyContainer _container;

        public static void Initialize() => _container = new DependencyContainer();

        public static void Bind<I, T>(in T implementation) where T : I => _container.Bind<I, T>(implementation);

        public static void Retrieve<I>() => _container.Resolve<I>();

        public static void ClearBindings() => _container.ClearBindings();
    }
}