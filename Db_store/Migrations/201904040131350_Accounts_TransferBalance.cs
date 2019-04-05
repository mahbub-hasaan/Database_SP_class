namespace Db_store.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Accounts_TransferBalance : DbMigration
    {
        public override void Up()
        {
            CreateStoredProcedure(
                "dbo.Account_TransferBalance",
                p => new
                {
                    SourceId = p.Int(),
                    DestId = p.Int(),
                    Amount = p.Double(),
                    Message=p.String(maxLength:200,outParameter:true)
                },
                body:
                @"DECLARE @COUNT1 INT, @COUNT2 INT;
	            DECLARE @CurrentBalance FLOAT;
	            SELECT @CurrentBalance=balance FROM Accounts WHERE Id=@SourceId;
	            IF @CurrentBalance >= @Amount
	            BEGIN
		            BEGIN TRANSACTION
			            UPDATE Accounts SET BALANCE=BALANCE-@Amount WHERE id=@SourceId;
			            SET @COUNT1=@@ROWCOUNT
			            UPDATE Accounts SET BALANCE=BALANCE+@Amount WHERE id=@DestId;
			            SET @COUNT2=@@ROWCOUNT

			            IF @COUNT1=@COUNT2
			            BEGIN
				            COMMIT
				            SET @Message='AMOUNT HAS BEEN TRANFERRED';
			            END
			            ELSE
			            BEGIN 
				            ROLLBACK
				            SET @Message='AMOUNT TRANFERED FAILED';
			            END
	            END
	            ELSE
	            SET @Message='DO NOT HAVE ENOUGH FUND';"
            );
        }
        
        public override void Down()
        {
        }
    }
}
