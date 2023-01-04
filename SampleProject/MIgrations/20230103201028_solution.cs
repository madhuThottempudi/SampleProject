using Microsoft.EntityFrameworkCore.Migrations;

namespace SampleProject.Migrations
{
    public partial class solution : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    OptionsId = table.Column<int>(type: "int", nullable: false),
                    AnswerValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.QuestionId);
                });

            migrationBuilder.CreateTable(
                name: "Options",
                columns: table => new
                {
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    QuestionType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OptionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OptionValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuestionRefId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Options", x => x.QuestionId);
                    table.ForeignKey(
                        name: "FK_Options_Answers_QuestionRefId",
                        column: x => x.QuestionRefId,
                        principalTable: "Answers",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuestionType = table.Column<int>(type: "int", nullable: false),
                    IsPublished = table.Column<bool>(type: "bit", nullable: false),
                    QuestionRefId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questions_Options_QuestionRefId",
                        column: x => x.QuestionRefId,
                        principalTable: "Options",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Options_QuestionRefId",
                table: "Options",
                column: "QuestionRefId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_QuestionRefId",
                table: "Questions",
                column: "QuestionRefId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Options");

            migrationBuilder.DropTable(
                name: "Answers");
        }
    }
}
