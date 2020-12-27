using Microsoft.EntityFrameworkCore.Migrations;

namespace EbayClone.Data.Migrations
{
    public partial class SeedItemsAndUsersData : Migration
    {
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder
				.Sql("INSERT INTO Users (FirstName, LastName, UserName, Email, EmailConfirmed, PhoneNumber, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnabled, AccessFailedCount, Address) Values ('Adam', 'Smith', 'adamsmith123', 'adamsmith@gmail.com', 'TRUE', '01233111111', 'TRUE', 'FALSE', 'FALSE', 0, '1 London Road, Greater London, UK')");
			migrationBuilder
				.Sql("INSERT INTO Users (FirstName, LastName, UserName, Email, EmailConfirmed, PhoneNumber, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnabled, AccessFailedCount, Address) Values ('Brad', 'Johnson', 'bradjohnson123', 'bradjohnson@gmail.com', 'TRUE', '01233222222', 'TRUE', 'FALSE', 'FALSE', 0, '2 London Road, Greater London, UK')");
			migrationBuilder
				.Sql("INSERT INTO Users (FirstName, LastName, UserName, Email, EmailConfirmed, PhoneNumber, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnabled, AccessFailedCount, Address) Values ('Charles', 'Moore', 'charlesmoore123', 'charlesmoore@gmail.com', 'TRUE', '01233333333', 'TRUE', 'FALSE', 'FALSE', 0, '3 London Road, Greater London, UK')");
			migrationBuilder
				.Sql("INSERT INTO Users (FirstName, LastName, UserName, Email, EmailConfirmed, PhoneNumber, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnabled, AccessFailedCount, Address) Values ('Darren', 'Bentley', 'darrenbentley123', 'darrenbentley@gmail.com', 'TRUE', '01233444444', 'TRUE', 'FALSE', 'FALSE', 0, '4 London Road, Greater London, UK')");

			migrationBuilder
				.Sql("INSERT INTO Items (Title, Price, Condition, IsAuction,SellerId) Values ('New Nike Men Trainers Size 10', 30.00, 'New', 'FALSE', (SELECT Id FROM Users WHERE FirstName = 'Adam'))");
			migrationBuilder
				.Sql("INSERT INTO Items (Title, Price, Condition, IsAuction,SellerId) Values ('New Adidas Ladies Trainers Size 4', 35.00, 'New', 'FALSE', (SELECT Id FROM Users WHERE FirstName = 'Adam'))");
			migrationBuilder
				.Sql("INSERT INTO Items (Title, Price, Condition, IsAuction,SellerId) Values ('New Fila Men''s Camalfi Trainers Size 8', 40.00, 'New', 'FALSE', (SELECT Id FROM Users WHERE FirstName = 'Adam'))");

			migrationBuilder
				.Sql("INSERT INTO Items (Title, Price, Condition, IsAuction,SellerId) Values ('Lindens Pro Billion X20 Capsules 20bn CFU(60 pack)', 20.00, 'New', 'FALSE', (SELECT Id FROM Users WHERE FirstName = 'Brad'))");
			migrationBuilder
				.Sql("INSERT INTO Items (Title, Price, Condition, IsAuction,SellerId) Values ('Zinc 30 Lozenges Aniseed With Acerola Vitamin C Immunity Support Lindens', 25.00, 'New', 'FALSE', (SELECT Id FROM Users WHERE FirstName = 'Brad'))");
			migrationBuilder
				.Sql("INSERT INTO Items (Title, Price, Condition, IsAuction,SellerId) Values ('Green Tea Extract 1000mg -slimming pills diet, weight loss (100 tablets)', 35.00, 'New', 'FALSE', (SELECT Id FROM Users WHERE FirstName = 'Brad'))");

			migrationBuilder
				.Sql("INSERT INTO Items (Title, Price, Condition, IsAuction,SellerId) Values ('Call of Duty: Black Ops Cold War (PS4)', 45.00, 'New', 'FALSE', (SELECT Id FROM Users WHERE FirstName = 'Charles'))");
			migrationBuilder
				.Sql("INSERT INTO Items (Title, Price, Condition, IsAuction,SellerId) Values ('Assassins Creed Valhalla (PS4)', 40.00, 'New', 'FALSE', (SELECT Id FROM Users WHERE FirstName = 'Charles'))");
			migrationBuilder
				.Sql("INSERT INTO Items (Title, Price, Condition, IsAuction,SellerId) Values ('Marvel''s Spider-Man: Miles Morales (PS5) Brand New & Sealed', 60.00, 'New', 'FALSE', (SELECT Id FROM Users WHERE FirstName = 'Charles'))");

			migrationBuilder
				.Sql("INSERT INTO Items (Title, Price, Condition, IsAuction,SellerId) Values ('Duck Feather Pillows 100% Cotton Cover Filled Luxury Hotel Quality Pillow - 4 Pack', 30.00, 'New', 'FALSE', (SELECT Id FROM Users WHERE FirstName = 'Darren'))");
			migrationBuilder
				.Sql("INSERT INTO Items (Title, Price, Condition, IsAuction,SellerId) Values ('Hotel Quality Egyptian Stripe Pillows Luxury Soft Hollowfibre Filled - 4 Pack', 30.00, 'New', 'FALSE', (SELECT Id FROM Users WHERE FirstName = 'Darren'))");
			migrationBuilder
				.Sql("INSERT INTO Items (Title, Price, Condition, IsAuction,SellerId) Values ('Pillows Quilted Luxury Ultra Loft Jumbo Super Bounce Back Pillows - 4 Pack', 30.00, 'New', 'FALSE', (SELECT Id FROM Users WHERE FirstName = 'Darren'))");
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
