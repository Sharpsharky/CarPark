using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CarPark.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "parking_spaces",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Number = table.Column<int>(type: "integer", nullable: false),
                    IsOccupied = table.Column<bool>(type: "boolean", nullable: false),
                    VehicleReg = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    VehicleType = table.Column<int>(type: "integer", nullable: true),
                    TimeInUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_parking_spaces", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "parking_tickets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    VehicleReg = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    VehicleType = table.Column<int>(type: "integer", nullable: false),
                    SpaceNumber = table.Column<int>(type: "integer", nullable: false),
                    TimeInUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TimeOutUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Charge = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_parking_tickets", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "parking_spaces",
                columns: new[] { "Id", "IsOccupied", "Number", "TimeInUtc", "VehicleReg", "VehicleType" },
                values: new object[,]
                {
                    { new Guid("00e1be6f-f963-4ece-8efb-bc6edb12e62d"), false, 32, null, null, null },
                    { new Guid("01fb0b51-5393-40c2-a56e-244127f91c96"), false, 15, null, null, null },
                    { new Guid("02f8264d-b9b0-481e-882a-676f011ca8ef"), false, 64, null, null, null },
                    { new Guid("04bbf469-1969-4b09-91c3-befd99f2d297"), false, 49, null, null, null },
                    { new Guid("06e44e57-19df-4b29-b6f9-b4cec2d070c0"), false, 94, null, null, null },
                    { new Guid("079faa95-27dd-457e-ad5c-e312088cdcd8"), false, 60, null, null, null },
                    { new Guid("091e4237-677b-4a86-b059-3559469922b6"), false, 5, null, null, null },
                    { new Guid("131755c6-23de-4ee8-99c8-8e80c4cdc463"), false, 67, null, null, null },
                    { new Guid("18dd34e9-7486-4c7f-b190-83b543bdddfd"), false, 58, null, null, null },
                    { new Guid("1d060a53-7452-4988-8ea1-f59184c6d55b"), false, 53, null, null, null },
                    { new Guid("1d2cb018-06e3-4569-a44b-5d3177636a38"), false, 72, null, null, null },
                    { new Guid("1eb3288e-a225-4e4c-a6f1-ce0113959603"), false, 73, null, null, null },
                    { new Guid("20da0a11-722a-421a-8ac2-26c515e32b66"), false, 38, null, null, null },
                    { new Guid("21c406c7-07c0-4315-9d3e-9bfd0e5c1a17"), false, 35, null, null, null },
                    { new Guid("269f0011-45b2-4c95-8d95-134645be320b"), false, 69, null, null, null },
                    { new Guid("27202a00-8d0e-4674-a9d4-c1e6d6b21e4d"), false, 89, null, null, null },
                    { new Guid("27bfd5a6-1291-4361-bd54-fa0d54b4fcfe"), false, 27, null, null, null },
                    { new Guid("283dd80d-9ac2-4a94-857f-52c7d4ce0480"), false, 65, null, null, null },
                    { new Guid("29838746-266f-419a-a9b6-08a35939b280"), false, 59, null, null, null },
                    { new Guid("29c103b4-7333-4909-b79d-18b33421238c"), false, 41, null, null, null },
                    { new Guid("2cc93342-a25e-4787-862f-139e5a7a86cc"), false, 7, null, null, null },
                    { new Guid("2eda2f24-b572-4d8c-a932-a304e2d2c0e7"), false, 44, null, null, null },
                    { new Guid("2eef1941-2268-42d1-b681-4ba0196f6405"), false, 21, null, null, null },
                    { new Guid("31543c78-d436-4257-9961-d26319c561ee"), false, 28, null, null, null },
                    { new Guid("38607bc4-4587-4852-b3c6-9e9e1950d354"), false, 34, null, null, null },
                    { new Guid("38c7b302-5287-4aa3-9c42-edeb8487ef25"), false, 40, null, null, null },
                    { new Guid("395b96e5-6cf6-4255-a846-1b3604d2908d"), false, 13, null, null, null },
                    { new Guid("3bd5d04d-38e4-4de9-98ce-0762f21dc139"), false, 96, null, null, null },
                    { new Guid("41f68bf9-006b-4246-8b38-5d5ee6f69c5a"), false, 56, null, null, null },
                    { new Guid("446b767c-9921-45bb-9fb3-c519f0c731c2"), false, 81, null, null, null },
                    { new Guid("4be032dd-c172-466a-9ba6-031ac4a49665"), false, 37, null, null, null },
                    { new Guid("4c181e1f-be00-4a32-ae67-f3e100491116"), false, 47, null, null, null },
                    { new Guid("5273c4a7-2451-4f5e-8215-eae60caa4c3f"), false, 19, null, null, null },
                    { new Guid("539ac1ce-0ca5-4f27-9195-82f635f0f6a1"), false, 66, null, null, null },
                    { new Guid("549fae87-797b-46e9-b950-1b3b5b2f6dea"), false, 12, null, null, null },
                    { new Guid("5a564477-835b-4786-bd92-91ed7e1c16de"), false, 75, null, null, null },
                    { new Guid("5cbd6b9d-5710-432d-93b3-7f9c7456ed9c"), false, 17, null, null, null },
                    { new Guid("61d53d20-ff4c-405a-9b2f-b59e082a5a5f"), false, 16, null, null, null },
                    { new Guid("68c02101-3405-4ea2-8db7-026c00f575fe"), false, 55, null, null, null },
                    { new Guid("692b5ec0-62e4-4493-b9e1-9d8bcbdec94d"), false, 70, null, null, null },
                    { new Guid("7083d813-bf20-4df6-89d8-dd727531bcbd"), false, 71, null, null, null },
                    { new Guid("71fa4b28-c82f-4922-876a-4ec3e3aec41a"), false, 92, null, null, null },
                    { new Guid("72faf6af-b621-46bd-b96b-30ffb1196c1d"), false, 78, null, null, null },
                    { new Guid("738a0d7c-63e4-4401-8cca-928c6062e88a"), false, 46, null, null, null },
                    { new Guid("7772a2d1-52c0-4357-a081-ef2b7fbac321"), false, 31, null, null, null },
                    { new Guid("77f1f8aa-3988-49c8-bf93-3b75984bf54a"), false, 33, null, null, null },
                    { new Guid("794208bc-1aef-4681-ae6c-8442ae8afcea"), false, 10, null, null, null },
                    { new Guid("7947a1c3-83e8-4fb0-addc-89f77e3c62b4"), false, 74, null, null, null },
                    { new Guid("7a4c1a85-da70-49c3-ba5e-faab9bdc2c2e"), false, 83, null, null, null },
                    { new Guid("7b6e58cc-e915-48fa-a26c-8278ebee9964"), false, 42, null, null, null },
                    { new Guid("7c04051d-5afd-47c4-8b33-37ba784b0ef1"), false, 93, null, null, null },
                    { new Guid("7c6efa4f-84ff-454a-a77a-f9bf47e9dca2"), false, 54, null, null, null },
                    { new Guid("7f8fe01b-e443-45b6-9792-fe903411033a"), false, 50, null, null, null },
                    { new Guid("8270484c-991d-4d52-8976-bcc8fe803d2e"), false, 88, null, null, null },
                    { new Guid("82c51759-0a6b-480c-8195-77561bae24ac"), false, 2, null, null, null },
                    { new Guid("86ee41ae-6bf3-41d3-ae35-51115f92ead4"), false, 25, null, null, null },
                    { new Guid("8a034a28-268e-49ca-8148-f047ae7cecca"), false, 1, null, null, null },
                    { new Guid("8ce880ab-80a1-442f-a2bc-e98c465b562f"), false, 18, null, null, null },
                    { new Guid("8f25583b-ee04-42bb-b218-3d9dce372387"), false, 39, null, null, null },
                    { new Guid("94091e3a-2b8d-4572-98d5-e2fb8f55ce83"), false, 14, null, null, null },
                    { new Guid("96ea1cf3-a2d8-4491-8b32-195985965538"), false, 63, null, null, null },
                    { new Guid("9760c218-0df6-432d-aeb9-fd07412e67a0"), false, 62, null, null, null },
                    { new Guid("9789346f-f263-42f2-a859-fc99bca9bd4e"), false, 90, null, null, null },
                    { new Guid("9a5e3072-3de1-40f8-9578-b0cbbb13a1b2"), false, 29, null, null, null },
                    { new Guid("a17a644b-6879-4dad-b942-2a406e809ed0"), false, 4, null, null, null },
                    { new Guid("a25b34d7-ba63-45ca-8e5a-f3ace7ec62cc"), false, 6, null, null, null },
                    { new Guid("a5b4aa67-fa6c-452d-a61c-42b65f3c5a40"), false, 97, null, null, null },
                    { new Guid("aa08eea3-853b-4abc-9e94-507b2c25c858"), false, 95, null, null, null },
                    { new Guid("af87adce-b4d9-449e-9060-a8e7198d9b96"), false, 98, null, null, null },
                    { new Guid("b393a764-f1db-4214-9010-7df2935493f9"), false, 80, null, null, null },
                    { new Guid("b677216a-19f5-4cdc-9cff-f447a95cc5a0"), false, 3, null, null, null },
                    { new Guid("b7a2ceb9-6ca5-445e-955d-f45a820a4c45"), false, 43, null, null, null },
                    { new Guid("b8c4ee61-af76-4453-802f-67bf0101e35a"), false, 24, null, null, null },
                    { new Guid("b907d703-da11-4034-b4d6-1be54344e77e"), false, 26, null, null, null },
                    { new Guid("ba7a624f-317e-423f-b0c0-0ec41e7d46f1"), false, 22, null, null, null },
                    { new Guid("bc5990b4-076a-44c7-870b-0c7ca19abd5e"), false, 45, null, null, null },
                    { new Guid("bdff9b0a-eba7-47ad-adde-7b81cda16b50"), false, 76, null, null, null },
                    { new Guid("be88710a-a9c1-4b32-83a2-5ba7e2431831"), false, 79, null, null, null },
                    { new Guid("c6c5d568-a03d-451f-9c18-d7248b26808e"), false, 100, null, null, null },
                    { new Guid("c6cc8b6a-bccd-40f1-a71e-94d09a5d65d1"), false, 36, null, null, null },
                    { new Guid("c8da610a-cfdd-44fc-aaf0-8669e7a56f63"), false, 77, null, null, null },
                    { new Guid("c926fc9a-933e-4373-873e-1f639287e91e"), false, 20, null, null, null },
                    { new Guid("ceb5e0a1-b45d-4110-a726-62336d5ab573"), false, 84, null, null, null },
                    { new Guid("d50d9979-b107-4608-b8c8-e56f5140217c"), false, 52, null, null, null },
                    { new Guid("d7589829-5b8b-488d-99e1-8c41d53546a9"), false, 51, null, null, null },
                    { new Guid("dc8f0c58-660b-476c-b1cd-a0dbbd9c876f"), false, 85, null, null, null },
                    { new Guid("e0a9d5cd-cbaf-47d9-9671-e9a69363e4b4"), false, 99, null, null, null },
                    { new Guid("e137b613-6f97-4246-9ae6-713e0a975cc0"), false, 8, null, null, null },
                    { new Guid("e2170b3b-477f-4ce6-b92a-085a77dbdca9"), false, 9, null, null, null },
                    { new Guid("e7f8fa74-98a4-448b-9803-3818048a150b"), false, 82, null, null, null },
                    { new Guid("ea75fdf6-5591-4372-9778-8adf58a4e573"), false, 30, null, null, null },
                    { new Guid("eb8535cd-0e19-4e7d-ae0c-1923f81ec83e"), false, 61, null, null, null },
                    { new Guid("f15ef783-fce6-42c2-a03c-9c69e773616b"), false, 87, null, null, null },
                    { new Guid("f1a363b2-b5bb-43f2-8065-98045e82146c"), false, 91, null, null, null },
                    { new Guid("f32ab857-a63f-40ae-8f99-bdf00a9bfaac"), false, 86, null, null, null },
                    { new Guid("f3d6eb98-6573-4703-8597-45f96037aa1b"), false, 11, null, null, null },
                    { new Guid("f522f3f3-efe8-475c-8a24-921aaecf2a66"), false, 68, null, null, null },
                    { new Guid("f590f29c-f076-4aca-9116-cc24bfdf1665"), false, 57, null, null, null },
                    { new Guid("faae4734-1527-4fd8-ac94-692041264229"), false, 23, null, null, null },
                    { new Guid("fd554804-2f21-497c-9b24-996d39929bef"), false, 48, null, null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_parking_spaces_Number",
                table: "parking_spaces",
                column: "Number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_ticket_vehicle_active",
                table: "parking_tickets",
                columns: new[] { "VehicleReg", "TimeOutUtc" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "parking_spaces");

            migrationBuilder.DropTable(
                name: "parking_tickets");
        }
    }
}
