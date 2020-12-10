using Microsoft.EntityFrameworkCore.Migrations;

namespace CoWorker.API.Migrations
{
    public partial class Settings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SettingsRecords_ApplicationUserId",
                table: "SettingsRecords");

            migrationBuilder.CreateIndex(
                name: "IX_SettingsRecords_ApplicationUserId",
                table: "SettingsRecords",
                column: "ApplicationUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SettingsRecords_ApplicationUserId",
                table: "SettingsRecords");

            migrationBuilder.CreateIndex(
                name: "IX_SettingsRecords_ApplicationUserId",
                table: "SettingsRecords",
                column: "ApplicationUserId",
                unique: true,
                filter: "[ApplicationUserId] IS NOT NULL");
        }
    }
}
