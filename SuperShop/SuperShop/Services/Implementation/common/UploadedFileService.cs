using SuperShop.Helper;
using SuperShop.Services.Interface.common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Services.Implementation.common
{
    public class UploadedFileService : IUploadedFileService
    {
        //private readonly IAppBasicConfigurationRepository _configurationsRepository;
        //public UploadedFileService(IAppBasicConfigurationRepository configurationsRepository)
        //{
        //    _configurationsRepository = configurationsRepository;
        //}

        public UploadedFileService()
        {

        }
        public UploadFileModel SaveUploadedFile(UploadFileModel uploadFile, string pFileLocationKey)
        {
            //string vFilePath = _configurationsRepository.GetConfigurationByKey(pFileLocationKey).ConfigurationValue;

            //vFilePath = Directory.GetCurrentDirectory() + vFilePath;            

            string vFilePath = "/app/shared";

            if (!Directory.Exists(vFilePath))
            {
                Directory.CreateDirectory(vFilePath);
            }

            byte[] byteArray = uploadFile.FileByte;
            int pos = uploadFile.FileName.LastIndexOf(".") + 1;
            string fileExt = uploadFile.FileName.Substring(pos, uploadFile.FileName.Length - pos);
            string FileFullPath = @vFilePath + @"/" + Guid.NewGuid().ToString() + "." + fileExt;
            File.WriteAllBytes(FileFullPath, byteArray);
            uploadFile.FileLocation = FileFullPath;
            uploadFile.FileType = fileExt;
            return uploadFile;
        }

        public List<UploadFileModel> SaveUploadedFiles(List<UploadFileModel> uploadFiles, string pFileLocationKey)
        {
            string filelocation = "/file/shared";
            string vFilePath = Directory.GetCurrentDirectory() + filelocation;

            if (!Directory.Exists(vFilePath))
            {
                Directory.CreateDirectory(vFilePath);
            }

            //string vFilePath = "/app/shared";

            foreach (var item in uploadFiles)
            {
                byte[] byteArray = item.FileByte;
                int pos = item.FileName.LastIndexOf(".") + 1;
                string fileExt = item.FileName.Substring(pos, item.FileName.Length - pos);
                string FileFullPath = @filelocation + @"/" + Guid.NewGuid().ToString() + "." + fileExt;
                File.WriteAllBytes(Directory.GetCurrentDirectory() + FileFullPath, byteArray);
                item.FileLocation = FileFullPath;
                item.FileType = fileExt;
            }
            return uploadFiles;
        }
        public void DeleteUploadedFiles(List<UploadFileModel> uploadFiles)
        {
            foreach (var item in uploadFiles)
            {
                if ((File.Exists(item.FileLocation)))
                {
                    File.Delete(item.FileLocation);
                }
            }
        }


        public UploadFileModel SaveUploadedFileBackOffice(UploadFileModel uploadFile, string pFileLocationKey, string filePrefix)
        {


            string vFilePath = "/app/shared/backoffice";

            //    vFilePath = Directory.GetCurrentDirectory() + vFilePath;

            if (!Directory.Exists(vFilePath))
            {
                Directory.CreateDirectory(vFilePath);
            }

            byte[] byteArray = uploadFile.FileByte;
            int pos = uploadFile.FileName.LastIndexOf(".") + 1;
            string fileExt = uploadFile.FileName.Substring(pos, uploadFile.FileName.Length - pos);

            string vFileName = filePrefix + "_" + Guid.NewGuid().ToString() + "." + fileExt;


            string FileFullPath = @vFilePath + @"/" + vFileName;
            File.WriteAllBytes(FileFullPath, byteArray);
            uploadFile.FileLocation = FileFullPath;
            uploadFile.FileName = FileFullPath;
            uploadFile.FileType = fileExt;
            return uploadFile;
        }
    }
}
