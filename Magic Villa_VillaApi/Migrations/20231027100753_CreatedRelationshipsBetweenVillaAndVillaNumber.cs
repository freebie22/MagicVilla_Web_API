using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Magic_Villa_VillaApi.Migrations
{
    /// <inheritdoc />
    public partial class CreatedRelationshipsBetweenVillaAndVillaNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VillaId",
                table: "VillaNumbers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            //migrationBuilder.UpdateData(
            //    table: "VillaNumbers",
            //    keyColumn: "VillaNo",
            //    keyValue: 100,
            //    columns: new[] { "CreatedDate", "UpdatedDate", "VillaId" },
            //    values: new object[] { new DateTime(2023, 10, 27, 13, 7, 53, 295, DateTimeKind.Local).AddTicks(4857), new DateTime(2023, 10, 27, 13, 7, 53, 295, DateTimeKind.Local).AddTicks(4861), 0 });

            //migrationBuilder.UpdateData(
            //    table: "VillaNumbers",
            //    keyColumn: "VillaNo",
            //    keyValue: 101,
            //    columns: new[] { "CreatedDate", "UpdatedDate", "VillaId" },
            //    values: new object[] { new DateTime(2023, 10, 27, 13, 7, 53, 295, DateTimeKind.Local).AddTicks(4864), new DateTime(2023, 10, 27, 13, 7, 53, 295, DateTimeKind.Local).AddTicks(4865), 0 });

            //migrationBuilder.UpdateData(
            //    table: "VillaNumbers",
            //    keyColumn: "VillaNo",
            //    keyValue: 102,
            //    columns: new[] { "CreatedDate", "UpdatedDate", "VillaId" },
            //    values: new object[] { new DateTime(2023, 10, 27, 13, 7, 53, 295, DateTimeKind.Local).AddTicks(4868), new DateTime(2023, 10, 27, 13, 7, 53, 295, DateTimeKind.Local).AddTicks(4870), 0 });

            //migrationBuilder.UpdateData(
            //    table: "Villas",
            //    keyColumn: "Id",
            //    keyValue: 1,
            //    column: "CreatedDate",
            //    value: new DateTime(2023, 10, 27, 13, 7, 53, 295, DateTimeKind.Local).AddTicks(4646));

            //migrationBuilder.UpdateData(
            //    table: "Villas",
            //    keyColumn: "Id",
            //    keyValue: 2,
            //    column: "CreatedDate",
            //    value: new DateTime(2023, 10, 27, 13, 7, 53, 295, DateTimeKind.Local).AddTicks(4702));

            //migrationBuilder.UpdateData(
            //    table: "Villas",
            //    keyColumn: "Id",
            //    keyValue: 3,
            //    column: "CreatedDate",
            //    value: new DateTime(2023, 10, 27, 13, 7, 53, 295, DateTimeKind.Local).AddTicks(4705));

            migrationBuilder.CreateIndex(
                name: "IX_VillaNumbers_VillaId",
                table: "VillaNumbers",
                column: "VillaId");

            migrationBuilder.AddForeignKey(
                name: "FK_VillaNumbers_Villas_VillaId",
                table: "VillaNumbers",
                column: "VillaId",
                principalTable: "Villas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VillaNumbers_Villas_VillaId",
                table: "VillaNumbers");

            migrationBuilder.DropIndex(
                name: "IX_VillaNumbers_VillaId",
                table: "VillaNumbers");

            migrationBuilder.DropColumn(
                name: "VillaId",
                table: "VillaNumbers");

            migrationBuilder.UpdateData(
                table: "VillaNumbers",
                keyColumn: "VillaNo",
                keyValue: 100,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 26, 21, 54, 42, 936, DateTimeKind.Local).AddTicks(1268), new DateTime(2023, 10, 26, 21, 54, 42, 936, DateTimeKind.Local).AddTicks(1272) });

            migrationBuilder.UpdateData(
                table: "VillaNumbers",
                keyColumn: "VillaNo",
                keyValue: 101,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 26, 21, 54, 42, 936, DateTimeKind.Local).AddTicks(1274), new DateTime(2023, 10, 26, 21, 54, 42, 936, DateTimeKind.Local).AddTicks(1276) });

            migrationBuilder.UpdateData(
                table: "VillaNumbers",
                keyColumn: "VillaNo",
                keyValue: 102,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 26, 21, 54, 42, 936, DateTimeKind.Local).AddTicks(1279), new DateTime(2023, 10, 26, 21, 54, 42, 936, DateTimeKind.Local).AddTicks(1281) });

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
    }
}
