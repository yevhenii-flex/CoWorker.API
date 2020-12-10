using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoWorker.API.Data.Migrations
{
    public partial class NullCheck : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_SettingsRecords_CurrentSettingsId",
                table: "Rooms");

            migrationBuilder.AlterColumn<int>(
                name: "CurrentSettingsId",
                table: "Rooms",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartTime",
                table: "BookingRecords",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndTime",
                table: "BookingRecords",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_SettingsRecords_CurrentSettingsId",
                table: "Rooms",
                column: "CurrentSettingsId",
                principalTable: "SettingsRecords",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_SettingsRecords_CurrentSettingsId",
                table: "Rooms");

            migrationBuilder.AlterColumn<int>(
                name: "CurrentSettingsId",
                table: "Rooms",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartTime",
                table: "BookingRecords",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndTime",
                table: "BookingRecords",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_SettingsRecords_CurrentSettingsId",
                table: "Rooms",
                column: "CurrentSettingsId",
                principalTable: "SettingsRecords",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
