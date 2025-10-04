using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Models.Shared
{
    public class ApplicationUser:Microsoft.AspNetCore.Identity.IdentityUser
    {
        public string FirstName {get; set; } = null!;
        public string LastName {get; set; } = null!;
}
}
