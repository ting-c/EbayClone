using Microsoft.EntityFrameworkCore.Migrations;

namespace EbayClone.Data.Migrations
{
    public partial class SeedItemsAndUsersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder
                .Sql("INSERT INTO Users (Name) Values ('Adam Smith')");
            migrationBuilder
                .Sql("INSERT INTO Users (Name) Values ('Brad Johnson')");
            migrationBuilder
                .Sql("INSERT INTO Users (Name) Values ('Charles Moore')");
            migrationBuilder
                .Sql("INSERT INTO Users (Name) Values ('Darren Bentley')");

            migrationBuilder
                .Sql("INSERT INTO Items (Title, SellerId) Values ('New Nike Men Trainers Size 10', (SELECT Id FROM Users WHERE Name = 'Adam Smith'))");
            migrationBuilder
                .Sql("INSERT INTO Items (Title, SellerId) Values ('New Adidas Ladies Trainers Size 4', (SELECT Id FROM Users WHERE Name = 'Adam Smith'))");
            migrationBuilder
                .Sql("INSERT INTO Items (Title, SellerId) Values ('New Fila Men's Camalfi Trainers Size 8', (SELECT Id FROM Users WHERE Name = 'Adam Smith'))");

            migrationBuilder
                .Sql("INSERT INTO Items (Title, SellerId) Values ('Lindens Pro Billion X20 Capsules 20bn CFU(60 pack)', (SELECT Id FROM Users WHERE Name = 'Brad Johnson'))");
            migrationBuilder
                .Sql("INSERT INTO Items (Title, SellerId) Values ('Zinc 30 Lozenges Aniseed With Acerola Vitamin C Immunity Support Lindens', (SELECT Id FROM Users WHERE Name = 'Brad Johnson'))");
            migrationBuilder
                .Sql("INSERT INTO Items (Title, SellerId) Values ('Green Tea Extract 1000mg -slimming pills diet, weight loss (100 tablets)', (SELECT Id FROM Users WHERE Name = 'Brad Johnson'))");

            migrationBuilder
                .Sql("INSERT INTO Items (Title, SellerId) Values ('Call of Duty: Black Ops Cold War (PS4)', (SELECT Id FROM Users WHERE Name = 'Charles Moore'))");
            migrationBuilder
                .Sql("INSERT INTO Items (Title, SellerId) Values ('Assassins Creed Valhalla (PS4)', (SELECT Id FROM Users WHERE Name = 'Charles Moore'))");
            migrationBuilder
                .Sql("INSERT INTO Items (Title, SellerId) Values ('Marvel's Spider-Man: Miles Morales (PS5) Brand New & Sealed', (SELECT Id FROM Users WHERE Name = 'Charles Moore'))");

            migrationBuilder
                .Sql("INSERT INTO Items (Title, SellerId) Values ('Duck Feather Pillows 100% Cotton Cover Filled Luxury Hotel Quality Pillow - 4 Pack', (SELECT Id FROM Users WHERE Name = 'Darren Bentley'))");
            migrationBuilder
                .Sql("INSERT INTO Items (Title, SellerId) Values ('Hotel Quality Egyptian Stripe Pillows Luxury Soft Hollowfibre Filled - 4 Pack', (SELECT Id FROM Users WHERE Name = 'Darren Bentley'))");
            migrationBuilder
                .Sql("INSERT INTO Items (Title, SellerId) Values ('Pillows Quilted Luxury Ultra Loft Jumbo Super Bounce Back Pillows - 4 Pack', (SELECT Id FROM Users WHERE Name = 'Darren Bentley'))");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder
                .Sql("DELETE FROM Items");
            migrationBuilder
                .Sql("DELETE FROM Users");
        }
    }
}
