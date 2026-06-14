using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UdemyClone.Domain.Migrations
{
    /// <inheritdoc />
    public partial class InicializarEstadoActual : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
