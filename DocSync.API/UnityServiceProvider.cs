using Unity;

namespace DocSync.API
{
    public class UnityServiceProvider : IServiceProvider
    {
        private readonly IUnityContainer _container;

        public UnityServiceProvider(IUnityContainer container)
        {
            _container = container ?? throw new ArgumentNullException(nameof(container));
        }

        public object GetService(Type serviceType)
        {
            // Resolve the requested service from Unity container
            return _container.IsRegistered(serviceType) ? _container.Resolve(serviceType) : null;
        }
    }
}
