using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace nihilitterApi.Migrations
{
    /// <inheritdoc />
    public partial class fourthMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "postDate",
                table: "NihilItems",
                newName: "PostDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PostDate",
                table: "NihilItems",
                newName: "postDate");
        }
    }
}
