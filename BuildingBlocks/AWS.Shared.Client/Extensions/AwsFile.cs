using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AWS.Shared.Client.Enums;

namespace AWS.Shared.Client.Extensions
{
    public static class AwsFile
    {
        public static string GetUrl(string filePath)
        {
            var path = filePath.Split("/");

            return $"https://{path[0]}.s3.ir-thr-at1.arvanstorage.ir/{path[1]}";
        }

        public static MediaType GetMediaType(string fileFormat)
        {
            MediaType mediaType;
            switch (fileFormat)
            {
                case null:
                case "":
                    mediaType = MediaType.None;
                    break;
                case "jpg":
                case "jpeg":
                case "png":
                    mediaType = MediaType.Image;
                    break;
                case "gif":
                    mediaType = MediaType.Gif;
                    break;
                case "mp4":
                case "mkv":
                    mediaType = MediaType.Video;
                    break;
                default:
                    mediaType = MediaType.None;
                    break;
            }

            return mediaType;
        }

    }
}

