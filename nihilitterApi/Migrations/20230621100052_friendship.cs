using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace nihilitterApi.Migrations
{
    /// <inheritdoc />
    public partial class friendship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserFriend_ApplicationUsers_FriendId",
                table: "UserFriend");

            migrationBuilder.CreateTable(
                name: "Friend",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: true),
                    FriendId = table.Column<long>(type: "bigint", nullable: true),
                    isConfirmed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Friend", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_UserFriend_Friend_FriendId",
                table: "UserFriend",
                column: "FriendId",
                principalTable: "Friend",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserFriend_Friend_FriendId",
                table: "UserFriend");

            migrationBuilder.DropTable(
                name: "Friend");

            migrationBuilder.AddForeignKey(
                name: "FK_UserFriend_ApplicationUsers_FriendId",
                table: "UserFriend",
                column: "FriendId",
                principalTable: "ApplicationUsers",
                principalColumn: "Id");
        }
    }
}
