using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoList_AccessModel.Migrations
{
    public partial class seedingData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
            table: "Roles",
            columns: new[] { "Name" },
            values: new object[,]
            {
                {"Admin" },
                {"User" }
            });

            migrationBuilder.InsertData(
            table: "Levels",
            columns: new[] { "Name" },
            values: new object[,]
            {
                {"High" },
                {"Medium" },
                {"Low" }
            });

            migrationBuilder.InsertData(
            table: "Categories",
            columns: new[] { "Name" },
            values: new object[,]
            {
                {"Work"},
                {"Personal"},
                {"Home"},
                {"Study"},
                {"Shopping"},
                {"Travel"},
                {"Finance"},
                {"Social"},
                {"Project"}
            });
        }
    

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Levels");
            migrationBuilder.Sql("DELETE FROM Categories");
            migrationBuilder.Sql("DELETE FROM Roles");
        }
    }
}
