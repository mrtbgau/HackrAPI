using API.DTO;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace API.Services.JWT
{
    public interface IJWTService
    {
        User? Login(LoginDTO loginDTO);
        string GenerateToken(string secret, List<Claim> claims);
    }
}