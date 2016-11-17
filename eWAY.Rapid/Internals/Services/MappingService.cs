using AutoMapper;

namespace eWAY.Rapid.Internals.Services
{
    internal class MappingService: IMappingService
    {

        private static bool _mappingsHaveBeenRegistered = false;
        private static readonly object _registerMappingsMutex = new object();

        public MappingService()
        {
            RegisterMapping();
        }

        public TDest Map<TSource, TDest>(TSource obj)
        {
            return Mapper.Map<TSource, TDest>(obj);
        }

        public static void RegisterMapping()
        {
            if (_mappingsHaveBeenRegistered) return;
            lock (_registerMappingsMutex)
            {
                if (!_mappingsHaveBeenRegistered)
                {
                    Mapper.Initialize(
                    cfg =>
                    {
                        cfg.CreateMissingTypeMaps = true;
                        cfg.AddProfile(new MappingProfile());
                    });
                    Mapper.AssertConfigurationIsValid();
                    _mappingsHaveBeenRegistered = true;
                }
            }
        }

    }
}
