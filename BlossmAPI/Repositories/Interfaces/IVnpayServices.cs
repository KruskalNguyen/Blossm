namespace BlossmAPI.Repositories.Interfaces
{
    public interface IVnpayServices
    {
        public Task<string> CreatePaymentUrl(int id);
    }
}
