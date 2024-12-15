namespace Module.User.Core.Helpers;

public interface IUserHelper
{
    Task<int> GetCompanyId(string email, int iCompanyId, string companyName);
    string GetDomainFromEmail(string email);
}
