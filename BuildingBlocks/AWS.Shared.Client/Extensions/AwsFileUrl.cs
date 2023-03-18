using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWS.Shared.Client.Extensions
{
    public static class AwsFileUrl
    {
        public static string GetUrl(string filePath)
        {
            var path = filePath.Split("/");

            return $"https://{path[0]}.s3.ir-thr-at1.arvanstorage.ir/{path[1]}";
        }
    }
}
