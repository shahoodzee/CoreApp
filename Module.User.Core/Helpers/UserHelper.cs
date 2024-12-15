using Microsoft.EntityFrameworkCore;
using Module.User.Core.Abstraction;
using Module.User.Core.Entities;

namespace Module.User.Core.Helpers;

public class UserHelper : IUserHelper
{
    private readonly IUserDbContext dbContext;
    public UserHelper(IUserDbContext _dbContext)
    {
        dbContext = _dbContext;
    }

    public async Task<int> GetCompanyId(string email, int iCompanyId, string companyName)
    {
        try
        {
            if (iCompanyId != -1)
            {
                return iCompanyId;
            }

            else if (!String.IsNullOrEmpty(companyName))
            {
                var companyObj = await dbContext.ApplicationCompanies.Where(c => c.CompanyName == companyName).FirstOrDefaultAsync();
                if (companyObj != null)
                {
                    return companyObj.Id;
                }

                ApplicationCompany objCompany = new();
                objCompany.CompanyName = companyName;
                objCompany.isActive = true;
                objCompany.CreatedDate = DateTime.UtcNow;
                objCompany.iCreatedBy = 1;
                var result = await dbContext.ApplicationCompanies.AddAsync(objCompany);
                await dbContext.SaveChangesAsync(CancellationToken.None);
                return result.Entity.Id;
            }

            else
            {
                string[] nonBusinessDomains = { "gmail.com", "outlook.com", "yahoo.com" }; // Non-business domains
                string domain = GetDomainFromEmail(email);
                if (Array.IndexOf(nonBusinessDomains, domain) == -1)
                {
                    int atIndex = email.IndexOf('@');
                    string temp = email.Substring(atIndex + 1, email.Length - atIndex - 1);
                    var company = temp.Split(".")[0];

                    var companyDb = await dbContext.ApplicationCompanies.Where(c => c.CompanyName == company).FirstOrDefaultAsync();
                    if (companyDb != null)
                    {
                        return companyDb.Id;
                    }

                    ApplicationCompany objCompany = new();
                    objCompany.CompanyName = company;
                    objCompany.isActive = true;
                    objCompany.CreatedDate = DateTime.UtcNow;
                    objCompany.iCreatedBy = 1;
                    var result = await dbContext.ApplicationCompanies.AddAsync(objCompany);
                    await dbContext.SaveChangesAsync(CancellationToken.None);
                    return result.Entity.Id;
                }
                else
                {
                    // By default, set company to Private
                    return 1;
                }
            }
        }
        catch (Exception ex)
        {
            return -1;
        }
    }


    public string GetDomainFromEmail(string email)
    {
        int atIndex = email.IndexOf('@');
        if (atIndex != -1)
        {
            return email.Substring(atIndex + 1);
        }
        return null;
    }
}
