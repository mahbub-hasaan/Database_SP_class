namespace Db_store.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Accounts_GetAllAccounts : DbMigration
    {
        public override void Up()
        {
            CreateStoredProcedure(
                "dbo.Account_GetAllAccounts",
                p => new
                {
                },
                body:
                @"Select * from Accounts inner join
                  Users on Accounts.UserId=Users.Id"
                );
        }
        
        public override void Down()
        {
        }
    }
}
