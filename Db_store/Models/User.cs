using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Db_store.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserPhone { get; set; }
    }
}