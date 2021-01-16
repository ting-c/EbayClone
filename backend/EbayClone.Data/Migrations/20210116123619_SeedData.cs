using Microsoft.EntityFrameworkCore.Migrations;

namespace EbayClone.Data.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
		{
			// Users
			migrationBuilder
				.Sql("INSERT INTO Users (FirstName, LastName, UserName, Email, EmailConfirmed, PhoneNumber, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnabled, AccessFailedCount, Address) Values ('Adam', 'Smith', 'adamsmith123', 'adamsmith@gmail.com', 'TRUE', '01233111111', 'TRUE', 'FALSE', 'FALSE', 0, '1 London Road, Greater London, UK')");
			migrationBuilder
				.Sql("INSERT INTO Users (FirstName, LastName, UserName, Email, EmailConfirmed, PhoneNumber, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnabled, AccessFailedCount, Address) Values ('Brad', 'Johnson', 'bradjohnson123', 'bradjohnson@gmail.com', 'TRUE', '01233222222', 'TRUE', 'FALSE', 'FALSE', 0, '2 London Road, Greater London, UK')");
			migrationBuilder
				.Sql("INSERT INTO Users (FirstName, LastName, UserName, Email, EmailConfirmed, PhoneNumber, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnabled, AccessFailedCount, Address) Values ('Charles', 'Moore', 'charlesmoore123', 'charlesmoore@gmail.com', 'TRUE', '01233333333', 'TRUE', 'FALSE', 'FALSE', 0, '3 London Road, Greater London, UK')");
			migrationBuilder
				.Sql("INSERT INTO Users (FirstName, LastName, UserName, Email, EmailConfirmed, PhoneNumber, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnabled, AccessFailedCount, Address) Values ('Darren', 'Bentley', 'darrenbentley123', 'darrenbentley@gmail.com', 'TRUE', '01233444444', 'TRUE', 'FALSE', 'FALSE', 0, '4 London Road, Greater London, UK')");

			// Items - Adam
			migrationBuilder
				.Sql("INSERT INTO Items (Title, Price, Quantity, Condition, IsAuction,SellerId) Values ('New Nike Men Trainers Size 10', 30.00, 10, 'New', 'FALSE', (SELECT Id FROM Users WHERE FirstName = 'Adam'))");
			migrationBuilder
				.Sql("INSERT INTO Items (Title, Price, Quantity, Condition, IsAuction,SellerId) Values ('New Nike Ladies Trainers Size 4', 35.00, 20, 'New', 'FALSE', (SELECT Id FROM Users WHERE FirstName = 'Adam'))");
			migrationBuilder
				.Sql("INSERT INTO Items (Title, Price, Quantity, Condition, IsAuction,SellerId) Values ('New Fila Men''s Camalfi Trainers Size 8', 40.00, 5, 'New', 'FALSE', (SELECT Id FROM Users WHERE FirstName = 'Adam'))");

			// Items - Brad
			migrationBuilder
				.Sql("INSERT INTO Items (Title, Price, Quantity, Condition, IsAuction,SellerId) Values ('Lindens Pro Billion X20 Capsules 20bn CFU(60 pack)', 20.00, 10, 'New', 'FALSE', (SELECT Id FROM Users WHERE FirstName = 'Brad'))");
			migrationBuilder
				.Sql("INSERT INTO Items (Title, Price, Quantity, Condition, IsAuction,SellerId) Values ('Zinc 30 Lozenges Aniseed With Acerola Vitamin C Immunity Support Lindens', 25.00, 20, 'New', 'FALSE', (SELECT Id FROM Users WHERE FirstName = 'Brad'))");
			migrationBuilder
				.Sql("INSERT INTO Items (Title, Price, Quantity, Condition, IsAuction,SellerId) Values ('Green Tea Extract 1000mg -slimming pills diet, weight loss (100 tablets)', 35.00, 30, 'New', 'FALSE', (SELECT Id FROM Users WHERE FirstName = 'Brad'))");

			// Items - Charles
			migrationBuilder
				.Sql("INSERT INTO Items (Title, Price, Quantity, Condition, IsAuction,SellerId) Values ('Call of Duty: Black Ops Cold War (PS4)', 45.00, 10, 'New', 'FALSE', (SELECT Id FROM Users WHERE FirstName = 'Charles'))");
			migrationBuilder
				.Sql("INSERT INTO Items (Title, Price, Quantity, Condition, IsAuction,SellerId) Values ('Assassins Creed Valhalla (PS4)', 40.00, 20, 'New', 'FALSE', (SELECT Id FROM Users WHERE FirstName = 'Charles'))");
			migrationBuilder
				.Sql("INSERT INTO Items (Title, Price, Quantity, Condition, IsAuction,SellerId) Values ('Marvel''s Spider-Man: Miles Morales (PS5) Brand New & Sealed', 60.00, 30, 'New', 'FALSE', (SELECT Id FROM Users WHERE FirstName = 'Charles'))");

			// Items - Darren
			migrationBuilder
				.Sql("INSERT INTO Items (Title, Price, Quantity, Condition, IsAuction,SellerId) Values ('Duck Feather Pillows 100% Cotton Cover Filled Luxury Hotel Quality Pillow - 4 Pack', 30.00, 15, 'New', 'FALSE', (SELECT Id FROM Users WHERE FirstName = 'Darren'))");
			migrationBuilder
				.Sql("INSERT INTO Items (Title, Price, Quantity, Condition, IsAuction,SellerId) Values ('Hotel Quality Egyptian Stripe Pillows Luxury Soft Hollowfibre Filled - 4 Pack', 30.00, 25, 'New', 'FALSE', (SELECT Id FROM Users WHERE FirstName = 'Darren'))");
			migrationBuilder
				.Sql("INSERT INTO Items (Title, Price, Quantity, Condition, IsAuction,SellerId) Values ('Pillows Quilted Luxury Ultra Loft Jumbo Super Bounce Back Pillows - 4 Pack', 30.00, 35, 'New', 'FALSE', (SELECT Id FROM Users WHERE FirstName = 'Darren'))");

			// FilePaths - Adam
			migrationBuilder
				.Sql("INSERT INTO FilePaths (UrlPath, ItemId, UserId) Values ('https://i.ibb.co/9Y9LqPn/shoes1.jpg', (SELECT Id FROM Items WHERE Title = 'New Nike Men Trainers Size 10'), (SELECT Id FROM Users WHERE FirstName = 'Adam'))");
			migrationBuilder
				.Sql("INSERT INTO FilePaths (UrlPath, ItemId, UserId) Values ('https://i.ibb.co/qBS9pwr/shoes2.jpg', (SELECT Id FROM Items WHERE Title = 'New Nike Ladies Trainers Size 4'), (SELECT Id FROM Users WHERE FirstName = 'Adam'))");
			migrationBuilder
				.Sql("INSERT INTO FilePaths (UrlPath, ItemId, UserId) Values ('https://i.ibb.co/gW1DkBG/shoes3.jpg', (SELECT Id FROM Items WHERE Title = 'New Fila Men''s Camalfi Trainers Size 8'), (SELECT Id FROM Users WHERE FirstName = 'Adam'))");

			// Filepaths - Brad
			migrationBuilder
				.Sql("INSERT INTO FilePaths (UrlPath, ItemId, UserId) Values ('https://i.ibb.co/jvb8cqt/pills1.jpg', (SELECT Id FROM Items WHERE Title = 'Lindens Pro Billion X20 Capsules 20bn CFU(60 pack)'), (SELECT Id FROM Users WHERE FirstName = 'Brad'))");
			migrationBuilder
				.Sql("INSERT INTO FilePaths (UrlPath, ItemId, UserId) Values ('https://i.ibb.co/5F8HjkC/pills2.jpg', (SELECT Id FROM Items WHERE Title = 'Zinc 30 Lozenges Aniseed With Acerola Vitamin C Immunity Support Lindens'), (SELECT Id FROM Users WHERE FirstName = 'Brad'))");
			migrationBuilder
				.Sql("INSERT INTO FilePaths (UrlPath, ItemId, UserId) Values ('https://i.ibb.co/d0wVt34/pills3.jpg', (SELECT Id FROM Items WHERE Title = 'Green Tea Extract 1000mg -slimming pills diet, weight loss (100 tablets)'), (SELECT Id FROM Users WHERE FirstName = 'Brad'))");

			// Filepaths - Charles
			migrationBuilder
				.Sql("INSERT INTO FilePaths (UrlPath, ItemId, UserId) Values ('https://i.ibb.co/KxBDDxB/blackops.jpg', (SELECT Id FROM Items WHERE Title = 'Call of Duty: Black Ops Cold War (PS4)'), (SELECT Id FROM Users WHERE FirstName = 'Charles'))");
			migrationBuilder
				.Sql("INSERT INTO FilePaths (UrlPath, ItemId, UserId) Values ('https://i.ibb.co/YZhCwSr/assassinscreed.jpg', (SELECT Id FROM Items WHERE Title = 'Assassins Creed Valhalla (PS4)'), (SELECT Id FROM Users WHERE FirstName = 'Charles'))");
			migrationBuilder
				.Sql("INSERT INTO FilePaths (UrlPath, ItemId, UserId) Values ('https://i.ibb.co/JF73JZz/spiderman.jpg', (SELECT Id FROM Items WHERE Title = 'Marvel''s Spider-Man: Miles Morales (PS5) Brand New & Sealed'), (SELECT Id FROM Users WHERE FirstName = 'Charles'))");

			// Filepaths - Darren
			migrationBuilder
				.Sql("INSERT INTO FilePaths (UrlPath, ItemId, UserId) Values ('https://i.ibb.co/M1ZwsRY/pillow1.jpg', (SELECT Id FROM Items WHERE Title = 'Duck Feather Pillows 100% Cotton Cover Filled Luxury Hotel Quality Pillow - 4 Pack'), (SELECT Id FROM Users WHERE FirstName = 'Darren'))");
			migrationBuilder
				.Sql("INSERT INTO FilePaths (UrlPath, ItemId, UserId) Values ('https://i.ibb.co/SKfTwsX/pillow2.jpg', (SELECT Id FROM Items WHERE Title = 'Hotel Quality Egyptian Stripe Pillows Luxury Soft Hollowfibre Filled - 4 Pack'), (SELECT Id FROM Users WHERE FirstName = 'Darren'))");
			migrationBuilder
				.Sql("INSERT INTO FilePaths (UrlPath, ItemId, UserId) Values ('https://i.ibb.co/dLkjTdb/pillow3.jpg', (SELECT Id FROM Items WHERE Title = 'Pillows Quilted Luxury Ultra Loft Jumbo Super Bounce Back Pillows - 4 Pack'), (SELECT Id FROM Users WHERE FirstName = 'Darren'))");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder
				.Sql("DELETE FROM Items");
			migrationBuilder
				.Sql("DELETE FROM Users");
			migrationBuilder
				.Sql("DELETE FROM FilePaths");
		}
    }
}
