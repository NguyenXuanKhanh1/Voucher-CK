namespace VoucherCK.SharedKernel.Interfaces
{
    public interface ISoftDelete : IDeletedBy, IDeletedDate
    {
        public bool IsDeleted { get; set; }
    }
}
