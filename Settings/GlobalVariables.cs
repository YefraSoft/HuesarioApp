

namespace HuesarioApp.Settings
{
    public static class GlobalVariables
    {
        public static string SalesAlbum => "Ventas";
        
        public static string DbPath => Path.Combine(FileSystem.AppDataDirectory, "mgs.db3");
    }
}
