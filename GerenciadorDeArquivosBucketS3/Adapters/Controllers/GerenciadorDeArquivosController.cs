using Amazon.S3;
using GerenciadorDeArquivosBucketS3.Application.Interfaces;
using GerenciadorDeArquivosBucketS3.Domain.Models.AmazonS3;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorDeArquivosBucketS3.Adapters.Controllers
{
    [ApiController]
    [Route("GerenciadorArquvios/v1")]
    public class GerenciadorDeArquivosController : ControllerBase
    {
        private readonly IServiceBucketS3 _awsServiceBucketS3;
        public GerenciadorDeArquivosController(IServiceBucketS3 serviceBucketS3)
        {

            _awsServiceBucketS3 = serviceBucketS3;

        }
        [HttpGet]
        [Route("Donwload")]
        public async Task<IResult> DownloadArquivos([FromQuery] string CaminhoPasta, [FromQuery]string NomeArquivo)
        {
            var request = new DownloadS3Request
            {
                Caminho = CaminhoPasta,
                NomeDoArquivo = NomeArquivo
            };
            var file2 = await _awsServiceBucketS3.DownloadPDFAsync(request);
            return Results.Ok(file2);
        }

        [HttpPost]
        [Route("Upload")]
        public async Task<IResult> UploadArquivos(IFormFile file)
        {
            var ret = await _awsServiceBucketS3.UploadPDFAsync(new Domain.Models.AmazonS3.UploadS3Request
            {
                Arquivo = file,
                ChaveIdentificador = Guid.NewGuid().ToString(),
            });


            return Results.Ok(ret);
        }
    }
}
