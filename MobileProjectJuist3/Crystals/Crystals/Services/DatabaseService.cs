using Crystals.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Crystals.Services
{
    public class DatabaseService
    {

        public readonly SQLiteAsyncConnection database;

        public DatabaseService(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<User>().Wait();
            database.CreateTableAsync<Crystal>().Wait();
        }
    }
}
