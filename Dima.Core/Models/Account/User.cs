using System;
using System.Collections.Generic;
using System.Text;

namespace Dima.Core.Models.Account
{
    public class User
    {
        public string Email { get; set; } = string.Empty;
        public Dictionary<string, string> Claims { get; set; } = [];
    }
}
