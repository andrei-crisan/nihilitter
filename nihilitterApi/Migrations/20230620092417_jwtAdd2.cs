using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace nihilitterApi.Migrations
{
    /// <inheritdoc />
    public partial class jwtAdd2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Message_ApplicationUsers_FromId",
                table: "Message");

            migrationBuilder.DropForeignKey(
                name: "FK_Message_ApplicationUsers_ToId",
                table: "Message");

            migrationBuilder.DropForeignKey(
                name: "FK_Message_ApplicationUsers_UserId",
                table: "Message");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Message",
                table: "Message");

            migrationBuilder.RenameTable(
                name: "Message",
                newName: "Messages");

            migrationBuilder.RenameIndex(
                name: "IX_Message_UserId",
                table: "Messages",
                newName: "IX_Messages_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Message_ToId",
                table: "Messages",
                newName: "IX_Messages_ToId");

            migrationBuilder.RenameIndex(
                name: "IX_Message_FromId",
                table: "Messages",
                newName: "IX_Messages_FromId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Messages",
                table: "Messages",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_ApplicationUsers_FromId",
                table: "Messages",
                column: "FromId",
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_ApplicationUsers_FromId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_ApplicationUsers_ToId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_ApplicationUsers_UserId",
                table: "Messages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Messages",
                table: "Messages");

            migrationBuilder.RenameTable(
                name: "Messages",
                newName: "Message");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_UserId",
                table: "Message",
                newName: "IX_Message_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_ToId",
                table: "Message",
                newName: "IX_Message_ToId");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_FromId",
                table: "Message",
                newName: "IX_Message_FromId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Message",
                table: "Message",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Message_ApplicationUsers_FromId",
                table: "Message",
                column: "FromId",
                principalTable: "ApplicationUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Message_ApplicationUsers_ToId",
                table: "Message",
                column: "ToId",
                principalTable: "ApplicationUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Message_ApplicationUsers_UserId",
                table: "Message",
                column: "UserId",
                principalTable: "ApplicationUsers",
                principalColumn: "Id");
        }
    }
}
