using AWS.API.Protos;
using AWS.Infrastructure.Persistence.AWS;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;

namespace AWS.API.GrpcServices
{
    public class AwsGrpcService : AwsService.AwsServiceBase
    {
        private AwsIndexDbContext _awsIndexDbContext;

        public AwsGrpcService(AwsIndexDbContext awsIndexDbContext)
        {
            _awsIndexDbContext = awsIndexDbContext;
        }


        public override async Task<GetObjectPathResponse> GetObjectPathByToken(GetObjectPathRequest request, ServerCallContext context)
        {
            Guid userId = Guid.Parse(request.UserId);
            var awsFile = await _awsIndexDbContext.AwsFiles.FirstOrDefaultAsync(x => x.UserId == userId && x.HaveUse == false && x.Token == request.Token);
            if (awsFile != null)
            {
                awsFile.HaveUse = true;
                awsFile.Token = "";
                _awsIndexDbContext.AwsFiles.Update(awsFile);
                await _awsIndexDbContext.SaveChangesAsync();
                return new GetObjectPathResponse() { FilePath = awsFile.FullPath,FileFormat = awsFile.FileFormat};
            }

            return new GetObjectPathResponse() { FilePath = "",FileFormat = ""};
        }

        public override async Task<DeleteObjectResponse> DeleteObjectByPath(DeleteObjectRequest request, ServerCallContext context)
        {
            Guid userId = Guid.Parse(request.UserId);
            var path = request.FilePath.Split("/");
            var awsFile = await _awsIndexDbContext.AwsFiles.FirstOrDefaultAsync(x => x.FileName == path[1] && x.UserId == userId);
            if (awsFile != null)
            {
                awsFile.HaveUse = false;
                _awsIndexDbContext.AwsFiles.Update(awsFile);
                await _awsIndexDbContext.SaveChangesAsync();
            }

            return new DeleteObjectResponse();
        }
    }
}
