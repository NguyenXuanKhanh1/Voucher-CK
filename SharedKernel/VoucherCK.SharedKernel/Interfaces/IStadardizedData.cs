namespace VoucherCK.SharedKernel.Interfaces;

public interface IStandardizedData
{
    public IDictionary<string, object?>? StandardizedData { get; set; }
}