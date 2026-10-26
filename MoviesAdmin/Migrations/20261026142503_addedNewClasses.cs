using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoviesAdmin.Migrations
{
    /// <inheritdoc />
    public partial class addedNewClasses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AudienceRatingKey",
                table: "Movie",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GenreKey",
                table: "Movie",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "audienceRatingid",
                table: "Movie",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "genreid",
                table: "Movie",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AudienceRating",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AudienceRating", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "CriticReview",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    rating = table.Column<int>(type: "int", nullable: false),
                    isPublished = table.Column<bool>(type: "bit", nullable: false),
                    createdBy = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    createdDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CriticReview", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Genre",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genre", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Movie_audienceRatingid",
                table: "Movie",
                column: "audienceRatingid");

            migrationBuilder.CreateIndex(
                name: "IX_Movie_genreid",
                table: "Movie",
                column: "genreid");

            migrationBuilder.AddForeignKey(
                name: "FK_Movie_AudienceRating_audienceRatingid",
                table: "Movie",
                column: "audienceRatingid",
                principalTable: "AudienceRating",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Movie_Genre_genreid",
                table: "Movie",
                column: "genreid",
                principalTable: "Genre",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movie_AudienceRating_audienceRatingid",
                table: "Movie");

            migrationBuilder.DropForeignKey(
                name: "FK_Movie_Genre_genreid",
                table: "Movie");

            migrationBuilder.DropTable(
                name: "AudienceRating");

            migrationBuilder.DropTable(
                name: "CriticReview");

            migrationBuilder.DropTable(
                name: "Genre");

            migrationBuilder.DropIndex(
                name: "IX_Movie_audienceRatingid",
                table: "Movie");

            migrationBuilder.DropIndex(
                name: "IX_Movie_genreid",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "AudienceRatingKey",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "GenreKey",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "audienceRatingid",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "genreid",
                table: "Movie");
        }
    }
}
