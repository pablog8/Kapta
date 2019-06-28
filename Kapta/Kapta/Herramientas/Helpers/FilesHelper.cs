namespace Kapta.Herramientas.Helpers
{
    using System.IO;

    public class FilesHelper
    {
        //para mandarlo al postman hay que enviarlo con bytes
        public static byte[] ReadFully(Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }
}
