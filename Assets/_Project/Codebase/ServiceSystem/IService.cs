using System;
using System.Collections.Generic;

namespace FishingGame.ServiceSystem
{
    public interface IService
    {
        private static readonly Dictionary<Type, IService> _Services = new Dictionary<Type, IService>();

        public static void AddService(in IService service)
        {
            var type = service.GetType();

            if (_Services.ContainsKey(type))
                throw new Exception("Cannot inject multiple of the same service.");
            
            _Services.Add(type, service);
        }

        public static void RemoveService<T>() where T : IService
        {
            var type = typeof(T);

            if (!_Services.ContainsKey(type))
                throw new Exception("Cannot remove service that was never added.");
            
            _Services.Remove(type);
        }

        public static T Get<T>() where T : class, IService
        {
            return _Services[typeof(T)] as T;
        }

        public static void ClearServices()
        {
            _Services.Clear();
        }
    }
}