using CommunityToolkit.Mvvm.Messaging;
using FinanceiroPessoal.Models;
using FinanceiroPessoal.Repositories;
using System.Text;

namespace FinanceiroPessoal.Views;

public partial class EditarTransacao : ContentPage
{
	private Transacao _transacao;
	private readonly ITransacaoRepositorie _transacaoRepositorie;
	public EditarTransacao(ITransacaoRepositorie transacaoRepositorie)
	{
		InitializeComponent();
		_transacaoRepositorie = transacaoRepositorie;
	}
	public void FechaPage(Object sender, EventArgs e) 
	{
		Navigation.PopAsync(true);
	}

	public void recebeEdicao(Transacao trans) 
	{
		_transacao = trans;

		if (_transacao.Tipo == TipoTransacao.Receita)
		{
			RadioReceita.IsChecked = true;
		}
		else { 
			RadioDespesa.IsChecked = true;
        }

        EntryId.Text = _transacao.Id.ToString();
        EntryNome.Text = _transacao.Nome;
        EntryValor.Text = _transacao.Valor.ToString();
        Entrydata.Date = _transacao.Data.DateTime;

    }

    private void Button_ClickedEditar(object sender, EventArgs e)
    {


        if (validaInputs(EntryNome.Text, EntryValor.Text)) {
            
            Transacao trans = new Transacao()
            {
                Id = Convert.ToInt32(EntryId.Text),
                Nome = EntryNome.Text,
                Tipo = RadioReceita.IsChecked ? TipoTransacao.Receita : TipoTransacao.Despesa,
                Valor = Math.Abs(Convert.ToDecimal(EntryValor.Text)),
                Data = Entrydata.Date
            };
            _transacaoRepositorie.Update(trans);
            Navigation.PopAsync(true);
            var cont = _transacaoRepositorie.GetAll().Count;
            WeakReferenceMessenger.Default.Send(string.Empty);
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
        else
        {
            sb.Append("Preencha o campo Nome corretamente");
        }
        if (!string.IsNullOrEmpty(valor) && double.TryParse(valor, out result))
        {
            valid = true;
        }
        else
        {
            sb.Append("Preencha o campo Valor corretamente");
        }
        if (valid == false)
        {
            lblErro.Text = sb.ToString();
            lblErro.IsVisible = true;
        }

        return valid;
    }

}