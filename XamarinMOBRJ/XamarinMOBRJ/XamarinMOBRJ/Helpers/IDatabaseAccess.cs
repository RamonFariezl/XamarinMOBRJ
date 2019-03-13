using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XamarinMOBRJ.Models;

namespace XamarinMOBRJ.Helpers
{
    public interface IDatabaseAccess
    {
        SQLiteAsyncConnection GetConnection();
        
    }
}
