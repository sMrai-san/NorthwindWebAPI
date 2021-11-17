using System;
using System.Collections.Generic;

namespace WebApi.Models
{
    public partial class Logins
    {
        public int LoginId { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public string Email { get; set; }
        public Nullable<int> AccesLevelID { get; set; }
    }
}
