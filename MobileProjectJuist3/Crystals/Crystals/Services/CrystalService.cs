using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Crystals.Models;

namespace Crystals.Services
{
    class CrystalService
    {
        public static Task<List<Crystal>> GetCrystalsAsync()

        {
            //Get all crystals.
            return App.DatabaseService.database.Table<Crystal>().ToListAsync();
        }

        public static Task<Crystal> GetCrystalAsync(int id)
        {
            // Get a specific crystal.
            return App.DatabaseService.database.Table<Crystal>()
                .Where(i => i.Id == id)
                .FirstOrDefaultAsync();
        }

        public static Task<List<Crystal>> GetCrystalsForUserAsync(int userId)
        {
            // Returns all crystls for a pertical user
            return App.DatabaseService.database.Table<Crystal>().Where(i => i.UserId == userId).ToListAsync();
        }

        

        public static Task<int> SaveCrystalAsync(Crystal crystal)
        {
            if (crystal.Id != 0)
            {
                // Update an existing crystal.
                return App.DatabaseService.database.UpdateAsync(crystal);
            }
            else
            {
                // Save a new crystal.
                return App.DatabaseService.database.InsertAsync(crystal);
            }
        }

        public static Task<int> DeleteCrystalAsync(Crystal crystal)
        {
            // Delete a User.
            return App.DatabaseService.database.DeleteAsync(crystal);
        }
    }
}
