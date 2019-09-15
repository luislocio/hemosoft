namespace HemoSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tipoSanguineoDoador : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Doadores", "FatorRh", c => c.Int());
            AddColumn("dbo.Doadores", "TipoSanguineo", c => c.Int());
            DropColumn("dbo.TriagensLaboratoriais", "FatorRh");
            DropColumn("dbo.TriagensLaboratoriais", "TipoSanguineo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TriagensLaboratoriais", "TipoSanguineo", c => c.Int());
            AddColumn("dbo.TriagensLaboratoriais", "FatorRh", c => c.Int());
            DropColumn("dbo.Doadores", "TipoSanguineo");
            DropColumn("dbo.Doadores", "FatorRh");
        }
    }
}
