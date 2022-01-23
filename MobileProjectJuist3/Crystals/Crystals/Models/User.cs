using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Crystals.Models
{
    [Table("Notes")]
    public class User
    {
        
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

       
        public string Name { get; set; }

        [Unique]
        public string Email { get; set; }

        public string Password { get; set; }
        public string Salt { get; set; }

    }
}
