using Microsoft.EntityFrameworkCore.Migrations;

namespace Sat.Recruitment.Persistence.Migrations
{
    public partial class inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    Email = table.Column<string>(maxLength: 100, nullable: false),
                    Address = table.Column<string>(nullable: false),
                    Phone = table.Column<string>(maxLength: 15, nullable: false),
                    UserType = table.Column<string>(nullable: true),
                    Money = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "Email", "Money", "Name", "Phone", "UserType" },
                values: new object[] { 1, "Peru 2464", "Juan@marmol.com", 1234m, "Juan", "+5491154762312", "Normal" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "Email", "Money", "Name", "Phone", "UserType" },
                values: new object[] { 2, "Alvear y Colombres", "Franco.Perez@gmail.com", 112234m, "Franco", "+534645213542", "Premium" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "Email", "Money", "Name", "Phone", "UserType" },
                values: new object[] { 3, "Garay y Otra Calle", "Agustina@gmail.com", 112234m, "Agustina", "+534645213542", "SuperUser" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
