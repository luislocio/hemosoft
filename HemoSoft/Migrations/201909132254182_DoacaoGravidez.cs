namespace HemoSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DoacaoGravidez : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ImpedimentosTemporarios", "BebidaAlcoolica", c => c.Boolean());
            AlterColumn("dbo.ImpedimentosTemporarios", "BebidaAlcoolicaUltimaVez", c => c.Int());
            AlterColumn("dbo.ImpedimentosTemporarios", "Gravidez", c => c.Int(nullable: false));
            AlterColumn("dbo.ImpedimentosTemporarios", "GravidezUltimaVez", c => c.Int());
            AlterColumn("dbo.ImpedimentosTemporarios", "Gripe", c => c.Boolean());
            AlterColumn("dbo.ImpedimentosTemporarios", "GripeUltimaVez", c => c.Int());
            AlterColumn("dbo.ImpedimentosTemporarios", "Tatuagem", c => c.Boolean());
            AlterColumn("dbo.ImpedimentosTemporarios", "TatuagemUltimaVez", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ImpedimentosTemporarios", "TatuagemUltimaVez", c => c.Int(nullable: false));
            AlterColumn("dbo.ImpedimentosTemporarios", "Tatuagem", c => c.Boolean(nullable: false));
            AlterColumn("dbo.ImpedimentosTemporarios", "GripeUltimaVez", c => c.Int(nullable: false));
            AlterColumn("dbo.ImpedimentosTemporarios", "Gripe", c => c.Boolean(nullable: false));
            AlterColumn("dbo.ImpedimentosTemporarios", "GravidezUltimaVez", c => c.Int(nullable: false));
            AlterColumn("dbo.ImpedimentosTemporarios", "Gravidez", c => c.Boolean(nullable: false));
            AlterColumn("dbo.ImpedimentosTemporarios", "BebidaAlcoolicaUltimaVez", c => c.Int(nullable: false));
            AlterColumn("dbo.ImpedimentosTemporarios", "BebidaAlcoolica", c => c.Boolean(nullable: false));
        }
    }
}
