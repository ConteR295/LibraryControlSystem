namespace LibraryControlSystem_Infrastructure;

//* Строки подключения к БД
public static class ConnectionStrings
{
    public static string ConnectionStringSqlite //строка подключения для Sqlite
    {
        get
        {
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "library.db");
            return dbPath; 
        }
    }
}
