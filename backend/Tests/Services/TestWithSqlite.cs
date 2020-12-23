using System;
using System.Threading.Tasks;
using EbayClone.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Tests.Services
{
	// create a base class for creating and disposing the in-memory Sqlite database.
	public abstract class TestWithSqlite : IDisposable
    {
        private const string InMemoryConnectionString = "DataSource=:memory:";
        private readonly SqliteConnection _connection;

        protected readonly EbayCloneDbContext _dbContext;

        protected TestWithSqlite()
        {
            _connection = new SqliteConnection(InMemoryConnectionString);
            _connection.Open();
            var options = new DbContextOptionsBuilder<EbayCloneDbContext>()
                .UseSqlite(_connection)
                .Options;
            _dbContext = new EbayCloneDbContext(options);
            _dbContext.Database.EnsureCreated();
        }
        
        public void Dispose()
        {
            _connection.Close();
        }
    }

    // to test setup
    public class EbayCloneDbContextTests : TestWithSqlite
    {
        [Fact]
        public async Task DatabaseIsAvailableAndCanBeConnectedTo()
        {
            Assert.True(await _dbContext.Database.CanConnectAsync());
        }
    }
}