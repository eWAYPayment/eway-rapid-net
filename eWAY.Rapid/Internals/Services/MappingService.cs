using AutoMapper;
using eWAY.Rapid.Internals.Mappings;

namespace eWAY.Rapid.Internals.Services
{
    internal class MappingService: IMappingService
    {
        private static readonly MapperConfiguration config;

        static MappingService()
        {

            config = new MapperConfiguration(c =>
            {
                #region CustomMapProfile

                c.AddProfile<CustomMapProfile>();

                #endregion

                #region EntitiesMapProfile
                
                c.AddProfile<EntitiesMapProfile>();

                #endregion

                #region RequestMapProfile
               
                c.AddProfile<RequestMapProfile>();

                #endregion

                #region ResponseMapProfile             

                c.AddProfile<ResponseMapProfile>();

                #endregion

            }
            );

        }

        public MappingService()
        {
        }

        public TDest Map<TSource, TDest>(TSource obj)
        {
            return config.CreateMapper().Map<TSource, TDest>(obj);
        }
  
    }
}
