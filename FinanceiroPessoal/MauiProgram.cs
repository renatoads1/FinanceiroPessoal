using CommunityToolkit.Mvvm.Messaging;
using FinanceiroPessoal.Repositories;
using FinanceiroPessoal.Views;
using LiteDB;
using Microsoft.Extensions.Logging;

namespace FinanceiroPessoal
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                .RegistraDatabasesRepositorios()
                .RegistraViews();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }

        public static MauiAppBuilder RegistraDatabasesRepositorios(this MauiAppBuilder appBuilder)
        {
            appBuilder.Services.AddSingleton<LiteDatabase>(
                opt => {
                  return  new LiteDatabase($"Filename={AppConfig.dbPath};Connection=Shared;");
                }
            );
            appBuilder.Services.AddTransient<ITransacaoRepositorie, TransacaoRepositorie>();
            appBuilder.Services.AddSingleton<IMessenger, WeakReferenceMessenger>();
            return appBuilder;

                
        }

        public static MauiAppBuilder RegistraViews(this MauiAppBuilder appBuilder) {
            appBuilder.Services.AddTransient<AdicionarTransacao>();
            appBuilder.Services.AddTransient<EditarTransacao>();
            appBuilder.Services.AddTransient<ListaTransacao>();
            return appBuilder;
        }

    }
}
