using Microsoft.EntityFrameworkCore.Migrations;

namespace MeetingDoc.Api.Migrations
{
    public partial class init_database : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MeetingAgendas_MeetingTimes_TimeId",
                table: "MeetingAgendas");

            migrationBuilder.DropForeignKey(
                name: "FK_MeetingContents_MeetingAgendas_AgendaId",
                table: "MeetingContents");

            migrationBuilder.DropForeignKey(
                name: "FK_MeetingNotes_MeetingContents_ContentId",
                table: "MeetingNotes");

            migrationBuilder.DropForeignKey(
                name: "FK_MeetingTimes_MeetingTopics_TopicId",
                table: "MeetingTimes");

            migrationBuilder.DropForeignKey(
                name: "FK_MeetingTopics_MeetingTypes_TypeId",
                table: "MeetingTopics");

            migrationBuilder.DropIndex(
                name: "IX_MeetingTopics_TypeId",
                table: "MeetingTopics");

            migrationBuilder.DropIndex(
                name: "IX_MeetingTimes_TopicId",
                table: "MeetingTimes");

            migrationBuilder.DropIndex(
                name: "IX_MeetingNotes_ContentId",
                table: "MeetingNotes");

            migrationBuilder.DropIndex(
                name: "IX_MeetingContents_AgendaId",
                table: "MeetingContents");

            migrationBuilder.DropIndex(
                name: "IX_MeetingAgendas_TimeId",
                table: "MeetingAgendas");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "MeetingTopics");

            migrationBuilder.DropColumn(
                name: "TopicId",
                table: "MeetingTimes");

            migrationBuilder.DropColumn(
                name: "ContentId",
                table: "MeetingNotes");

            migrationBuilder.DropColumn(
                name: "AgendaId",
                table: "MeetingContents");

            migrationBuilder.DropColumn(
                name: "TimeId",
                table: "MeetingAgendas");

            migrationBuilder.AddColumn<int>(
                name: "MeetingTypeId",
                table: "MeetingTopics",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MeetingTopicId",
                table: "MeetingTimes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MeetingContentId",
                table: "MeetingNotes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MeetingAgendaId",
                table: "MeetingContents",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MeetingTimeId",
                table: "MeetingAgendas",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MeetingTopics_MeetingTypeId",
                table: "MeetingTopics",
                column: "MeetingTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingTimes_MeetingTopicId",
                table: "MeetingTimes",
                column: "MeetingTopicId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingNotes_MeetingContentId",
                table: "MeetingNotes",
                column: "MeetingContentId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingContents_MeetingAgendaId",
                table: "MeetingContents",
                column: "MeetingAgendaId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingAgendas_MeetingTimeId",
                table: "MeetingAgendas",
                column: "MeetingTimeId");

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingAgendas_MeetingTimes_MeetingTimeId",
                table: "MeetingAgendas",
                column: "MeetingTimeId",
                principalTable: "MeetingTimes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingContents_MeetingAgendas_MeetingAgendaId",
                table: "MeetingContents",
                column: "MeetingAgendaId",
                principalTable: "MeetingAgendas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingNotes_MeetingContents_MeetingContentId",
                table: "MeetingNotes",
                column: "MeetingContentId",
                principalTable: "MeetingContents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingTimes_MeetingTopics_MeetingTopicId",
                table: "MeetingTimes",
                column: "MeetingTopicId",
                principalTable: "MeetingTopics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingTopics_MeetingTypes_MeetingTypeId",
                table: "MeetingTopics",
                column: "MeetingTypeId",
                principalTable: "MeetingTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MeetingAgendas_MeetingTimes_MeetingTimeId",
                table: "MeetingAgendas");

            migrationBuilder.DropForeignKey(
                name: "FK_MeetingContents_MeetingAgendas_MeetingAgendaId",
                table: "MeetingContents");

            migrationBuilder.DropForeignKey(
                name: "FK_MeetingNotes_MeetingContents_MeetingContentId",
                table: "MeetingNotes");

            migrationBuilder.DropForeignKey(
                name: "FK_MeetingTimes_MeetingTopics_MeetingTopicId",
                table: "MeetingTimes");

            migrationBuilder.DropForeignKey(
                name: "FK_MeetingTopics_MeetingTypes_MeetingTypeId",
                table: "MeetingTopics");

            migrationBuilder.DropIndex(
                name: "IX_MeetingTopics_MeetingTypeId",
                table: "MeetingTopics");

            migrationBuilder.DropIndex(
                name: "IX_MeetingTimes_MeetingTopicId",
                table: "MeetingTimes");

            migrationBuilder.DropIndex(
                name: "IX_MeetingNotes_MeetingContentId",
                table: "MeetingNotes");

            migrationBuilder.DropIndex(
                name: "IX_MeetingContents_MeetingAgendaId",
                table: "MeetingContents");

            migrationBuilder.DropIndex(
                name: "IX_MeetingAgendas_MeetingTimeId",
                table: "MeetingAgendas");

            migrationBuilder.DropColumn(
                name: "MeetingTypeId",
                table: "MeetingTopics");

            migrationBuilder.DropColumn(
                name: "MeetingTopicId",
                table: "MeetingTimes");

            migrationBuilder.DropColumn(
                name: "MeetingContentId",
                table: "MeetingNotes");

            migrationBuilder.DropColumn(
                name: "MeetingAgendaId",
                table: "MeetingContents");

            migrationBuilder.DropColumn(
                name: "MeetingTimeId",
                table: "MeetingAgendas");

            migrationBuilder.AddColumn<int>(
                name: "TypeId",
                table: "MeetingTopics",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TopicId",
                table: "MeetingTimes",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ContentId",
                table: "MeetingNotes",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AgendaId",
                table: "MeetingContents",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TimeId",
                table: "MeetingAgendas",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MeetingTopics_TypeId",
                table: "MeetingTopics",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingTimes_TopicId",
                table: "MeetingTimes",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingNotes_ContentId",
                table: "MeetingNotes",
                column: "ContentId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingContents_AgendaId",
                table: "MeetingContents",
                column: "AgendaId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingAgendas_TimeId",
                table: "MeetingAgendas",
                column: "TimeId");

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingAgendas_MeetingTimes_TimeId",
                table: "MeetingAgendas",
                column: "TimeId",
                principalTable: "MeetingTimes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingContents_MeetingAgendas_AgendaId",
                table: "MeetingContents",
                column: "AgendaId",
                principalTable: "MeetingAgendas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingNotes_MeetingContents_ContentId",
                table: "MeetingNotes",
                column: "ContentId",
                principalTable: "MeetingContents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingTimes_MeetingTopics_TopicId",
                table: "MeetingTimes",
                column: "TopicId",
                principalTable: "MeetingTopics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingTopics_MeetingTypes_TypeId",
                table: "MeetingTopics",
                column: "TypeId",
                principalTable: "MeetingTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
