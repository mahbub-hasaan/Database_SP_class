using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Transactions;
using Db_store.Models;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
namespace Db_store.Controllers
{
    public class HomeController : Controller
    {
        private dataContex DataContex= new dataContex();
        public ActionResult Index()
        {
            var user=DataContex.Users.SqlQuery("exec SelectAlluser").ToList<User>();
            return View(user);
        }

        public ActionResult About()
        {

            var accounts = this.DataContex.Accounts.ToList();
            return View(accounts);
        }

        public ActionResult Contact()
        {
               
            return View();
        }

        [HttpPost]
        public ActionResult Contact(string source,string dest,string amount)
        {
            SqlParameter Message=new SqlParameter("@Message", SqlDbType.NVarChar,200);
            Message.Direction = ParameterDirection.Output;          
            var data= DataContex.Database.SqlQuery<string>("EXEC Account_TransferBalance @SourceId,@DestId,@Amount,@Message OUT",
                                                                        new SqlParameter("SourceId",Convert.ToInt32(source)),
                                                                        new SqlParameter("DestId",Convert.ToInt32(dest)),
                                                                        new SqlParameter("Amount",Convert.ToSingle(amount)),
                                                                        Message).ToList();
            ViewBag.msg = (string) Message.Value;
            return View();
        }
    }
}