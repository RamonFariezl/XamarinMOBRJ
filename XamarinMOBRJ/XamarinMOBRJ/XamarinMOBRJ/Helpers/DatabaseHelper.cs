using Prism.Services;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XamarinMOBRJ.Models;

namespace XamarinMOBRJ.Helpers
{
    public class DatabaseHelper 
    {
        private SQLiteAsyncConnection _sqliteconnection;
        

        public DatabaseHelper(SQLiteAsyncConnection sqliteConnection)
        {
            _sqliteconnection = sqliteConnection;       
            _sqliteconnection.CreateTableAsync<Estados>();
        }

        // Insert new Contact to DB   
        public async Task<int>  InsertEstado(Estados listaEstados)
        {
            return await _sqliteconnection.InsertAsync(listaEstados);
            
        }

        public async Task<List<Estados>> GetEstadosAsync()
        {
            return await _sqliteconnection.Table<Estados>().ToListAsync();
        }


    }
}
