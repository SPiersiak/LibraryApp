using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryApp.API.Migrations
{
    public partial class init3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Borroweds_CopiesOfTheBooks_CopiesId",
                table: "Borroweds");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_CopiesOfTheBooks_CopiesId",
                table: "Reservations");

            migrationBuilder.DropTable(
                name: "CopiesOfTheBooks");

            migrationBuilder.RenameColumn(
                name: "CopiesId",
                table: "Reservations",
                newName: "BookId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_CopiesId",
                table: "Reservations",
                newName: "IX_Reservations_BookId");

            migrationBuilder.RenameColumn(
                name: "CopiesId",
                table: "Borroweds",
                newName: "BookId");

            migrationBuilder.RenameIndex(
                name: "IX_Borroweds_CopiesId",
                table: "Borroweds",
                newName: "IX_Borroweds_BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_Borroweds_Books_BookId",
                table: "Borroweds",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "BookId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Books_BookId",
                table: "Reservations",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "BookId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Borroweds_Books_BookId",
                table: "Borroweds");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Books_BookId",
                table: "Reservations");

            migrationBuilder.RenameColumn(
                name: "BookId",
                table: "Reservations",
                newName: "CopiesId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_BookId",
                table: "Reservations",
                newName: "IX_Reservations_CopiesId");

            migrationBuilder.RenameColumn(
                name: "BookId",
                table: "Borroweds",
                newName: "CopiesId");

            migrationBuilder.RenameIndex(
                name: "IX_Borroweds_BookId",
                table: "Borroweds",
                newName: "IX_Borroweds_CopiesId");

            migrationBuilder.CreateTable(
                name: "CopiesOfTheBooks",
                columns: table => new
                {
                    CopiesId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookId = table.Column<long>(type: "bigint", nullable: false),
                    Condition = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CopiesOfTheBooks", x => x.CopiesId);
                    table.ForeignKey(
                        name: "FK_CopiesOfTheBooks_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CopiesOfTheBooks_BookId",
                table: "CopiesOfTheBooks",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_Borroweds_CopiesOfTheBooks_CopiesId",
                table: "Borroweds",
                column: "CopiesId",
                principalTable: "CopiesOfTheBooks",
                principalColumn: "CopiesId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_CopiesOfTheBooks_CopiesId",
                table: "Reservations",
                column: "CopiesId",
                principalTable: "CopiesOfTheBooks",
                principalColumn: "CopiesId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
