using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AWS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddFileFormat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileFormat",
                table: "AwsFiles",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileFormat",
                table: "AwsFiles");
        }
    }
}
