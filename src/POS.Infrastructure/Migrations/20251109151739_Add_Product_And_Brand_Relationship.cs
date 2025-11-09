using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POS.Infrastructure.Migrations;

/// <inheritdoc />
public partial class Add_Product_And_Brand_Relationship : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateIndex(
            name: "ix_products_brand_id",
            schema: "public",
            table: "products",
            column: "brand_id");

        migrationBuilder.AddForeignKey(
            name: "fk_products_brands_brand_id",
            schema: "public",
            table: "products",
            column: "brand_id",
            principalSchema: "public",
            principalTable: "brands",
            principalColumn: "id",
            onDelete: ReferentialAction.Restrict);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "fk_products_brands_brand_id",
            schema: "public",
            table: "products");

        migrationBuilder.DropIndex(
            name: "ix_products_brand_id",
            schema: "public",
            table: "products");
    }
}
