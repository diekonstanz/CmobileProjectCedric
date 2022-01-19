using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace MobileProjectJuist3.Models
{
    public class Crystal
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }

    }
}
