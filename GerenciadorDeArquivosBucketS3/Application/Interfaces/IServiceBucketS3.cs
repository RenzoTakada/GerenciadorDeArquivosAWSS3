using GerenciadorDeArquivosBucketS3.Domain.Models.AmazonS3;

namespace GerenciadorDeArquivosBucketS3.Application.Interfaces
{
    public interface IServiceBucketS3
    {
        public Task<bool> UploadPDFAsync(UploadS3Request amazonS3Request);
        public  Task<bool> DownloadPDFAsync(DownloadS3Request s3Request);
    }
}
