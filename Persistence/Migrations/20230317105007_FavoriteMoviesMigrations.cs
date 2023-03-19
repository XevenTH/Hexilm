using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FavoriteMoviesMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRooms_AspNetUsers_UserAppId",
                table: "UserRooms");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRooms_Room_RoomId",
                table: "UserRooms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRooms",
                table: "UserRooms");

            migrationBuilder.RenameTable(
                name: "UserRooms",
                newName: "UserRooms_Join");

            migrationBuilder.RenameIndex(
                name: "IX_UserRooms_UserAppId",
                table: "UserRooms_Join",
                newName: "IX_UserRooms_Join_UserAppId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRooms_Join",
                table: "UserRooms_Join",
                columns: new[] { "RoomId", "UserAppId" });

            migrationBuilder.CreateTable(
                name: "FavoriteMovies_Join",
                columns: table => new
                {
                    UserAppId = table.Column<string>(type: "TEXT", nullable: false),
                    MovieId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteMovies_Join", x => new { x.UserAppId, x.MovieId });
                    table.ForeignKey(
                        name: "FK_FavoriteMovies_Join_AspNetUsers_UserAppId",
                        column: x => x.UserAppId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FavoriteMovies_Join_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteMovies_Join_MovieId",
                table: "FavoriteMovies_Join",
                column: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRooms_Join_AspNetUsers_UserAppId",
                table: "UserRooms_Join",
                column: "UserAppId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRooms_Join_Room_RoomId",
                table: "UserRooms_Join",
                column: "RoomId",
                principalTable: "Room",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRooms_Join_AspNetUsers_UserAppId",
                table: "UserRooms_Join");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRooms_Join_Room_RoomId",
                table: "UserRooms_Join");

            migrationBuilder.DropTable(
                name: "FavoriteMovies_Join");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRooms_Join",
                table: "UserRooms_Join");

            migrationBuilder.RenameTable(
                name: "UserRooms_Join",
                newName: "UserRooms");

            migrationBuilder.RenameIndex(
                name: "IX_UserRooms_Join_UserAppId",
                table: "UserRooms",
                newName: "IX_UserRooms_UserAppId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRooms",
                table: "UserRooms",
                columns: new[] { "RoomId", "UserAppId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserRooms_AspNetUsers_UserAppId",
                table: "UserRooms",
                column: "UserAppId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRooms_Room_RoomId",
                table: "UserRooms",
                column: "RoomId",
                principalTable: "Room",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
