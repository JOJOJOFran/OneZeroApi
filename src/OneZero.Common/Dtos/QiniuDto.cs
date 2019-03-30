using System;
using System.Collections.Generic;
using System.Text;

namespace OneZero.Common.Dtos
{
    public class QiniuDto
    {
        public string UploadToken { get; set; }

        public IEnumerable<string> DownloadUrl { get; set; }
    }
}
