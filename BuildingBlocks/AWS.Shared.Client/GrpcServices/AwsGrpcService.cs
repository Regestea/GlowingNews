using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AWS.API.Protos;

namespace AWS.Shared.Client.GrpcServices
{
    public class AwsGrpcService
    {
        private AwsService.AwsServiceClient _awsGrpcService;

        public AwsGrpcService(AwsService.AwsServiceClient awsGrpcService)
        {
            _awsGrpcService = awsGrpcService;
        }

        public async Task<GetObjectPathResponse> GetObjectPathAsync(Guid userId, string token)
        {
            var request = new GetObjectPathRequest()
            {
                Token = token,
                UserId = userId.ToString()
            };
            var response = await _awsGrpcService.GetObjectPathByTokenAsync(request);
            return response;
        }

        public async Task DeleteObjectAsync(Guid userId, string filePath)
        {
            var request = new DeleteObjectRequest()
            {
                UserId = userId.ToString(),
                FilePath = filePath
            };
            await _awsGrpcService.DeleteObjectByPathAsync(request);
        }
    }
}
