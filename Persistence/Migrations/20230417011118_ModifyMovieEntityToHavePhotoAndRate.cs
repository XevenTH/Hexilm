using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ModifyMovieEntityToHavePhotoAndRate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Picture",
                table: "Movies");

            migrationBuilder.AddColumn<Guid>(
                name: "MovieId",
                table: "Photos",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Rate",
                table: "Movies",
                type: "REAL",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "UserRateCount",
                table: "Movies",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Photos_MovieId",
                table: "Photos",
                column: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Movies_MovieId",
                table: "Photos",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Movies_MovieId",
                table: "Photos");

            migrationBuilder.DropIndex(
                name: "IX_Photos_MovieId",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "MovieId",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "Rate",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "UserRateCount",
                table: "Movies");

            migrationBuilder.AddColumn<string>(
                name: "Picture",
                table: "Movies",
                type: "TEXT",
                nullable: true);
        }
    }
}
