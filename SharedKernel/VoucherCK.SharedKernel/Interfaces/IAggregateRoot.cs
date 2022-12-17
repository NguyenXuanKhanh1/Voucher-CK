namespace VoucherCK.SharedKernel.Interfaces
{
    public interface IAggregateRoot : IEntityIdString, IModifiedBy, IModifiedDate, ICreatedDate, ICreatedBy, ISoftDelete
    {
    }
}
