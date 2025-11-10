using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POS.Infrastructure.Migrations;

/// <inheritdoc />
public partial class Add_Product_And_Brand_Tables : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(
            name: "public");

        migrationBuilder.CreateTable(
            name: "brands",
            schema: "public",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
            },
            constraints: table => table.PrimaryKey("pk_brands", x => x.id));

        migrationBuilder.CreateTable(
            name: "outbox_messages",
            schema: "public",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                name = table.Column<string>(type: "text", nullable: false),
                content = table.Column<string>(type: "jsonb", nullable: false),
                created_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                processed_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                error = table.Column<string>(type: "text", nullable: true)
            },
            constraints: table => table.PrimaryKey("pk_outbox_messages", x => x.id));

        migrationBuilder.CreateTable(
            name: "products",
            schema: "public",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                product_code = table.Column<string>(type: "text", nullable: false),
                category = table.Column<int>(type: "integer", nullable: false),
                brand_id = table.Column<Guid>(type: "uuid", nullable: false),
                is_vatable = table.Column<bool>(type: "boolean", nullable: false),
                description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
            },
            constraints: table => table.PrimaryKey("pk_products", x => x.id));

        migrationBuilder.CreateTable(
            name: "product_variants",
            schema: "public",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                product_id = table.Column<Guid>(type: "uuid", nullable: false),
                sku = table.Column<string>(type: "character varying(12)", maxLength: 12, nullable: false),
                barcode = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                is_vatable = table.Column<bool>(type: "boolean", nullable: false),
                price_amount = table.Column<decimal>(type: "numeric", nullable: false),
                price_currency = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                package_size_unit = table.Column<int>(type: "integer", nullable: false),
                package_size_value = table.Column<decimal>(type: "numeric", nullable: false),
                unit_of_measure_items_per_case = table.Column<int>(type: "integer", nullable: true),
                unit_of_measure_type = table.Column<int>(type: "integer", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_product_variants", x => x.id);
                table.ForeignKey(
                    name: "fk_product_variants_products_product_id",
                    column: x => x.product_id,
                    principalSchema: "public",
                    principalTable: "products",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateIndex(
            name: "ix_product_variants_barcode",
            schema: "public",
            table: "product_variants",
            column: "barcode",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "ix_product_variants_product_id",
            schema: "public",
            table: "product_variants",
            column: "product_id");

        migrationBuilder.CreateIndex(
            name: "ix_product_variants_sku",
            schema: "public",
            table: "product_variants",
            column: "sku",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "ix_products_product_code",
            schema: "public",
            table: "products",
            column: "product_code",
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "brands",
            schema: "public");

        migrationBuilder.DropTable(
            name: "outbox_messages",
            schema: "public");

        migrationBuilder.DropTable(
            name: "product_variants",
            schema: "public");

        migrationBuilder.DropTable(
            name: "products",
            schema: "public");
    }
}
