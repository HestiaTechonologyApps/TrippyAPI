using System.Security.Claims;
using Trippy.Domain.DTO;
using Trippy.Domain.Entities;

public interface IJwtService
{
    string GenerateToken(UserDTO user);
    ClaimsPrincipal? ValidateToken(string token);

}
