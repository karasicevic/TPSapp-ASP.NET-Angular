using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MasterTPSApp.Server.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Places",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    PostalCode = table.Column<int>(type: "int", nullable: false),
                    Population = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Places", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(33)", maxLength: 33, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(33)", maxLength: 33, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PersonalIdNumber = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    Height = table.Column<long>(type: "bigint", nullable: false),
                    PlaceOfBirthId = table.Column<long>(type: "bigint", nullable: false),
                    PlaceOfResidenceId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Persons_Places_PlaceOfBirthId",
                        column: x => x.PlaceOfBirthId,
                        principalTable: "Places",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Persons_Places_PlaceOfResidenceId",
                        column: x => x.PlaceOfResidenceId,
                        principalTable: "Places",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Persons_PlaceOfBirthId",
                table: "Persons",
                column: "PlaceOfBirthId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_PlaceOfResidenceId",
                table: "Persons",
                column: "PlaceOfResidenceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropTable(
                name: "Places");
        }
    }
}
