using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Shapees.Controllers.DatabaseModelControllers
{
    //added for image load
    public static class LoadImage
    {

        public static byte[] GetPictureData(string imagePath)
        {
            FileStream fs = new FileStream(imagePath, FileMode.Open);
            byte[] byteData = new byte[fs.Length];
            fs.Read(byteData, 0, byteData.Length);
            return byteData;
        }
    }

    //added for image load
    public static class FileStrem
    {
        /// <summary>
        /// get absolute path
        /// </summary>
        /// <param name="relativepath">"TextFile.txt"</param>
        /// <returns></returns>
        public static string GetFilePath(string relativepath)
        {
            return Path.Combine(Directory.GetCurrentDirectory(), relativepath);
        }
    }
}
