[assembly: Xamarin.Forms.Dependency(typeof(Kapta.iOS.Implementations.PathService))]

namespace Kapta.iOS.Implementations
{
    using Kapta.Herramientas.Interfaces;
    using System;
    using System.IO;

    public class PathService : IPathService
    {
        public string GetDatabasePath()
        {
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }

            return Path.Combine(libFolder, "Kapta.db3");
        }
    }
}