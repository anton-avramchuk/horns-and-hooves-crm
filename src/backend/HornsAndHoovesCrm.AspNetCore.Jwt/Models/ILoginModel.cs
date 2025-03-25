namespace HornsAndHoovesCrm.AspNetCore.Jwt.Models;

public interface ILoginModel
{
    public string UserName { get; set; }

    public string Password { get; set; }
}

