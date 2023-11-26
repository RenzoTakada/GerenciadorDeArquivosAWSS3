namespace GerenciadorDeArquivosBucketS3.Domain.Models.AmazonS3
{
    public class DownloadS3Request
    {
        public string Caminho { get; set; }
        public string NomeDoArquivo { get; set; }
    }
}
