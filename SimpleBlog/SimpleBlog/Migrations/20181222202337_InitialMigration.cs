using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SimpleBlog.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Login = table.Column<string>(nullable: true),
                    Salt = table.Column<byte[]>(nullable: true),
                    PasswordHash = table.Column<byte[]>(nullable: true),
                    IsAdmin = table.Column<bool>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Text = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    Tags = table.Column<string>(nullable: true),
                    AdminId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_Accounts_AdminId",
                        column: x => x.AdminId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Content = table.Column<string>(nullable: true),
                    PostId = table.Column<int>(nullable: true),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comments_Accounts_UserId",
                        column: x => x.UserId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "CreatedAt", "Discriminator", "IsAdmin", "Login", "PasswordHash", "Salt", "UpdatedAt" },
                values: new object[] { 1, new DateTime(2018, 12, 22, 20, 23, 36, 582, DateTimeKind.Utc), "Admin", true, "SuperAdmin666", new byte[] { 127, 146, 53, 149, 64, 71, 39, 171, 139, 201, 219, 196, 19, 71, 194, 111, 170, 172, 186, 110, 148, 199, 28, 107, 246, 37, 79, 26, 202, 107, 25, 125, 11, 29, 33, 64, 147, 139, 151, 198, 16, 107, 7, 49, 150, 190, 37, 233, 99, 52, 71, 54, 210, 89, 140, 162, 31, 7, 109, 147, 150, 27, 167, 99 }, new byte[] { 126, 75, 58, 102, 157, 49, 112, 239, 23, 45, 183, 190, 129, 167, 160, 60, 0, 113, 149, 38, 151, 142, 25, 75, 74, 218, 189, 135, 234, 194, 165, 237, 7, 203, 216, 131, 224, 183, 106, 92, 102, 210, 167, 173, 237, 69, 210, 247, 45, 222, 1, 28, 210, 125, 136, 90, 7, 214, 107, 50, 161, 14, 203, 99, 135, 142, 231, 69, 88, 73, 142, 225, 141, 120, 56, 3, 252, 194, 237, 11, 30, 18, 31, 98, 189, 68, 206, 226, 33, 22, 182, 246, 86, 254, 114, 191, 200, 66, 160, 143, 13, 114, 118, 206, 208, 66, 69, 58, 159, 216, 249, 94, 120, 127, 228, 25, 141, 24, 102, 195, 220, 246, 212, 221, 28, 124, 47, 253 }, new DateTime(2018, 12, 22, 20, 23, 36, 582, DateTimeKind.Utc) });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PostId",
                table: "Comments",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_AdminId",
                table: "Posts",
                column: "AdminId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}
