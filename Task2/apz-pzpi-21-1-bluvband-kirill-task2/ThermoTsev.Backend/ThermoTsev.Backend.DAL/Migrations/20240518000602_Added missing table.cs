using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThermoTsev.Backend.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Addedmissingtable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shipments_Location_EndLocationId",
                table: "Shipments");

            migrationBuilder.DropForeignKey(
                name: "FK_Shipments_Location_StartLocationId",
                table: "Shipments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Location",
                table: "Location");

            migrationBuilder.RenameTable(
                name: "Location",
                newName: "Locations");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Locations",
                table: "Locations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Shipments_Locations_EndLocationId",
                table: "Shipments",
                column: "EndLocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Shipments_Locations_StartLocationId",
                table: "Shipments",
                column: "StartLocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shipments_Locations_EndLocationId",
                table: "Shipments");

            migrationBuilder.DropForeignKey(
                name: "FK_Shipments_Locations_StartLocationId",
                table: "Shipments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Locations",
                table: "Locations");

            migrationBuilder.RenameTable(
                name: "Locations",
                newName: "Location");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Location",
                table: "Location",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Shipments_Location_EndLocationId",
                table: "Shipments",
                column: "EndLocationId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Shipments_Location_StartLocationId",
                table: "Shipments",
                column: "StartLocationId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
