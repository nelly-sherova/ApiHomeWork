using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiHW.Migrations
{
    public partial class MyFirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Quotes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quotes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Quotes",
                columns: new[] { "Id", "Author", "InsertDate", "Text" },
                values: new object[,]
                {
                    { 1, "Наполеон Хилл", new DateTime(2020, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Что разум человека может постигнуть и во что он может поверить, того он способен достичь" },
                    { 2, "Альберт Эйнштейн", new DateTime(1890, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Стремитесь не к успеху, а к ценностям, которые он дает" },
                    { 3, "Достоевский", new DateTime(1890, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Надо любить жизнь больше, чем смысл жизни." },
                    { 4, "Амелия Эрхарт", new DateTime(1800, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Сложнее всего начать действовать, все остальное зависит только от упорства" },
                    { 5, "Борис Стругацкий", new DateTime(1100, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Начинать всегда стоит с того, что сеет сомнения." }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Quotes");
        }
    }
}
