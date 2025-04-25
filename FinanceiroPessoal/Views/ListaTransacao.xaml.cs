using CommunityToolkit.Mvvm.Messaging;
using FinanceiroPessoal.Models;
using FinanceiroPessoal.Repositories;
using Microsoft.Extensions.Configuration;
using static FinanceiroPessoal.Views.AdicionarTransacao;

namespace FinanceiroPessoal.Views;

public partial class ListaTransacao : ContentPage
{
    private readonly AdicionarTransacao _adicionarTransacao;
    private readonly EditarTransacao _editarTransacao;
    private readonly ITransacaoRepositorie _transacaoRepositorie;
    private Transacao _transacao;
    public ListaTransacao(AdicionarTransacao add, EditarTransacao editar,ITransacaoRepositorie transacaoRepositorie)
	{
        _adicionarTransacao = add;
        _editarTransacao = editar;
        _transacaoRepositorie = transacaoRepositorie;
        InitializeComponent();
        ListTrans.ItemsSource = _transacaoRepositorie.GetAll();
        MessagingCenter.Subscribe<AdicionarTransacao>(this, "DadosAtualizados", (sender) =>
        {
            ListTrans.ItemsSource = _transacaoRepositorie.GetAll();
        });
    }

    public void SetTransectionToEdit(Transacao trans) {
        _transacao = trans;
    }

    public void GoToAdd(object sender, EventArgs e)
    {
        Navigation.PushAsync(_adicionarTransacao);
    }
    public void GoToEdit(object sender, EventArgs e) 
    {
        Navigation.PushAsync(_editarTransacao);
    }
    protected override void OnAppearing()
    {
        AtualizaPg();
        base.OnAppearing();
        ListTrans.ItemsSource = _transacaoRepositorie.GetAll();

        WeakReferenceMessenger.Default.Register<string>(this, (r, m) =>
        {
            AtualizaPg();
        });
        
    }

    private void AtualizaPg() {
        var itens = _transacaoRepositorie.GetAll();
        ListTrans.ItemsSource = itens;
        var receita = itens.Where(a => a.Tipo == Models.TipoTransacao.Receita).Sum(a => a.Valor);
        var despesa = itens.Where(a => a.Tipo == Models.TipoTransacao.Despesa).Sum(a => a.Valor);
        var saldo = (receita - despesa);
        _receita.Text = receita.ToString("C");
        _despesa.Text = despesa.ToString("C");
        _saldo.Text = saldo.ToString("C");
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        WeakReferenceMessenger.Default.Unregister<string>(this);
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        var grid = (Grid)sender;
        var gesture = (TapGestureRecognizer)grid.GestureRecognizers[0];
        var transacao = (Transacao)gesture.CommandParameter;
        _editarTransacao.recebeEdicao(transacao); // Use the instance of EditarTransacao  
        Navigation.PushAsync(_editarTransacao);
    }

    private async void Excluir(object sender, TappedEventArgs e)
    {
        var result = await App.Current.MainPage.DisplayAlert("Excluir", "tem certeza que deseja Excluir?", "Sim","Não");
        if (result) {
            //excluir aqui 
            Transacao trans = (Transacao)e.Parameter;
            _transacaoRepositorie.Delete(trans);
            var itens = _transacaoRepositorie.GetAll();
            ListTrans.ItemsSource = itens;
        }
    }
}