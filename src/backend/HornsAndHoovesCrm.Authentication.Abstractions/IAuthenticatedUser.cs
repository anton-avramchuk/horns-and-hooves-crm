namespace HornsAndHoovesCrm.Authentication.Abstractions;

public interface IAuthenticatedUser
{
    string Id { get; }
    
    string Name { get; }
    
    string? Email { get; }
    
    IReadOnlyCollection<string> Roles { get; }
    
    IReadOnlyCollection<string> Permissions { get; }
}