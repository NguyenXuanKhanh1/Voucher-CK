namespace ChargeHub.SharedKernel.Interfaces;

public interface IMultilingualData<T> where T : class
{
    public IDictionary<string, T?>? MultilingualData { get; set; }
    
}