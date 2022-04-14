using System;
using System.Collections.Generic;

#nullable disable

namespace CRUD_Core.Db_Context
{
    public partial class Logiuse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
