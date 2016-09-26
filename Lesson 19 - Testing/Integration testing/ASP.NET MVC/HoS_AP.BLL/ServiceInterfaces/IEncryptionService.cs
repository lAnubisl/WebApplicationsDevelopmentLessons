namespace HoS_AP.BLL.ServiceInterfaces
{
    internal interface IEncryptionService
    {
        bool IsValidPassword(string password, string correctHash);
    }
}