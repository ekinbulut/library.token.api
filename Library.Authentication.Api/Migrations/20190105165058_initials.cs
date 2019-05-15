using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Library.Authentication.Api.Migrations
{
#pragma warning disable IDE1006 // Naming Styles
    public partial class initials : Migration
#pragma warning restore IDE1006 // Naming Styles
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "ROLES",
                table => new
                         {
                             ID = table.Column<int>(nullable: false)
                                 .Annotation("SqlServer:ValueGenerationStrategy"
                                             , SqlServerValueGenerationStrategy.IdentityColumn)
                             , CREATED = table.Column<DateTime>("datetime2", nullable: false)
                             , UPDATED = table.Column<DateTime>("datetime2", nullable: false)
                             , CREATED_BY = table.Column<string>(nullable: true)
                             , UPDATED_BY = table.Column<string>(nullable: true)
                             , NAME = table.Column<string>(maxLength: 150, nullable: true)
                         },
                constraints: table => { table.PrimaryKey("PK_ROLES", x => x.ID); });

            migrationBuilder.CreateTable(
                "PERMISSIONS",
                table => new
                         {
                             ID = table.Column<int>(nullable: false)
                                 .Annotation("SqlServer:ValueGenerationStrategy"
                                             , SqlServerValueGenerationStrategy.IdentityColumn)
                             , CREATED = table.Column<DateTime>("datetime2", nullable: false)
                             , UPDATED = table.Column<DateTime>("datetime2", nullable: false)
                             , CREATED_BY = table.Column<string>(nullable: true)
                             , UPDATED_BY = table.Column<string>(nullable: true)
                             , TYPE = table.Column<string>(maxLength: 150, nullable: true)
                             , ERoleId = table.Column<int>(nullable: true)
                         },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PERMISSIONS", x => x.ID);
                    table.ForeignKey(
                        "FK_PERMISSIONS_ROLES_ERoleId",
                        x => x.ERoleId,
                        "ROLES",
                        "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                "USERS",
                table => new
                         {
                             ID = table.Column<int>(nullable: false)
                                 .Annotation("SqlServer:ValueGenerationStrategy"
                                             , SqlServerValueGenerationStrategy.IdentityColumn)
                             , CREATED = table.Column<DateTime>("datetime2", nullable: false)
                             , UPDATED = table.Column<DateTime>("datetime2", nullable: false)
                             , CREATED_BY = table.Column<string>(nullable: true)
                             , UPDATED_BY = table.Column<string>(nullable: true)
                             , USERNAME = table.Column<string>(maxLength: 150, nullable: true)
                             , PASSWORD = table.Column<string>(maxLength: 150, nullable: true)
                             , NAME = table.Column<string>(maxLength: 150, nullable: true)
                             , EMAIL = table.Column<string>(maxLength: 50, nullable: true)
                             , ROLE_ID = table.Column<int>(maxLength: 1, nullable: false)
                         },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USERS", x => x.ID);
                    table.ForeignKey(
                        "FK_USERS_ROLES_ROLE_ID",
                        x => x.ROLE_ID,
                        "ROLES",
                        "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                "ROLES",
                new[] {"ID", "CREATED", "CREATED_BY", "NAME", "UPDATED", "UPDATED_BY"},
                new object[]
                {
                    1, new DateTime(2019, 1, 5, 19, 50, 57, 527, DateTimeKind.Local).AddTicks(4957), "SYSTEM"
                    , "Administrator", new DateTime(2019, 1, 5, 19, 50, 57, 529, DateTimeKind.Local).AddTicks(7981)
                    , null
                });

            migrationBuilder.InsertData(
                "ROLES",
                new[] {"ID", "CREATED", "CREATED_BY", "NAME", "UPDATED", "UPDATED_BY"},
                new object[]
                {
                    2, new DateTime(2019, 1, 5, 19, 50, 57, 530, DateTimeKind.Local).AddTicks(9693), "SYSTEM", "Guest"
                    , new DateTime(2019, 1, 5, 19, 50, 57, 530, DateTimeKind.Local).AddTicks(9710), null
                });

            migrationBuilder.InsertData(
                "USERS",
                new[]
                {
                    "ID", "CREATED", "CREATED_BY", "EMAIL", "NAME", "PASSWORD", "ROLE_ID", "UPDATED", "UPDATED_BY"
                    , "USERNAME"
                },
                new object[]
                {
                    1, new DateTime(2019, 1, 5, 19, 50, 57, 531, DateTimeKind.Local).AddTicks(8257), "SYSTEM"
                    , "admin@admin.com", null, "181985", 1
                    , new DateTime(2019, 1, 5, 19, 50, 57, 531, DateTimeKind.Local).AddTicks(8274), null
                    , "admin@admin.com"
                });

            migrationBuilder.CreateIndex(
                "IX_PERMISSIONS_ERoleId",
                "PERMISSIONS",
                "ERoleId");

            migrationBuilder.CreateIndex(
                "IX_USERS_ROLE_ID",
                "USERS",
                "ROLE_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "PERMISSIONS");

            migrationBuilder.DropTable(
                "USERS");

            migrationBuilder.DropTable(
                "ROLES");
        }
    }
}