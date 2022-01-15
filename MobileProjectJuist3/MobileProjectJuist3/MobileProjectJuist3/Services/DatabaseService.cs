using MobileProjectJuist3.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileProjectJuist3.Services
{
    public class DatabaseService
    {

        private static DatabaseService DBService
        {
            get
            {
                if(DBService == null)
                {
                    DBService = new DatabaseService(App.DatabasePath);
                }
                return DBService;
            }
            set {}
        }

        public static SQLiteAsyncConnection Database
        {
            get
            {
                return DBService.database;
            }
            private set { }
        }

        readonly SQLiteAsyncConnection database;

        private DatabaseService(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<User>().Wait();
        }
    }
}
