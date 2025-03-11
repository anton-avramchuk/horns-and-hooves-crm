using HornsAndHoovesCrm.Authentication.Abstractions;

namespace HornsAndHoovesCrm.Authentication.Core;

public class AuthenticatedUser : IAuthenticatedUser
{
    public AuthenticatedUser(string id, string name, string? email, IReadOnlyCollection<string> roles,
        IReadOnlyCollection<string> permissions)
    {
        Id = id;
        Name = name;
        Email = email;
        Roles = roles;
        Permissions = permissions;
    }


    public AuthenticatedUser(string id, string name, string? email) : this(id, name, email, Array.Empty<string>(),
        Array.Empty<string>())
    {
    }


    public string Id { get; }
    public string Name { get; }
    public string? Email { get; }
    public IReadOnlyCollection<string> Roles { get; }
    public IReadOnlyCollection<string> Permissions { get; }
}