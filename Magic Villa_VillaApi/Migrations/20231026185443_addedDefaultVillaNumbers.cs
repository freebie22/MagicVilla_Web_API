using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Magic_Villa_VillaApi.Migrations
{
    /// <inheritdoc />
    public partial class addedDefaultVillaNumbers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "VillaNumbers",
                columns: new[] { "VillaNo", "CreatedDate", "SpecialDetails", "UpdatedDate" },
                values: new object[,]
                {
                    { 100, new DateTime(2023, 10, 26, 21, 54, 42, 936, DateTimeKind.Local).AddTicks(1268), "Villa number is 100", new DateTime(2023, 10, 26, 21, 54, 42, 936, DateTimeKind.Local).AddTicks(1272) },
                    { 101, new DateTime(2023, 10, 26, 21, 54, 42, 936, DateTimeKind.Local).AddTicks(1274), "Villa number is 101", new DateTime(2023, 10, 26, 21, 54, 42, 936, DateTimeKind.Local).AddTicks(1276) },
                    { 102, new DateTime(2023, 10, 26, 21, 54, 42, 936, DateTimeKind.Local).AddTicks(1279), "Villa number is 102", new DateTime(2023, 10, 26, 21, 54, 42, 936, DateTimeKind.Local).AddTicks(1281) }
                });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 10, 26, 21, 54, 42, 936, DateTimeKind.Local).AddTicks(1048));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2023, 10, 26, 21, 54, 42, 936, DateTimeKind.Local).AddTicks(1101));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2023, 10, 26, 21, 54, 42, 936, DateTimeKind.Local).AddTicks(1105));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "VillaNumbers",
                keyColumn: "VillaNo",
                keyValue: 100);

            migrationBuilder.DeleteData(
                table: "VillaNumbers",
                keyColumn: "VillaNo",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "VillaNumbers",
                keyColumn: "VillaNo",
                keyValue: 102);

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 10, 26, 21, 33, 38, 852, DateTimeKind.Local).AddTicks(1470));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2023, 10, 26, 21, 33, 38, 852, DateTimeKind.Local).AddTicks(1519));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2023, 10, 26, 21, 33, 38, 852, DateTimeKind.Local).AddTicks(1523));
        }
    }
}
