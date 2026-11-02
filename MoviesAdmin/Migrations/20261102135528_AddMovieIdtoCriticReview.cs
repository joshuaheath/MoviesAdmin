using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoviesAdmin.Migrations
{
    /// <inheritdoc />
    public partial class AddMovieIdtoCriticReview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "CriticReview",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AddColumn<int>(
                name: "movieId",
                table: "CriticReview",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CriticReview_movieId",
                table: "CriticReview",
                column: "movieId");

            migrationBuilder.AddForeignKey(
                name: "FK_CriticReview_Movie_movieId",
                table: "CriticReview",
                column: "movieId",
                principalTable: "Movie",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CriticReview_Movie_movieId",
                table: "CriticReview");

            migrationBuilder.DropIndex(
                name: "IX_CriticReview_movieId",
                table: "CriticReview");

            migrationBuilder.DropColumn(
                name: "movieId",
                table: "CriticReview");

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "CriticReview",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000);
        }
    }
}
