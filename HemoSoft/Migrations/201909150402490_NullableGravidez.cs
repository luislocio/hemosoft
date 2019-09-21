namespace HemoSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NullableGravidez : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ImpedimentosTemporarios", "Gravidez", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ImpedimentosTemporarios", "Gravidez", c => c.Int(nullable: false));
        }
    }
}
