namespace GerenciadorDeArquivosBucketS3.Domain.Models.AmazonS3
{
    public struct UploadS3Request
    {
        public string ChaveIdentificador { get; set; }
        public IFormFile Arquivo { get; set; }

    }
}
