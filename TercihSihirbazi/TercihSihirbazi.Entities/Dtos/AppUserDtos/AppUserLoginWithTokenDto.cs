using TercihSihirbazi.Entities.Interfaces;

namespace TercihSihirbazi.Entities.Dtos.AppUserDtos;
public class AppUserLoginWithTokenDto : IDto
{
    public UserData userData { get; set; }
    public string accessToken { get; set; }
    public string refreshToken { get; set; }
}

public class UserData
{
    public string UserName { get; set; }
    public string NickName { get; set; }
    public AppUserAbilityDto ability { get; set; }
    public string role { get; set; }

}
