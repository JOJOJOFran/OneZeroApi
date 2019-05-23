using System;
using System.Collections.Generic;
using System.Text;

namespace OneZero.Options
{
    public class QiniuOption
    {

        public string AccessKey { get; set; }

        public string SecretKey { get; set; }

        public string Bucket { get; set; }

        public string Domain { get; set; }

        public string ExpireSeconds { get; set; } = "3600";


    }
}
