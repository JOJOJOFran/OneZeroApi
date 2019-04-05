using Microsoft.Extensions.DependencyInjection;
using OneZero.DependencyInjections;
using OneZero.Options;
using Qiniu.Http;
using Qiniu.Storage;
using Qiniu.Util;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;

namespace OneZero.Common.Qiniu
{
    [Dependency(ServiceLifetime.Scoped)]
    public class QiniuHelper
    {
        private readonly string AccessKey;
        private readonly string SecretKey;
        private readonly string Bucket;
        private readonly string Domain;
        private readonly int ExpireSeconds;

        public QiniuHelper(OneZeroOption option)
        {
            AccessKey = option.QiniuOption.AccessKey;
            SecretKey = option.QiniuOption.SecretKey;
            Bucket = option.QiniuOption.Bucket;
            Domain = option.QiniuOption.Domain;
            ExpireSeconds = Convert.ToInt32(option.QiniuOption.ExpireSeconds);
        }


        public string CreateUploadToken(string fileKey)
        {
            Mac mac = new Mac(AccessKey, SecretKey);
            PutPolicy putPolicy = new PutPolicy();
            putPolicy.Scope = $"{Bucket}:{fileKey}";
            putPolicy.SetExpires(ExpireSeconds);
            return Auth.CreateUploadToken(mac, putPolicy.ToJsonString());
        }

        public string CreateDownloadToken(string fileKey)
        {
            var mac = SetMac();
            PutPolicy putPolicy = new PutPolicy();
            putPolicy.Scope = $"{Bucket}:{fileKey}";
            putPolicy.SetExpires(3600);
            return Auth.CreateDownloadToken(mac, putPolicy.ToJsonString());
        }

        public string CreatePrivateUrl(string fileKey)
        {
            return DownloadManager.CreatePublishUrl(Domain, fileKey);
        }

        public IEnumerable<string> CreatePrivateUrlList(string filekeys)
        {
            var datas = new List<string>(10);
            var files = filekeys.Split('|').OrderBy(v => v);
            foreach (var item in files)
            {
                string file = null;
                if (item.Contains('^'))
                {
                    file = item.Split('^')[1];
                }
                else
                {
                    file = item;
                }
                datas.Add(CreatePrivateUrl(file));
            }
            return datas;
        }

        public async Task<string> DeleteFile(string fileKey)
        {
            string Msg = "删除成功";
            Config config = new Config();
            config.Zone = Zone.ZoneCnEast;
            var mac = SetMac();
            BucketManager bucketManager = new BucketManager(mac, config);
            string newKey = "UploadFileTest_337756368.dat";
            await bucketManager.Copy(Bucket, "UploadFileTest_337756368.dat", Bucket, newKey);
            HttpResult deleteRet = await bucketManager.Delete(Bucket, newKey);
            if (deleteRet.Code != (int)HttpCode.OK)
            {
                Msg = "删除失败";
            }
            return Msg;
        }


        private Mac SetMac()
        {
            return new Mac(AccessKey, SecretKey);
        }

    }
}
