namespace Db_store.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Relationship : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Accounts", "UserId", "dbo.Users");
            DropIndex("dbo.Accounts", new[] { "UserId" });
            RenameColumn(table: "dbo.Accounts", name: "UserId", newName: "User_Id");
            AlterColumn("dbo.Accounts", "User_Id", c => c.Int());
            CreateIndex("dbo.Accounts", "User_Id");
            AddForeignKey("dbo.Accounts", "User_Id", "dbo.Users", "Id");
            AlterStoredProcedure(
                "dbo.Account_Insert",
                p => new
                    {
                        AccountType = p.String(),
                        Balance = p.Double(),
                        User_Id = p.Int(),
                    },
                body:
                    @"INSERT [dbo].[Accounts]([AccountType], [Balance], [User_Id])
                      VALUES (@AccountType, @Balance, @User_Id)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[Accounts]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[Accounts] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            AlterStoredProcedure(
                "dbo.Account_Update",
                p => new
                    {
                        Id = p.Int(),
                        AccountType = p.String(),
                        Balance = p.Double(),
                        User_Id = p.Int(),
                    },
                body:
                    @"UPDATE [dbo].[Accounts]
                      SET [AccountType] = @AccountType, [Balance] = @Balance, [User_Id] = @User_Id
                      WHERE ([Id] = @Id)"
            );
            
            AlterStoredProcedure(
                "dbo.Account_Delete",
                p => new
                    {
                        Id = p.Int(),
                        User_Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[Accounts]
                      WHERE (([Id] = @Id) AND (([User_Id] = @User_Id) OR ([User_Id] IS NULL AND @User_Id IS NULL)))"
            );
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Accounts", "User_Id", "dbo.Users");
            DropIndex("dbo.Accounts", new[] { "User_Id" });
            AlterColumn("dbo.Accounts", "User_Id", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Accounts", name: "User_Id", newName: "UserId");
            CreateIndex("dbo.Accounts", "UserId");
            AddForeignKey("dbo.Accounts", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            throw new NotSupportedException("Scaffolding create or alter procedure operations is not supported in down methods.");
        }
    }
}
