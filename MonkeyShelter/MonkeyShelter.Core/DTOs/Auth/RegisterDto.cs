using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonkeyShelter.Core.DTOs.Auth
{
    public class RegisterDto
    {
        public string Username { get; set; } = null!;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = null!;
        public string Role { get; set; } = string.Empty;
    }
}
