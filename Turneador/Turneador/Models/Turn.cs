using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Turneador.Models
{
    public class Turn
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }
        public String SelectedDate { get; set; }
        public String Process { get; set; }

        [ForeignKey(typeof(User))]
        public int UserId { get; set; }
        [ManyToOne]
        public User User { get; set; }
    }
}
