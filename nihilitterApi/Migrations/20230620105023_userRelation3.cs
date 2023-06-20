using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace nihilitterApi.Migrations
{
    /// <inheritdoc />
    public partial class userRelation3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUsers_ApplicationUsers_UserId",
                table: "ApplicationUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_ApplicationUsers_ToId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_ApplicationUsers_UserId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_NihilItems_ApplicationUsers_UserId",
                table: "NihilItems");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationUsers_UserId",
                table: "ApplicationUsers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ApplicationUsers");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Messages",
                newName: "ToId1");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_UserId",
                table: "Messages",
                newName: "IX_Messages_ToId1");

            migrationBuilder.AlterColumn<string>(
                name: "Post",
                table: "NihilItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MessageBody",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "ApplicationUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "ApplicationUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "UserFriend",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    FriendId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFriend", x => new { x.UserId, x.FriendId });
                    table.ForeignKey(
                        name: "FK_UserFriend_ApplicationUsers_FriendId",
                        column: x => x.FriendId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserFriend_ApplicationUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserFriend_FriendId",
                table: "UserFriend",
                column: "FriendId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_ApplicationUsers_ToId",
                table: "Messages",
                column: "ToId",
                principalTable: "ApplicationUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_ApplicationUsers_ToId1",
                table: "Messages",
                column: "ToId1",
                principalTable: "ApplicationUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NihilItems_ApplicationUsers_UserId",
                table: "NihilItems",
                column: "UserId",
                principalTable: "ApplicationUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_ApplicationUsers_ToId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_ApplicationUsers_ToId1",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_NihilItems_ApplicationUsers_UserId",
                table: "NihilItems");

            migrationBuilder.DropTable(
                name: "UserFriend");

            migrationBuilder.RenameColumn(
                name: "ToId1",
                table: "Messages",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_ToId1",
                table: "Messages",
                newName: "IX_Messages_UserId");

            migrationBuilder.AlterColumn<string>(
                name: "Post",
                table: "NihilItems",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "MessageBody",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "ApplicationUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "ApplicationUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "ApplicationUsers",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUsers_UserId",
                table: "ApplicationUsers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUsers_ApplicationUsers_UserId",
                table: "ApplicationUsers",
                column: "UserId",
                principalTable: "ApplicationUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_ApplicationUsers_ToId",
                table: "Messages",
                column: "ToId",
                principalTable: "ApplicationUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_ApplicationUsers_UserId",
                table: "Messages",
                column: "UserId",
                principalTable: "ApplicationUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NihilItems_ApplicationUsers_UserId",
                table: "NihilItems",
                column: "UserId",
                principalTable: "ApplicationUsers",
                principalColumn: "Id");
        }
    }
}
