using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Dima.Core.Requests.Account
{
    public class RegisterRequest
    {
        [Required(ErrorMessage ="E-mail")]
        [EmailAddress(ErrorMessage = "Incorrect E-mail")]
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
