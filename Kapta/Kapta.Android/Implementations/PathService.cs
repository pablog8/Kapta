[assembly: Xamarin.Forms.Dependency(typeof(Kapta.Droid.Implementations.PathService))]

namespace Kapta.Droid.Implementations
{
    using Kapta.Herramientas.Interfaces;
    using System;
    using System.IO;

    public class PathService : IPathService
    {
        public string GetDatabasePath()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(path, "Kapta.db3");
        }
    }
}