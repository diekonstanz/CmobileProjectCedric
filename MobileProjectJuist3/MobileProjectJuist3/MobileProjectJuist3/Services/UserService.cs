using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using MobileProjectJuist3.Models;
using System.Threading.Tasks;

namespace MobileProjectJuist3.Services
{
    public class UserService
    {
        public static Task<List<User>> GetUsersAsync()
        {
            //Get all Users.
            return App.DatabaseService.database.Table<User>().ToListAsync();
        }

        public static Task<User> GetUserAsync(int id)
        {
            // Get a specific User.
            return App.DatabaseService.database.Table<User>()
                            .Where(i => i.Id == id)
                            .FirstOrDefaultAsync();
        }

        public static Task<int> SaveUserAsync(User user)
        {
            if (user.Id != 0)
            {
                // Update an existing User.
                return App.DatabaseService.database.UpdateAsync(user);
            }
            else
            {
                // Save a new User.
                return App.DatabaseService.database.InsertAsync(user);
            }
        }

        public static Task<int> DeleteUserAsync(User user)
        {
            // Delete a User.
            return App.DatabaseService.database.DeleteAsync(user);
        }
    }
}
