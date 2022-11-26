using CodecellShare.Constant;
using CodecellShare.Interfaces;
using CodecellShare.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CodeCellZarinpalV6
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCodecellZarinpalGetway(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddScoped<IZarinpalService, ZarinpalService>();

            services.AddHttpClient(ClientName.CodecellZarinpalClientName, client =>
            {
                client.BaseAddress = new Uri("https://api.zarinpal.com/pg/v4/payment/");
            });

            return services;
        }
    }
}
