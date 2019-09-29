using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

namespace Squad
{
    public class SquadItemDatabase
    {
        readonly SQLiteAsyncConnection database;

        public SquadItemDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<SquadItem>().Wait();
            database.CreateTableAsync<Friend>().Wait();

        }

        public Task<List<Friend>> GetFriendsAsync()
        {
            return database.Table<Friend>().ToListAsync();
        }

        public Task<int> SaveFriendAsync(Friend item)
        {
            if (item.friendName != null && item.friendName != "")
            {
                return database.InsertAsync(item);
            }
            else return null;
        }

        public Task<int> DeleteFriendAsync(Friend item)
        {
            return database.DeleteAsync(item);
        }

        public Task<int> DeleteItemAsync(Friend item)
        {
            return database.DeleteAsync(item);
        }

        public Task<List<SquadItem>> GetItemsAsync()
        {
            return database.Table<SquadItem>().ToListAsync();
        }

        public Task<List<SquadItem>> GetItemsNotDoneAsync()
        {
            return database.QueryAsync<SquadItem>("SELECT * FROM [SquadItem] WHERE [Done] = 0");
        }
        public Task<List<SquadItem>> GetSquads()
        {
            return database.QueryAsync<SquadItem>("SELECT * FROM [SquadItem]");
        }

        public Task<SquadItem> GetItemAsync(int id)
        {
            return database.Table<SquadItem>().Where(i => i.SquadId == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(SquadItem item)
        {
            if (item.SquadId != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(SquadItem item)
        {
            return database.DeleteAsync(item);
        }
    }
}

