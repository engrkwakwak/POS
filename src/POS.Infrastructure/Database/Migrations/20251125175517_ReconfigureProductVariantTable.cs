using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POS.Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class ReconfigureProductVariantTable : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "unit_of_measure_items_per_case",
            schema: "public",
            table: "product_variants");

        migrationBuilder.RenameColumn(
            name: "unit_of_measure_type",
            schema: "public",
            table: "product_variants",
            newName: "packaging_type");

        migrationBuilder.AlterColumn<decimal>(
            name: "price_amount",
            schema: "public",
            table: "product_variants",
            type: "numeric(18,2)",
            precision: 18,
            scale: 2,
            nullable: false,
            oldClrType: typeof(decimal),
            oldType: "numeric");

        migrationBuilder.AlterColumn<decimal>(
            name: "package_size_value",
            schema: "public",
            table: "product_variants",
            type: "numeric(18,2)",
            precision: 18,
            scale: 2,
            nullable: false,
            oldClrType: typeof(decimal),
            oldType: "numeric");

        migrationBuilder.AlterColumn<string>(
            name: "package_size_unit",
            schema: "public",
            table: "product_variants",
            type: "character varying(10)",
            maxLength: 10,
            nullable: false,
            oldClrType: typeof(int),
            oldType: "integer");

        migrationBuilder.AlterColumn<bool>(
            name: "is_vatable",
            schema: "public",
            table: "product_variants",
            type: "boolean",
            nullable: false,
            defaultValue: true,
            oldClrType: typeof(bool),
            oldType: "boolean");

        migrationBuilder.AlterColumn<string>(
            name: "barcode",
            schema: "public",
            table: "product_variants",
            type: "character varying(20)",
            maxLength: 20,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "character varying(100)",
            oldMaxLength: 100);

        migrationBuilder.AlterColumn<string>(
            name: "packaging_type",
            schema: "public",
            table: "product_variants",
            type: "text",
            nullable: false,
            oldClrType: typeof(int),
            oldType: "integer");

        migrationBuilder.AddColumn<int>(
            name: "packaging_quantity",
            schema: "public",
            table: "product_variants",
            type: "integer",
            nullable: false,
            defaultValue: 1);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "packaging_quantity",
            schema: "public",
            table: "product_variants");

        migrationBuilder.RenameColumn(
            name: "packaging_type",
            schema: "public",
            table: "product_variants",
            newName: "unit_of_measure_type");

        migrationBuilder.AlterColumn<decimal>(
            name: "price_amount",
            schema: "public",
            table: "product_variants",
            type: "numeric",
            nullable: false,
            oldClrType: typeof(decimal),
            oldType: "numeric(18,2)",
            oldPrecision: 18,
            oldScale: 2);

        migrationBuilder.AlterColumn<decimal>(
            name: "package_size_value",
            schema: "public",
            table: "product_variants",
            type: "numeric",
            nullable: false,
            oldClrType: typeof(decimal),
            oldType: "numeric(18,2)",
            oldPrecision: 18,
            oldScale: 2);

        migrationBuilder.AlterColumn<int>(
            name: "package_size_unit",
            schema: "public",
            table: "product_variants",
            type: "integer",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "character varying(10)",
            oldMaxLength: 10);

        migrationBuilder.AlterColumn<bool>(
            name: "is_vatable",
            schema: "public",
            table: "product_variants",
            type: "boolean",
            nullable: false,
            oldClrType: typeof(bool),
            oldType: "boolean",
            oldDefaultValue: true);

        migrationBuilder.AlterColumn<string>(
            name: "barcode",
            schema: "public",
            table: "product_variants",
            type: "character varying(100)",
            maxLength: 100,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "character varying(20)",
            oldMaxLength: 20);

        migrationBuilder.AlterColumn<int>(
            name: "unit_of_measure_type",
            schema: "public",
            table: "product_variants",
            type: "integer",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "text");

        migrationBuilder.AddColumn<int>(
            name: "unit_of_measure_items_per_case",
            schema: "public",
            table: "product_variants",
            type: "integer",
            nullable: true);
    }
}
