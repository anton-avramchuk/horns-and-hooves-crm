using HornsAndHoovesCrm.AspNetCore.Jwt.Models;

namespace HornsAndHoovesCrm.AspNetCore.Jwt.Services;

public interface ILoginService<in TLoginModel> where TLoginModel : ILoginModel
{
    Task<LoginResult?> LoginAsync(TLoginModel loginModel);
}