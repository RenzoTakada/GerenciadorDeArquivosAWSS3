using GerenciadorDeArquivosBucketS3.Adapters.BucketsS3.Connect;
using GerenciadorDeArquivosBucketS3.Adapters.BucketsS3.Services;
using GerenciadorDeArquivosBucketS3.Application.Interfaces;

namespace GerenciadorDeArquivosBucketS3.Infrastructure
{
    public static class DomainAmazonS3
    {
        public static void AddAmazonS3(this IServiceCollection services, IConfiguration configuration)
        {
            var connect = configuration.GetSection("AWS").Get<AmazonS3Connect>();
            services.AddSingleton<AmazonS3Connect>(connect);

            services.AddScoped<IServiceBucketS3, ServiceBucketS3>();
        }
    }
}
