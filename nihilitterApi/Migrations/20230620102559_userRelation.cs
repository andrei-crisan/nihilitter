using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace nihilitterApi.Migrations
{
    /// <inheritdoc />
    public partial class userRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "NihilItems",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_NihilItems_UserId",
                table: "NihilItems",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_NihilItems_ApplicationUsers_UserId",
                table: "NihilItems",
                column: "UserId",
                principalTable: "ApplicationUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NihilItems_ApplicationUsers_UserId",
                table: "NihilItems");

            migrationBuilder.DropIndex(
                name: "IX_NihilItems_UserId",
                table: "NihilItems");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "NihilItems");
        }
    }
}
