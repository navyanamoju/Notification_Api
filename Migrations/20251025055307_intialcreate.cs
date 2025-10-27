using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace NotificationsApi.Migrations
{
    /// <inheritdoc />
    public partial class intialcreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Department = table.Column<string>(type: "text", nullable: false),
                    Salary = table.Column<decimal>(type: "numeric", nullable: false),
                    Created_At = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "notificationmessage",
                columns: table => new
                {
                    notificationmessageid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    notificationchannel = table.Column<string>(type: "text", nullable: false),
                    notificationheading = table.Column<string>(type: "text", nullable: false),
                    notificationbody = table.Column<string>(type: "text", nullable: false),
                    notificationfooter = table.Column<string>(type: "text", nullable: false),
                    notificationsubject = table.Column<string>(type: "text", nullable: false),
                    repeatevery = table.Column<int>(type: "integer", nullable: true),
                    nooftimestorepeat = table.Column<int>(type: "integer", nullable: true),
                    createdby = table.Column<string>(type: "text", nullable: true),
                    createddate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    updatedby = table.Column<string>(type: "text", nullable: true),
                    updateddate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    repeatnotification = table.Column<string>(type: "text", nullable: false),
                    usedocumenttemplate = table.Column<string>(type: "text", nullable: true),
                    documenttemplateid = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notificationmessage", x => x.notificationmessageid);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "notificationmessage");
        }
    }
}
