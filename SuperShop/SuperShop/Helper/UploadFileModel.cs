using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Helper
{
    public class UploadFileModel
    {
        public long Id { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public long FileSize { get; set; }
        public string FileLocation { get; set; }
        public byte[] FileByte { get; set; }
    }
}
