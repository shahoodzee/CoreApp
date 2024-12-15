namespace Module.User.Core.ResponseDto;

public class LoginDto
{
    public string Token { get; set; }
    public bool isEmailConfirmed { get; set; } = false;
}
