using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventSourcingDemo.Migrations
{
    /// <inheritdoc />
    public partial class AddedNameToTypeAndAttack : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "PokemonTypes",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "PokemonAttacks",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "PokemonTypes");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "PokemonAttacks");
        }
    }
}
