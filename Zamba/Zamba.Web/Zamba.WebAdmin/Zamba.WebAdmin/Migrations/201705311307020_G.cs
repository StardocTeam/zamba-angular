namespace Zamba.WebAdmin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class G : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.USERADMIN",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        idadmin = c.Int(nullable: false),
                        nombre = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.USERPARAM",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        idparam = c.Int(nullable: false),
                        nombre = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.USRNOTES",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        nombre = c.String(),
                        conf_mailserver = c.String(),
                        conf_basemail = c.String(),
                        conf_patharch = c.String(),
                        conf_vistaexportacion = c.String(),
                        Conf_Papelera = c.String(),
                        Conf_Nomarchtxt = c.String(),
                        Conf_seqatt = c.Int(nullable: false),
                        conf_lockeo = c.Int(nullable: false),
                        conf_acumimg = c.Int(nullable: false),
                        conf_limimg = c.Int(nullable: false),
                        conf_destext = c.Int(nullable: false),
                        conf_textosubject = c.String(),
                        Conf_Borrar = c.String(),
                        conf_archctrl = c.String(),
                        conf_schedulesel = c.Int(nullable: false),
                        conf_schedulevar = c.Int(nullable: false),
                        conf_ejecutable = c.String(),
                        conf_nomusernotes = c.String(),
                        conf_nomuserred = c.String(),
                        conf_charsreempsubj = c.Int(nullable: false),
                        conf_reintento = c.Int(nullable: false),
                        activo = c.Int(nullable: false),
                        conf_seqimg = c.Int(nullable: false),
                        conf_bodyandattachsinexportedmails = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.USRNOTES");
            DropTable("dbo.USERPARAM");
            DropTable("dbo.USERADMIN");
        }
    }
}
