using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Db_store.Models;
namespace Db_store.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string AccountType { get; set; }
        public double Balance { get; set; }
        public virtual User User { get; set; }
    }
}