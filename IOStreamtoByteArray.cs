using System.IO;

namespace ConsoleApplication1
{
    public static class IOStreamtoByteArray
    {
        /// <summary>
        /// Helper function to convert System.IO.Stream data to a byte[] array
        /// </summary>
        /// <param name="input">Any System.IO.Stream input</param>
        /// <returns></returns>
        public static byte[] ReadFully(Stream input)
        {
            var buffer = new byte[16 * 1024];
            using (var ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }
}
