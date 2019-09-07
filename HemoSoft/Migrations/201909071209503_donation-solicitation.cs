namespace HemoSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class donationsolicitation : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Doacoes", "IdDoacao", "dbo.Solicitacoes");
            AddColumn("dbo.Doacoes", "Solicitacao_IdSolicitacao", c => c.Int());
            CreateIndex("dbo.Doacoes", "Solicitacao_IdSolicitacao");
            AddForeignKey("dbo.Doacoes", "Solicitacao_IdSolicitacao", "dbo.Solicitacoes", "IdSolicitacao");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Doacoes", "Solicitacao_IdSolicitacao", "dbo.Solicitacoes");
            DropIndex("dbo.Doacoes", new[] { "Solicitacao_IdSolicitacao" });
            DropColumn("dbo.Doacoes", "Solicitacao_IdSolicitacao");
            AddForeignKey("dbo.Doacoes", "IdDoacao", "dbo.Solicitacoes", "IdSolicitacao");
        }
    }
}
