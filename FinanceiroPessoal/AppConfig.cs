using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceiroPessoal
{
    public class AppConfig
    {
        private static string dbNome = "database.db";
        private static string dbDiretorio = FileSystem.Current.AppDataDirectory;
        public static string dbPath = Path.Combine(dbDiretorio, dbNome);
    }
}
