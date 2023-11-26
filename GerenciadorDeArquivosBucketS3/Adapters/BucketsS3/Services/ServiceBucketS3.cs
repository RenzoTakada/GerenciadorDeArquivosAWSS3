using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using GerenciadorDeArquivosBucketS3.Adapters.BucketsS3.Connect;
using GerenciadorDeArquivosBucketS3.Application.Interfaces;
using GerenciadorDeArquivosBucketS3.Domain.Models.AmazonS3;
using System.IO;
using System.Security.AccessControl;

namespace GerenciadorDeArquivosBucketS3.Adapters.BucketsS3.Services
{
    public class ServiceBucketS3 : IServiceBucketS3
    {
        private readonly IAmazonS3 _awsS3Client;
        private readonly AmazonS3Connect _awsS3Connect;
        public ServiceBucketS3(AmazonS3Connect S3Connect)
        {
            _awsS3Connect = S3Connect;
            var awsKeyId = S3Connect.KeyId;
            var awsSecredkey = S3Connect.Secretkey;
            var awsCredentials = new BasicAWSCredentials(awsKeyId, awsSecredkey);
            var regionBucket = new AmazonS3Config
            {
                RegionEndpoint = Amazon.RegionEndpoint.USEast1
            };
            _awsS3Client = new AmazonS3Client(awsCredentials,regionBucket);
        }

        public async Task<bool> UploadPDFAsync(UploadS3Request amazonS3Request)
        {

            using var FilememoryStream = new MemoryStream();

            amazonS3Request.Arquivo.CopyTo(FilememoryStream);

            var fileTransferS3 = new TransferUtility(_awsS3Client);

            var requestFileS3 = new TransferUtilityUploadRequest
            {
                InputStream = FilememoryStream,
                Key = _awsS3Connect.BucketPrefix + amazonS3Request.Arquivo.FileName,
                BucketName = _awsS3Connect.Bucket,
                ContentType = amazonS3Request.Arquivo.ContentType
            };

            await fileTransferS3.UploadAsync(requestFileS3);

            return true;

        }
        public async Task<bool> DownloadPDFAsync(DownloadS3Request s3Request)
        {

            var fileTransferS3 = new TransferUtility(_awsS3Client);

            var requestFileS3 = new TransferUtilityOpenStreamRequest
            {
                Key = _awsS3Connect.BucketPrefix + s3Request.NomeDoArquivo,
                BucketName = _awsS3Connect.Bucket,

            };

            var ret = await fileTransferS3.OpenStreamAsync(requestFileS3);

            string outputPath = Path.Combine(s3Request.Caminho, s3Request.NomeDoArquivo);
            using (var fileStream = new FileStream(outputPath, FileMode.Create))
            {
                await ret.CopyToAsync(fileStream);
            }

            return true;

        }

    }
}
