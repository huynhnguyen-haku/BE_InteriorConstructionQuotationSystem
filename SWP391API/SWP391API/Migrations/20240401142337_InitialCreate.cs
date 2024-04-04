using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWP391API.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "article_types",
                columns: table => new
                {
                    article_type_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    article_type_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_article_types", x => x.article_type_id);
                });

            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "construction_style",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    construction_type = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_construction_style", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "home_style",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_home_style", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "role",
                columns: table => new
                {
                    role_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    role_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role", x => x.role_id);
                });

            migrationBuilder.CreateTable(
                name: "style",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    price = table.Column<double>(type: "float", nullable: true),
                    description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_style", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    fullname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    birthdate = table.Column<DateTime>(type: "date", nullable: true),
                    email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    phone_number = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    avt_url = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    role_id = table.Column<int>(type: "int", nullable: false),
                    token = table.Column<string>(type: "nvarchar(3000)", maxLength: 3000, nullable: true),
                    expireDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    status = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.user_id);
                    table.ForeignKey(
                        name: "FK__user__role_id__7A672E12",
                        column: x => x.role_id,
                        principalTable: "role",
                        principalColumn: "role_id");
                });

            migrationBuilder.CreateTable(
                name: "article",
                columns: table => new
                {
                    article_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    content = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    article_type_id = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "date", nullable: false),
                    status = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((1))"),
                    Img = table.Column<string>(type: "ntext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_article", x => x.article_id);
                    table.ForeignKey(
                        name: "FK__article__article__6C190EBB",
                        column: x => x.article_type_id,
                        principalTable: "article_types",
                        principalColumn: "article_type_id");
                    table.ForeignKey(
                        name: "FK__article__user_id__6D0D32F4",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "completedProject",
                columns: table => new
                {
                    project_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    style_id = table.Column<int>(type: "int", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    project_title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    project_description = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    project_image = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    startDate = table.Column<DateTime>(type: "date", nullable: true),
                    endDate = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__complete__BC799E1F43E8F264", x => x.project_id);
                    table.ForeignKey(
                        name: "FK__completed__style__6E01572D",
                        column: x => x.style_id,
                        principalTable: "style",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__completed__user___6EF57B66",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "product",
                columns: table => new
                {
                    product_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    category_id = table.Column<int>(type: "int", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    size = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    image_url = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    status = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product", x => x.product_id);
                    table.ForeignKey(
                        name: "FK__product__categor__70DDC3D8",
                        column: x => x.category_id,
                        principalTable: "categories",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__product__user_id__71D1E811",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "quotation",
                columns: table => new
                {
                    quotation_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    quotation_status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    style_id = table.Column<int>(type: "int", nullable: true),
                    square = table.Column<double>(type: "float", nullable: true),
                    totalBill = table.Column<double>(type: "float", nullable: true),
                    status = table.Column<int>(type: "int", nullable: true),
                    user_id = table.Column<int>(type: "int", nullable: true),
                    witdh = table.Column<double>(type: "float", nullable: true),
                    height = table.Column<double>(type: "float", nullable: true),
                    length = table.Column<double>(type: "float", nullable: true),
                    totalConstructionCost = table.Column<double>(type: "float", nullable: true),
                    totalProductCost = table.Column<double>(type: "float", nullable: true),
                    homeStyleId = table.Column<int>(type: "int", nullable: true),
                    floorConstructionId = table.Column<int>(type: "int", nullable: true),
                    WallConstructId = table.Column<int>(type: "int", nullable: true),
                    CeilingConstructId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_quotation", x => x.quotation_id);
                    table.ForeignKey(
                        name: "FK_quotation_construction_CeilingConstructIdstyle",
                        column: x => x.CeilingConstructId,
                        principalTable: "construction_style",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_quotation_construction_floorConstructionIdstyle",
                        column: x => x.floorConstructionId,
                        principalTable: "construction_style",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_quotation_construction_WallConstructIdstyle",
                        column: x => x.WallConstructId,
                        principalTable: "construction_style",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_quotation_home_style",
                        column: x => x.homeStyleId,
                        principalTable: "home_style",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_quotation_style",
                        column: x => x.style_id,
                        principalTable: "style",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_quotation_user",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "ProductInProject",
                columns: table => new
                {
                    product_id = table.Column<int>(type: "int", nullable: false),
                    project_id = table.Column<int>(type: "int", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductInProject", x => new { x.product_id, x.project_id });
                    table.ForeignKey(
                        name: "FK_ProductInProject_completedProject",
                        column: x => x.project_id,
                        principalTable: "completedProject",
                        principalColumn: "project_id");
                    table.ForeignKey(
                        name: "FK_ProductInProject_product",
                        column: x => x.product_id,
                        principalTable: "product",
                        principalColumn: "product_id");
                });

            migrationBuilder.CreateTable(
                name: "quotation_temp",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false),
                    product_id = table.Column<int>(type: "int", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_quotation_temp", x => new { x.user_id, x.product_id });
                    table.ForeignKey(
                        name: "FK_quotation_temp_product",
                        column: x => x.product_id,
                        principalTable: "product",
                        principalColumn: "product_id");
                    table.ForeignKey(
                        name: "FK_quotation_temp_user",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "contract",
                columns: table => new
                {
                    contract_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    quotation_id = table.Column<int>(type: "int", nullable: true),
                    contract_status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contract", x => x.contract_id);
                    table.ForeignKey(
                        name: "FK__contract__quotat__6FE99F9F",
                        column: x => x.quotation_id,
                        principalTable: "quotation",
                        principalColumn: "quotation_id");
                });

            migrationBuilder.CreateTable(
                name: "quotation_detail",
                columns: table => new
                {
                    quotation_d_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    quotation_id = table.Column<int>(type: "int", nullable: true),
                    product_id = table.Column<int>(type: "int", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    price = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__quotatio__F90A1A865964317E", x => x.quotation_d_id);
                    table.ForeignKey(
                        name: "FK__quotation__produ__440B1D61",
                        column: x => x.product_id,
                        principalTable: "product",
                        principalColumn: "product_id");
                    table.ForeignKey(
                        name: "FK_quotation_detail_quotation",
                        column: x => x.quotation_id,
                        principalTable: "quotation",
                        principalColumn: "quotation_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_article_article_type_id",
                table: "article",
                column: "article_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_article_user_id",
                table: "article",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_completedProject_style_id",
                table: "completedProject",
                column: "style_id");

            migrationBuilder.CreateIndex(
                name: "IX_completedProject_user_id",
                table: "completedProject",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_contract_quotation_id",
                table: "contract",
                column: "quotation_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_category_id",
                table: "product",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_user_id",
                table: "product",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductInProject_project_id",
                table: "ProductInProject",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "IX_quotation_CeilingConstructId",
                table: "quotation",
                column: "CeilingConstructId");

            migrationBuilder.CreateIndex(
                name: "IX_quotation_floorConstructionId",
                table: "quotation",
                column: "floorConstructionId");

            migrationBuilder.CreateIndex(
                name: "IX_quotation_homeStyleId",
                table: "quotation",
                column: "homeStyleId");

            migrationBuilder.CreateIndex(
                name: "IX_quotation_style_id",
                table: "quotation",
                column: "style_id");

            migrationBuilder.CreateIndex(
                name: "IX_quotation_user_id",
                table: "quotation",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_quotation_WallConstructId",
                table: "quotation",
                column: "WallConstructId");

            migrationBuilder.CreateIndex(
                name: "IX_quotation_detail_product_id",
                table: "quotation_detail",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_quotation_detail_quotation_id",
                table: "quotation_detail",
                column: "quotation_id");

            migrationBuilder.CreateIndex(
                name: "IX_quotation_temp_product_id",
                table: "quotation_temp",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_role_id",
                table: "user",
                column: "role_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "article");

            migrationBuilder.DropTable(
                name: "contract");

            migrationBuilder.DropTable(
                name: "ProductInProject");

            migrationBuilder.DropTable(
                name: "quotation_detail");

            migrationBuilder.DropTable(
                name: "quotation_temp");

            migrationBuilder.DropTable(
                name: "article_types");

            migrationBuilder.DropTable(
                name: "completedProject");

            migrationBuilder.DropTable(
                name: "quotation");

            migrationBuilder.DropTable(
                name: "product");

            migrationBuilder.DropTable(
                name: "construction_style");

            migrationBuilder.DropTable(
                name: "home_style");

            migrationBuilder.DropTable(
                name: "style");

            migrationBuilder.DropTable(
                name: "categories");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "role");
        }
    }
}
