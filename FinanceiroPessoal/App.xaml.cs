using FinanceiroPessoal.Views;
using System.Globalization;

namespace FinanceiroPessoal
{
    public partial class App : Application
    {
        public App(ListaTransacao listar)
        {
            InitializeComponent();
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("pt-BR");
            CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("pt-BR");
            MainPage = new NavigationPage(listar);
        }
    }
}
