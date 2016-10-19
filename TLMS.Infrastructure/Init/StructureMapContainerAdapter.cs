using System;
using ServiceStack.Configuration;
using ServiceStack.Logging;
using StructureMap;

namespace TLMS.Infrastructure.Init
{
    public class StructureMapContainerAdapter : IContainerAdapter
    {
        private static readonly ILog _logger = LogManager.GetLogger("StructureMapContainerAdapter");
        private readonly Container _container;

        public StructureMapContainerAdapter(Container container)
        {
            _container = container;
        }

        public T TryResolve<T>()
        {
            try
            {
                var nestedContainer = _container.GetNestedContainer();

                T instance;
                if (nestedContainer != null)
                {
                    instance = nestedContainer.TryGetInstance<T>();
                }
                else
                {
                    instance = _container.TryGetInstance<T>();
                }

                return instance;
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
                throw;
            }
        }

        public T Resolve<T>()
        {
            return _container.GetNestedContainer().GetInstance<T>();
        }
    }

}
