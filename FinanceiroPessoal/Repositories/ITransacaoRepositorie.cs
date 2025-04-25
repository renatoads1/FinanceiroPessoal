using FinanceiroPessoal.Models;

namespace FinanceiroPessoal.Repositories
{
    public interface ITransacaoRepositorie
    {
        void Add(Transacao transacao);
        void Delete(Transacao transacao);
        Transacao Get(int id);
        List<Transacao> GetAll();
        void Update(Transacao transacao);
    }
}