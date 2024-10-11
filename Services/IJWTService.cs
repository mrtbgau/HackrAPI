using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace API.Services
{
    public interface IJWTService
    {
        User Login(string email, string password);
        string GenerateToken(string secret, List<Claim> claims);
    }
}