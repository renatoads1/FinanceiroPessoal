using CommunityToolkit.Mvvm.Messaging;
using FinanceiroPessoal.Models;
using FinanceiroPessoal.Repositories;
using System.Text;

namespace FinanceiroPessoal.Views;

public partial class AdicionarTransacao : ContentPage
{
    private readonly ITransacaoRepositorie _transacaoRepository;
    public AdicionarTransacao(ITransacaoRepositorie transacaoRepositorie)
	{

		InitializeComponent();
        _transacaoRepository = transacaoRepositorie;
    }
    public void FechaPage(Object sender, EventArgs e)
    {
        Navigation.PopAsync(true);
    }

    private void Button_ClickedAdd(object sender, EventArgs e)
    {
        if (validaInputs(nome.Text, valor.Text))
        {
            Transacao trans = new Transacao() { 
                Nome = nome.Text,
                Tipo = receita.IsChecked ? TipoTransacao.Receita : TipoTransacao.Despesa,
                Valor = Math.Abs(Convert.ToDecimal(valor.Text)),
                Data = data.Date 
            };

            _transacaoRepository.Add(trans);
            Navigation.PopAsync(true);
            var cont = _transacaoRepository.GetAll().Count;
            App.Current.MainPage.DisplayAlert("Message", $"existem {cont} registros no banco!!", "OK");
            WeakReferenceMessenger.Default.Send(string.Empty);
        }
        else { 
            return;
        }
    }
    
    private bool validaInputs(string nome, string valor)
    {
        StringBuilder sb = new StringBuilder();
        double result;
        bool valid = false;
        if (!string.IsNullOrEmpty(nome))
        {
            valid = true;
        }
        else {
            sb.Append("Preencha o campo Nome corretamente");
        }
        if (!string.IsNullOrEmpty(valor) && double.TryParse(valor, out result))
        {
            valid = true;
        }
        else {
            sb.Append("Preencha o campo Valor corretamente");
        }
        if (valid == false) { 
            lblErro.Text = sb.ToString();
            lblErro.IsVisible = true;
        }

        return valid;
    }

}