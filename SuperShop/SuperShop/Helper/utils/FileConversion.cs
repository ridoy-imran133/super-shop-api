using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Helper.utils
{
    public class FileConversion
    {
        public static UploadFileModel SaveUploadedFiles(UploadFileModel item, string folderPath)
        {

            string FilePath = Directory.GetCurrentDirectory().ToString();

            FilePath += folderPath;

            System.IO.Directory.CreateDirectory(FilePath);

            byte[] byteArray = item.FileByte;
            int pos = item.FileName.LastIndexOf(".") + 1;
            string fileExt = item.FileName.Substring(pos, item.FileName.Length - pos);

            string newFileName = Guid.NewGuid().ToString() + "." + fileExt;

            string FileFullPath = @FilePath + "/" + newFileName;

            System.IO.File.WriteAllBytes(@FileFullPath, byteArray);

            item.FileLocation = FileFullPath;
            item.FileName = newFileName;


            return item;
        }
    }
}