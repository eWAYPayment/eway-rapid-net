namespace eWAY.Rapid.Internals.Services
{
    internal interface IMappingService 
    {
        TDest Map<TSource, TDest>(TSource obj);
    }
}
