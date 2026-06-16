using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LumiBeauty.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddSpecialistManagement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Bookings_SpecialistName_AppointmentDate",
                table: "Bookings");

            migrationBuilder.AlterColumn<string>(
                name: "SpecialistName",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "ServiceCategory",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "SpecialistId",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Specialists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Specialty = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rating = table.Column<decimal>(type: "decimal(2,1)", precision: 2, scale: 1, nullable: false),
                    SupportedCategories = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specialists", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Specialists",
                columns: new[] { "Id", "IsActive", "Name", "Rating", "Specialty", "SupportedCategories" },
                values: new object[,]
                {
                    { 1, true, "Elif Yılmaz", 4.9m, "Cilt bakımı uzmanı", "skin" },
                    { 2, true, "Derya Kaya", 4.8m, "Nail artist", "nails" },
                    { 3, true, "Melis Aksoy", 4.9m, "Kaş, kirpik ve kalıcı makyaj uzmanı", "brows,makeup" },
                    { 4, true, "Selin Demir", 4.7m, "Vücut bakımı ve epilasyon uzmanı", "body" },
                    { 5, true, "Ceren Arslan", 4.8m, "Saç bakım ve şekillendirme uzmanı", "hair" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_SpecialistId_AppointmentDate",
                table: "Bookings",
                columns: new[] { "SpecialistId", "AppointmentDate" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Specialists");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_SpecialistId_AppointmentDate",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "ServiceCategory",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "SpecialistId",
                table: "Bookings");

            migrationBuilder.AlterColumn<string>(
                name: "SpecialistName",
                table: "Bookings",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_SpecialistName_AppointmentDate",
                table: "Bookings",
                columns: new[] { "SpecialistName", "AppointmentDate" });
        }
    }
}
