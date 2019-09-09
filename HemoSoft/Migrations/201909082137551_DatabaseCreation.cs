namespace HemoSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DatabaseCreation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Doacoes",
                c => new
                    {
                        IdDoacao = c.Int(nullable: false),
                        DataDoacao = c.DateTime(nullable: false),
                        StatusDoacao = c.Int(nullable: false),
                        Doador_IdDoador = c.Int(),
                        Solicitacao_IdSolicitacao = c.Int(),
                        Triador_IdTriador = c.Int(),
                    })
                .PrimaryKey(t => t.IdDoacao)
                .ForeignKey("dbo.Doadores", t => t.Doador_IdDoador)
                .ForeignKey("dbo.ImpedimentosDefinitivos", t => t.IdDoacao)
                .ForeignKey("dbo.ImpedimentosTemporarios", t => t.IdDoacao)
                .ForeignKey("dbo.Solicitacoes", t => t.Solicitacao_IdSolicitacao)
                .ForeignKey("dbo.Triadores", t => t.Triador_IdTriador)
                .ForeignKey("dbo.TriagensClinicas", t => t.IdDoacao)
                .ForeignKey("dbo.TriagensLaboratoriais", t => t.IdDoacao)
                .Index(t => t.IdDoacao)
                .Index(t => t.Doador_IdDoador)
                .Index(t => t.Solicitacao_IdSolicitacao)
                .Index(t => t.Triador_IdTriador);
            
            CreateTable(
                "dbo.Doadores",
                c => new
                    {
                        IdDoador = c.Int(nullable: false, identity: true),
                        Cpf = c.String(),
                        EstadoCivil = c.Int(nullable: false),
                        NomeCompleto = c.String(),
                        Genero = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdDoador);
            
            CreateTable(
                "dbo.ImpedimentosDefinitivos",
                c => new
                    {
                        IdImpedimentosDefinitivos = c.Int(nullable: false, identity: true),
                        AntecedenteAvc = c.Boolean(),
                        HepatiteB = c.Boolean(),
                        HepatiteC = c.Boolean(),
                        Hiv = c.Boolean(),
                    })
                .PrimaryKey(t => t.IdImpedimentosDefinitivos);
            
            CreateTable(
                "dbo.ImpedimentosTemporarios",
                c => new
                    {
                        IdImpedimentosTemporarios = c.Int(nullable: false, identity: true),
                        BebidaAlcoolica = c.Boolean(nullable: false),
                        BebidaAlcoolicaUltimaVez = c.Int(nullable: false),
                        Gravidez = c.Boolean(nullable: false),
                        GravidezUltimaVez = c.Int(nullable: false),
                        Gripe = c.Boolean(nullable: false),
                        GripeUltimaVez = c.Int(nullable: false),
                        Tatuagem = c.Boolean(nullable: false),
                        TatuagemUltimaVez = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdImpedimentosTemporarios);
            
            CreateTable(
                "dbo.Solicitacoes",
                c => new
                    {
                        IdSolicitacao = c.Int(nullable: false, identity: true),
                        DataSolicitacao = c.DateTime(nullable: false),
                        Solicitante_IdSolicitante = c.Int(),
                    })
                .PrimaryKey(t => t.IdSolicitacao)
                .ForeignKey("dbo.Solicitantes", t => t.Solicitante_IdSolicitante)
                .Index(t => t.Solicitante_IdSolicitante);
            
            CreateTable(
                "dbo.Solicitantes",
                c => new
                    {
                        IdSolicitante = c.Int(nullable: false, identity: true),
                        Cnpj = c.String(),
                        RazaoSocial = c.String(),
                        Responsavel = c.String(),
                        Senha = c.String(),
                        StatusUsuario = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdSolicitante);
            
            CreateTable(
                "dbo.Triadores",
                c => new
                    {
                        IdTriador = c.Int(nullable: false, identity: true),
                        Matricula = c.String(),
                        NomeCompleto = c.String(),
                        Senha = c.String(),
                        StatusUsuario = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdTriador);
            
            CreateTable(
                "dbo.TriagensClinicas",
                c => new
                    {
                        IdTriagemClinica = c.Int(nullable: false, identity: true),
                        Peso = c.Double(nullable: false),
                        Pulso = c.Int(nullable: false),
                        StatusTriagem = c.Int(nullable: false),
                        Temperatura = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.IdTriagemClinica);
            
            CreateTable(
                "dbo.TriagensLaboratoriais",
                c => new
                    {
                        IdTriagemLaboratorial = c.Int(nullable: false, identity: true),
                        FatorRh = c.Int(),
                        HepatiteB = c.Boolean(),
                        HepatiteC = c.Boolean(),
                        Hiv = c.Boolean(),
                        StatusTriagem = c.Int(),
                        TipoSanguineo = c.Int(),
                    })
                .PrimaryKey(t => t.IdTriagemLaboratorial);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Doacoes", "IdDoacao", "dbo.TriagensLaboratoriais");
            DropForeignKey("dbo.Doacoes", "IdDoacao", "dbo.TriagensClinicas");
            DropForeignKey("dbo.Doacoes", "Triador_IdTriador", "dbo.Triadores");
            DropForeignKey("dbo.Solicitacoes", "Solicitante_IdSolicitante", "dbo.Solicitantes");
            DropForeignKey("dbo.Doacoes", "Solicitacao_IdSolicitacao", "dbo.Solicitacoes");
            DropForeignKey("dbo.Doacoes", "IdDoacao", "dbo.ImpedimentosTemporarios");
            DropForeignKey("dbo.Doacoes", "IdDoacao", "dbo.ImpedimentosDefinitivos");
            DropForeignKey("dbo.Doacoes", "Doador_IdDoador", "dbo.Doadores");
            DropIndex("dbo.Solicitacoes", new[] { "Solicitante_IdSolicitante" });
            DropIndex("dbo.Doacoes", new[] { "Triador_IdTriador" });
            DropIndex("dbo.Doacoes", new[] { "Solicitacao_IdSolicitacao" });
            DropIndex("dbo.Doacoes", new[] { "Doador_IdDoador" });
            DropIndex("dbo.Doacoes", new[] { "IdDoacao" });
            DropTable("dbo.TriagensLaboratoriais");
            DropTable("dbo.TriagensClinicas");
            DropTable("dbo.Triadores");
            DropTable("dbo.Solicitantes");
            DropTable("dbo.Solicitacoes");
            DropTable("dbo.ImpedimentosTemporarios");
            DropTable("dbo.ImpedimentosDefinitivos");
            DropTable("dbo.Doadores");
            DropTable("dbo.Doacoes");
        }
    }
}
