using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MeetingDoc.Api.Migrations
{
    public partial class initdb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MeetingTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IsRemoved = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<int>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    IsDraft = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeetingTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IsRemoved = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<int>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Position = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<byte[]>(nullable: true),
                    PasswordSalt = table.Column<byte[]>(nullable: true),
                    PhoneNo = table.Column<string>(nullable: true),
                    Level = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MeetingTopics",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IsRemoved = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<int>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    MeetingTypeId = table.Column<int>(nullable: false),
                    IsDraft = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeetingTopics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MeetingTopics_MeetingTypes_MeetingTypeId",
                        column: x => x.MeetingTypeId,
                        principalTable: "MeetingTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MeetingTimes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IsRemoved = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<int>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    MeetingTopicId = table.Column<int>(nullable: false),
                    Count = table.Column<int>(nullable: false),
                    FiscalYear = table.Column<string>(nullable: true),
                    MeetingDate = table.Column<DateTime>(nullable: false),
                    Location = table.Column<string>(nullable: true),
                    IsDraft = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeetingTimes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MeetingTimes_MeetingTopics_MeetingTopicId",
                        column: x => x.MeetingTopicId,
                        principalTable: "MeetingTopics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MeetingAgendas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IsRemoved = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<int>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    MeetingTimeId = table.Column<int>(nullable: false),
                    Number = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    IsDraft = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeetingAgendas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MeetingAgendas_MeetingTimes_MeetingTimeId",
                        column: x => x.MeetingTimeId,
                        principalTable: "MeetingTimes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MeetingAgendaUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IsRemoved = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<int>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    MeetingAgendaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeetingAgendaUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MeetingAgendaUsers_MeetingAgendas_MeetingAgendaId",
                        column: x => x.MeetingAgendaId,
                        principalTable: "MeetingAgendas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MeetingAgendaUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MeetingContents",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IsRemoved = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<int>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    FileName = table.Column<string>(nullable: true),
                    FileBase64 = table.Column<byte[]>(type: "MediumBlob", nullable: true),
                    Ordinal = table.Column<int>(nullable: false),
                    MeetingAgendaId = table.Column<int>(nullable: false),
                    Ratio = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeetingContents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MeetingContents_MeetingAgendas_MeetingAgendaId",
                        column: x => x.MeetingAgendaId,
                        principalTable: "MeetingAgendas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MeetingNotes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IsRemoved = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<int>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    MeetingContentId = table.Column<int>(nullable: false),
                    NoteHeader = table.Column<string>(nullable: true),
                    Note = table.Column<byte[]>(type: "MediumBlob", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeetingNotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MeetingNotes_MeetingContents_MeetingContentId",
                        column: x => x.MeetingContentId,
                        principalTable: "MeetingContents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MeetingNotes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MeetingAgendas_MeetingTimeId",
                table: "MeetingAgendas",
                column: "MeetingTimeId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingAgendaUsers_MeetingAgendaId",
                table: "MeetingAgendaUsers",
                column: "MeetingAgendaId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingAgendaUsers_UserId",
                table: "MeetingAgendaUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingContents_MeetingAgendaId",
                table: "MeetingContents",
                column: "MeetingAgendaId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingNotes_MeetingContentId",
                table: "MeetingNotes",
                column: "MeetingContentId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingNotes_UserId",
                table: "MeetingNotes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingTimes_MeetingTopicId",
                table: "MeetingTimes",
                column: "MeetingTopicId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingTopics_MeetingTypeId",
                table: "MeetingTopics",
                column: "MeetingTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MeetingAgendaUsers");

            migrationBuilder.DropTable(
                name: "MeetingNotes");

            migrationBuilder.DropTable(
                name: "MeetingContents");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "MeetingAgendas");

            migrationBuilder.DropTable(
                name: "MeetingTimes");

            migrationBuilder.DropTable(
                name: "MeetingTopics");

            migrationBuilder.DropTable(
                name: "MeetingTypes");
        }
    }
}
