using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MonkeyShelter.Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Shelters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shelters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Species",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Species", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Monkeys",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Weight = table.Column<double>(type: "float", nullable: false),
                    ArrivalDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DepartureDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SpeciesId = table.Column<int>(type: "int", nullable: false),
                    ShelterId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Monkeys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Monkeys_Shelters_ShelterId",
                        column: x => x.ShelterId,
                        principalTable: "Shelters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Monkeys_Species_SpeciesId",
                        column: x => x.SpeciesId,
                        principalTable: "Species",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VetChecks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CheckDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MonkeyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VetChecks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VetChecks_Monkeys_MonkeyId",
                        column: x => x.MonkeyId,
                        principalTable: "Monkeys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Shelters",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Krnjaca" },
                    { 2, "Batajnica" }
                });

            migrationBuilder.InsertData(
                table: "Species",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Kapucin" },
                    { 2, "Zlatni lavlji tamari" },
                    { 3, "Mandril" },
                    { 4, "Babun" },
                    { 5, "Langur" },
                    { 6, "Pauk majmun" },
                    { 7, "Veveričasti majmun" },
                    { 8, "Rezus makaki" },
                    { 9, "Gibon" },
                    { 10, "Uakari" },
                    { 11, "Nosati majmun" },
                    { 12, "Džepni marmoset" },
                    { 13, "Howler majmun" },
                    { 14, "Gvenon" },
                    { 15, "Dril" }
                });

            migrationBuilder.InsertData(
                table: "Monkeys",
                columns: new[] { "Id", "ArrivalDate", "DepartureDate", "Name", "ShelterId", "SpeciesId", "Weight" },
                values: new object[,]
                {
                    { new Guid("0111cc28-1ead-4973-9a2f-b8e89869489c"), new DateTime(2025, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Laza1", 1, 1, 44.0 },
                    { new Guid("03d71cb3-a68b-448e-9f00-6d2db66bcc1c"), new DateTime(2025, 5, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Jova3", 1, 3, 55.0 },
                    { new Guid("04999673-5408-43ac-9eab-8e0af9798190"), new DateTime(2025, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Mika1", 1, 1, 33.0 },
                    { new Guid("06ff6efb-45ef-4747-94d5-21eb5cfdb7a3"), new DateTime(2025, 5, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Laza7", 1, 7, 44.0 },
                    { new Guid("0cd3df03-ab4a-427e-9751-9a38b5902a3f"), new DateTime(2025, 5, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Jova7", 1, 7, 55.0 },
                    { new Guid("0dcbbdbd-e74b-46e8-ab32-0ccbfae47465"), new DateTime(2025, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Sima5", 1, 5, 66.0 },
                    { new Guid("0f153dc2-a163-4233-a845-dfa3a84487ab"), new DateTime(2025, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Jova8", 1, 8, 55.0 },
                    { new Guid("13165eaf-8d2d-4ab6-a8ee-551a18023cd0"), new DateTime(2025, 5, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Sima4", 1, 4, 66.0 },
                    { new Guid("17d279cc-689f-4874-90ae-e2644b2c59ba"), new DateTime(2025, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Sima12", 1, 12, 66.0 },
                    { new Guid("2040ad2c-69b3-4129-8aae-628a81c34015"), new DateTime(2025, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Jova14", 1, 14, 55.0 },
                    { new Guid("20a8648e-dc0c-4815-bd14-b89ce4e04c74"), new DateTime(2025, 5, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Cica6", 1, 6, 77.0 },
                    { new Guid("21a42287-dcdb-4838-ae3e-d379a22fc004"), new DateTime(2025, 5, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Laza6", 1, 6, 44.0 },
                    { new Guid("24450d97-b650-47a9-b65c-41c69f7bc8bb"), new DateTime(2025, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Pera8", 1, 8, 22.0 },
                    { new Guid("25c282b4-1fd3-496c-8915-f9e434e4041e"), new DateTime(2025, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Pera12", 1, 12, 22.0 },
                    { new Guid("2a9833eb-cff7-4580-9ec2-2eea1caabd6f"), new DateTime(2025, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Jova12", 1, 12, 55.0 },
                    { new Guid("301b697d-480d-47f4-a5d9-56cf64db9e2b"), new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Sima10", 1, 10, 66.0 },
                    { new Guid("3fab5e6f-df96-483f-9824-e8ccfea70f41"), new DateTime(2025, 5, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Cica7", 1, 7, 77.0 },
                    { new Guid("3fb8b5b6-8d22-4a3c-99bf-e9923bb81036"), new DateTime(2025, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Mika14", 1, 14, 33.0 },
                    { new Guid("40c773a7-1d82-4dc5-bb4f-0b705a188c70"), new DateTime(2025, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Sima2", 1, 2, 66.0 },
                    { new Guid("42029059-8fb7-49c2-8355-6da8f1b928d8"), new DateTime(2025, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Cica8", 1, 8, 77.0 },
                    { new Guid("438cc2ee-91d1-4f72-b4cf-b353c467e0d9"), new DateTime(2025, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Cica2", 1, 2, 77.0 },
                    { new Guid("43c6dcd8-b0fe-49e4-87d0-4d0eec6fb84b"), new DateTime(2025, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Mika2", 1, 2, 33.0 },
                    { new Guid("466d7f08-b362-4698-b818-16269200dd74"), new DateTime(2025, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Pera2", 1, 2, 22.0 },
                    { new Guid("4754f9a1-b49a-4b59-b8ed-c7ae893debaf"), new DateTime(2025, 5, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Jova9", 1, 9, 55.0 },
                    { new Guid("48ddc4f3-1120-44e4-a090-61fb2ce96c14"), new DateTime(2025, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Pera5", 1, 5, 22.0 },
                    { new Guid("49fab9d1-5f65-4717-a97f-977ed0d46cb9"), new DateTime(2025, 5, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Jova11", 1, 11, 55.0 },
                    { new Guid("4b8a14c1-fd79-43ee-bb54-420344931cef"), new DateTime(2025, 5, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Sima7", 1, 7, 66.0 },
                    { new Guid("4c8be266-ece0-4c03-a94b-822e4cd4b67c"), new DateTime(2025, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Cica12", 1, 12, 77.0 },
                    { new Guid("4f301620-8e76-4d95-b92e-3e1addc8d706"), new DateTime(2025, 5, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Mika7", 1, 7, 33.0 },
                    { new Guid("5235fd66-7468-4f8b-98e4-08b54f3e868a"), new DateTime(2025, 5, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Laza11", 1, 11, 44.0 },
                    { new Guid("56067503-20ec-4a6b-9cac-f4c466a645c4"), new DateTime(2025, 5, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Jova6", 1, 6, 55.0 },
                    { new Guid("58867445-a5dd-4f67-94b9-f1fadd9ed279"), new DateTime(2025, 5, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Mika3", 1, 3, 33.0 },
                    { new Guid("627de6f7-604e-457b-b1d0-b57fa1d11083"), new DateTime(2025, 5, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Pera3", 1, 3, 22.0 },
                    { new Guid("6977c50f-63ca-4c8b-a9e4-c20edce86caa"), new DateTime(2025, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Sima14", 1, 14, 66.0 },
                    { new Guid("6a2fe30f-d0bf-4973-8cd3-de390e243b75"), new DateTime(2025, 5, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Pera4", 1, 4, 22.0 },
                    { new Guid("6c3fe9fe-6ac5-4133-a02f-43506ef86240"), new DateTime(2025, 5, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Cica4", 1, 4, 77.0 },
                    { new Guid("6c861032-ef62-4ef9-84f8-9a11aeedc484"), new DateTime(2025, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Laza8", 1, 8, 44.0 },
                    { new Guid("6e267371-7cb3-4f63-afa1-c876e44691f5"), new DateTime(2025, 5, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Sima6", 1, 6, 66.0 },
                    { new Guid("70120d00-c1ff-4187-b549-eadaa924aa17"), new DateTime(2025, 5, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Cica9", 1, 9, 77.0 },
                    { new Guid("702a8e87-0da2-4964-a57d-1d97986fdd97"), new DateTime(2025, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Mika12", 1, 12, 33.0 },
                    { new Guid("7151343a-29ad-45d3-a4ab-9e09bc42973b"), new DateTime(2025, 5, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Sima3", 1, 3, 66.0 },
                    { new Guid("71c98498-776c-4562-b00d-cc57aff50095"), new DateTime(2025, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Cica1", 1, 1, 77.0 }
                });

            migrationBuilder.InsertData(
                table: "Monkeys",
                columns: new[] { "Id", "ArrivalDate", "DepartureDate", "Name", "ShelterId", "SpeciesId", "Weight" },
                values: new object[,]
                {
                    { new Guid("725cc9a4-1fdb-4064-a72b-cd7e8e95eb7c"), new DateTime(2025, 5, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Pera6", 1, 6, 22.0 },
                    { new Guid("78dcfb89-4eda-4949-9b87-93bcac428348"), new DateTime(2025, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Mika15", 1, 15, 33.0 },
                    { new Guid("7ae7641d-050c-4d18-bd4b-754e5e29ff88"), new DateTime(2025, 5, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Laza9", 1, 9, 44.0 },
                    { new Guid("81c9eadc-97a4-455f-a38e-276792e27d17"), new DateTime(2025, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Sima15", 1, 15, 66.0 },
                    { new Guid("846ef5ab-c3a7-4080-97bf-99f6d576388e"), new DateTime(2025, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Jova1", 1, 1, 55.0 },
                    { new Guid("8b3b82ec-e195-4c72-9e2a-d586c93b2e78"), new DateTime(2025, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Mika13", 1, 13, 33.0 },
                    { new Guid("8be3d780-41e1-4d78-a2d4-411908d8a396"), new DateTime(2025, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Laza14", 1, 14, 44.0 },
                    { new Guid("8deb0c93-57d8-4150-9b03-dd0aba3a7001"), new DateTime(2025, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Cica13", 1, 13, 77.0 },
                    { new Guid("9187e29f-934a-422f-bbc9-55685dcfb1a6"), new DateTime(2025, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Cica14", 1, 14, 77.0 },
                    { new Guid("92a656c0-4f51-463c-b8bf-e4d1f522a5f1"), new DateTime(2025, 5, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Sima9", 1, 9, 66.0 },
                    { new Guid("99cceea0-1f8f-4c1d-a23f-3376007d44fc"), new DateTime(2025, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Jova5", 1, 5, 55.0 },
                    { new Guid("9e512851-3e4d-4608-afcc-aab57cd28942"), new DateTime(2025, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Cica5", 1, 5, 77.0 },
                    { new Guid("a291a9b7-5312-4236-9ae1-9dfba00f7562"), new DateTime(2025, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Mika8", 1, 8, 33.0 },
                    { new Guid("a3a068f9-bc9f-49bf-a41d-2ad631557890"), new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Mika10", 1, 10, 33.0 },
                    { new Guid("a5b976ad-6c12-494c-8b98-aa91d378c62b"), new DateTime(2025, 5, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Pera7", 1, 7, 22.0 },
                    { new Guid("a8394419-f65a-467d-ae69-04df0f0e964f"), new DateTime(2025, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Sima8", 1, 8, 66.0 },
                    { new Guid("ac89233f-efbf-4c05-8b69-6598a4388b63"), new DateTime(2025, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Sima1", 1, 1, 66.0 },
                    { new Guid("af19443f-add8-4f27-abb2-4747429ab6b7"), new DateTime(2025, 5, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Mika11", 1, 11, 33.0 },
                    { new Guid("af51b546-cd94-4f46-9eca-d2770ac77a6f"), new DateTime(2025, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Pera13", 1, 13, 22.0 },
                    { new Guid("af6d2531-265d-4653-bf40-9a2e75cf92ef"), new DateTime(2025, 5, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Jova4", 1, 4, 55.0 },
                    { new Guid("b2f702b7-c13d-4510-b773-edb3701c4840"), new DateTime(2025, 5, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Pera9", 1, 9, 22.0 },
                    { new Guid("b70ecbc9-9439-473d-b734-c524c1e64c65"), new DateTime(2025, 5, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Mika9", 1, 9, 33.0 },
                    { new Guid("bb26cbc4-e9e3-412a-8963-bc39d9ae160c"), new DateTime(2025, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Laza12", 1, 12, 44.0 },
                    { new Guid("bba9c97b-0707-4b3b-8b47-8532be78ca99"), new DateTime(2025, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Pera14", 1, 14, 22.0 },
                    { new Guid("bc803d57-9185-4710-93d9-f2981e6e4242"), new DateTime(2025, 5, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Mika6", 1, 6, 33.0 },
                    { new Guid("c0839795-4123-4d40-b682-7a1874042698"), new DateTime(2025, 5, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Laza4", 1, 4, 44.0 },
                    { new Guid("c54c6207-2f4d-402a-88cd-88b3343a421d"), new DateTime(2025, 5, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Pera11", 1, 11, 22.0 },
                    { new Guid("c7515ad4-9435-4c04-8141-6b99d866e87c"), new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Laza10", 1, 10, 44.0 },
                    { new Guid("c86df92d-efae-4870-b6b4-24f31f95a17f"), new DateTime(2025, 5, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Cica3", 1, 3, 77.0 },
                    { new Guid("c8e390cd-76b8-4b20-b397-e18a3175a161"), new DateTime(2025, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Pera1", 1, 1, 22.0 },
                    { new Guid("ca262390-c139-4a7b-90d2-e70d58386ff6"), new DateTime(2025, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Jova13", 1, 13, 55.0 },
                    { new Guid("cd7e8e0b-9f30-4f1e-a4c3-de2dd05bd793"), new DateTime(2025, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Jova2", 1, 2, 55.0 },
                    { new Guid("cd8ba658-2f1a-4076-bcc9-c367d06c8bb3"), new DateTime(2025, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Laza2", 1, 2, 44.0 },
                    { new Guid("cf0a3ad4-901f-4f53-a130-0ba2841da7cb"), new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Jova10", 1, 10, 55.0 },
                    { new Guid("cfada7f8-326b-485f-bce1-9eb9dff30544"), new DateTime(2025, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Pera15", 1, 15, 22.0 },
                    { new Guid("d5570c47-e2ad-4460-8394-cddd851604d7"), new DateTime(2025, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Sima13", 1, 13, 66.0 },
                    { new Guid("d6d349be-1e52-463a-9ef5-3802f38e61fa"), new DateTime(2025, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Jova15", 1, 15, 55.0 },
                    { new Guid("dc748ffd-9b59-4fb4-8140-ced416cafa2d"), new DateTime(2025, 5, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Sima11", 1, 11, 66.0 },
                    { new Guid("dcde0d5f-47c8-454b-aafd-8dfb9a0052ab"), new DateTime(2025, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Laza15", 1, 15, 44.0 },
                    { new Guid("dcf37036-4689-466e-a869-07c2da3af52c"), new DateTime(2025, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Mika5", 1, 5, 33.0 },
                    { new Guid("df3439af-d580-472c-9c62-a459c2abe610"), new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Cica10", 1, 10, 77.0 },
                    { new Guid("dfe739a0-7876-490b-9ab7-9a480eb88600"), new DateTime(2025, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Cica15", 1, 15, 77.0 }
                });

            migrationBuilder.InsertData(
                table: "Monkeys",
                columns: new[] { "Id", "ArrivalDate", "DepartureDate", "Name", "ShelterId", "SpeciesId", "Weight" },
                values: new object[,]
                {
                    { new Guid("e0789f08-858d-42c9-9c23-aa4e97cde77d"), new DateTime(2025, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Laza13", 1, 13, 44.0 },
                    { new Guid("e861c6d5-8692-4a03-aa6d-43f1ba5a2bd0"), new DateTime(2025, 5, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Mika4", 1, 4, 33.0 },
                    { new Guid("eeb9bc8b-1277-4ec9-8817-cb3c05f73f07"), new DateTime(2025, 5, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Laza3", 1, 3, 44.0 },
                    { new Guid("f0e96d63-0ac5-4551-b13e-33bbad085d0f"), new DateTime(2025, 5, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Cica11", 1, 11, 77.0 },
                    { new Guid("f5fab2e8-c06d-42a6-92b2-bd07cedf1156"), new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Pera10", 1, 10, 22.0 },
                    { new Guid("fb07fc84-cde4-4c2e-80d0-8d7e153e916c"), new DateTime(2025, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Laza5", 1, 5, 44.0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Monkeys_ShelterId",
                table: "Monkeys",
                column: "ShelterId");

            migrationBuilder.CreateIndex(
                name: "IX_Monkeys_SpeciesId",
                table: "Monkeys",
                column: "SpeciesId");

            migrationBuilder.CreateIndex(
                name: "IX_VetChecks_MonkeyId",
                table: "VetChecks",
                column: "MonkeyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "VetChecks");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Monkeys");

            migrationBuilder.DropTable(
                name: "Shelters");

            migrationBuilder.DropTable(
                name: "Species");
        }
    }
}
