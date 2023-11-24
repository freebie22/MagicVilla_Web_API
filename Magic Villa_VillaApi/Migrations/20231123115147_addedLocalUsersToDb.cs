using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Magic_Villa_VillaApi.Migrations
{
    /// <inheritdoc />
    public partial class addedLocalUsersToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LocalUsers",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalUsers", x => x.ID);
                });

            migrationBuilder.UpdateData(
                table: "VillaNumbers",
                keyColumn: "VillaNo",
                keyValue: 100,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 11, 23, 13, 51, 47, 687, DateTimeKind.Local).AddTicks(1047), new DateTime(2023, 11, 23, 13, 51, 47, 687, DateTimeKind.Local).AddTicks(1050) });

            migrationBuilder.UpdateData(
                table: "VillaNumbers",
                keyColumn: "VillaNo",
                keyValue: 101,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 11, 23, 13, 51, 47, 687, DateTimeKind.Local).AddTicks(1052), new DateTime(2023, 11, 23, 13, 51, 47, 687, DateTimeKind.Local).AddTicks(1054) });

            migrationBuilder.UpdateData(
                table: "VillaNumbers",
                keyColumn: "VillaNo",
                keyValue: 102,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 11, 23, 13, 51, 47, 687, DateTimeKind.Local).AddTicks(1056), new DateTime(2023, 11, 23, 13, 51, 47, 687, DateTimeKind.Local).AddTicks(1058) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 11, 23, 13, 51, 47, 687, DateTimeKind.Local).AddTicks(826));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2023, 11, 23, 13, 51, 47, 687, DateTimeKind.Local).AddTicks(878));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2023, 11, 23, 13, 51, 47, 687, DateTimeKind.Local).AddTicks(881));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LocalUsers");

            migrationBuilder.UpdateData(
                table: "VillaNumbers",
                keyColumn: "VillaNo",
                keyValue: 100,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 11, 3, 13, 46, 28, 533, DateTimeKind.Local).AddTicks(7922), new DateTime(2023, 11, 3, 13, 46, 28, 533, DateTimeKind.Local).AddTicks(7925) });

            migrationBuilder.UpdateData(
                table: "VillaNumbers",
                keyColumn: "VillaNo",
                keyValue: 101,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 11, 3, 13, 46, 28, 533, DateTimeKind.Local).AddTicks(7928), new DateTime(2023, 11, 3, 13, 46, 28, 533, DateTimeKind.Local).AddTicks(7930) });

            migrationBuilder.UpdateData(
                table: "VillaNumbers",
                keyColumn: "VillaNo",
                keyValue: 102,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 11, 3, 13, 46, 28, 533, DateTimeKind.Local).AddTicks(7933), new DateTime(2023, 11, 3, 13, 46, 28, 533, DateTimeKind.Local).AddTicks(7935) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 11, 3, 13, 46, 28, 533, DateTimeKind.Local).AddTicks(7682));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2023, 11, 3, 13, 46, 28, 533, DateTimeKind.Local).AddTicks(7738));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2023, 11, 3, 13, 46, 28, 533, DateTimeKind.Local).AddTicks(7742));
        }
    }
}
