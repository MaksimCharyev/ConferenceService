using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConferenceService.Migrations
{
    /// <inheritdoc />
    public partial class UpdateApplication : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_applications_activities_typeId",
                table: "applications");

            migrationBuilder.RenameColumn(
                name: "typeId",
                table: "applications",
                newName: "activityId");

            migrationBuilder.RenameIndex(
                name: "IX_applications_typeId",
                table: "applications",
                newName: "IX_applications_activityId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "sumbittedAt",
                table: "submittedApplications",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "createdAt",
                table: "applications",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "applications",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "applications",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_applications_activities_activityId",
                table: "applications",
                column: "activityId",
                principalTable: "activities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_applications_activities_activityId",
                table: "applications");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "applications");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "applications");

            migrationBuilder.RenameColumn(
                name: "activityId",
                table: "applications",
                newName: "typeId");

            migrationBuilder.RenameIndex(
                name: "IX_applications_activityId",
                table: "applications",
                newName: "IX_applications_typeId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "sumbittedAt",
                table: "submittedApplications",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "createdAt",
                table: "applications",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AddForeignKey(
                name: "FK_applications_activities_typeId",
                table: "applications",
                column: "typeId",
                principalTable: "activities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
