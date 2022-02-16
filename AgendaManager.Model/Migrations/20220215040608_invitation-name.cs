using Microsoft.EntityFrameworkCore.Migrations;

namespace AgendaManager.Model.Migrations
{
    public partial class invitationname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Invitations",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Invitations");
        }
    }
}
