using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorDemo.Api.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "BlazorDemo");

            migrationBuilder.CreateTable(
                name: "Book",
                schema: "BlazorDemo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BookTitle = table.Column<string>(nullable: false),
                    ISBN = table.Column<string>(nullable: true),
                    PublisherName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.Id);
                });

            migrationBuilder.InsertData(
                schema: "BlazorDemo",
                table: "Book",
                columns: new[] { "Id", "BookTitle", "ISBN", "PublisherName" },
                values: new object[] { 1, "C#", "12312", "Oreally" });

            migrationBuilder.InsertData(
                schema: "BlazorDemo",
                table: "Book",
                columns: new[] { "Id", "BookTitle", "ISBN", "PublisherName" },
                values: new object[] { 2, "dot net", "781273", "Wrox" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Book",
                schema: "BlazorDemo");
        }
    }
}
