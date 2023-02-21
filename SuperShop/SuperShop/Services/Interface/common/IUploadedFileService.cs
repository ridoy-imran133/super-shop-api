using SuperShop.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Services.Interface.common
{
    public interface IUploadedFileService
    {
        public UploadFileModel SaveUploadedFile(UploadFileModel uploadFile, string pFileLocationKey);
        public List<UploadFileModel> SaveUploadedFiles(List<UploadFileModel> uploadFiles, string pFileLocationKey);
        public void DeleteUploadedFiles(List<UploadFileModel> uploadFiles);
        public UploadFileModel SaveUploadedFileBackOffice(UploadFileModel uploadFile, string pFileLocationKey, string filePrefix);
    }
}
