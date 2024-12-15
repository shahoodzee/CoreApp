namespace Shared.Models.Contracts;

public class AccountVerificationContract
{
    public string name {  get; set; }
    public string email {  get; set; }
    public string confirmationLink { get; set; }
}
