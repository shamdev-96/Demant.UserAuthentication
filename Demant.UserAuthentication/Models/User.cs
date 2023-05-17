
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demant.UserAuthentication.Models
{
    internal class User
    {
        public string LoginName { get; set; }
        public string Country { get; set; }
        public string DateOfBirth { get; set; }

        public Boolean IsOnline { get; set; }

    }
}
