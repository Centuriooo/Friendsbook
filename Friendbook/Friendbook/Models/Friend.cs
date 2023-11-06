using System;
using SQLite;

namespace Friendbook.Models
{
    public class Friend
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime Birthday { get; set; } = DateTime.Now;
        public DateTime Date { get; set; }
        public string Email { get; set; }

        public int Phone { get; set; }
        public string Note { get; set; }
    }
}

