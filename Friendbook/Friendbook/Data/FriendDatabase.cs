using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using Friendbook.Models;

namespace Friendbook.Data
{
    public class FriendDatabase
    {
        readonly SQLiteAsyncConnection database;

        public FriendDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Friend>().Wait();
        }

        public Task<List<Friend>> GetFriendsAsync()
        {
            //Get all friends.
            return database.Table<Friend>().ToListAsync();
        }

        public Task<Friend> GetFriendAsync(int id)
        {
            // Get a specific friend.
            return database.Table<Friend>()
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveFriendAsync(Friend friend)
        {
            if (friend.ID != 0)
            {
                // Update an existing friend.
                return database.UpdateAsync(friend);
            }
            else
            {
                // Save a new friend.
                return database.InsertAsync(friend);
            }
        }

        public Task<int> DeleteFriendAsync(Friend friend)
        {
            // Delete a friend.
            return database.DeleteAsync(friend);
        }
    }
}