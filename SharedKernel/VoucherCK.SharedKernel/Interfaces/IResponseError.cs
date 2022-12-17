namespace VoucherCK.SharedKernel.Interfaces
{
    public interface IResponseError
    {
        string GetName();
        int GetStatus();

        int GetCode();

        string GetMessage();
    }

}
