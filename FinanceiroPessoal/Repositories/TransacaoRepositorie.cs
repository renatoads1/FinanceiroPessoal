using FinanceiroPessoal.Models;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceiroPessoal.Repositories
{
    public class TransacaoRepositorie : ITransacaoRepositorie
    {
        private readonly LiteDatabase _liteDatabase;
        public TransacaoRepositorie(LiteDatabase liteDatabase)
        {
            _liteDatabase = liteDatabase;
        }

        public List<Transacao> GetAll()
        {
            return _liteDatabase.GetCollection<Transacao>("transacoes").Query().OrderByDescending(a=>a.Data).ToList();
        }
        public void Add(Transacao transacao)
        {
            var col = _liteDatabase.GetCollection<Transacao>("transacoes");
            col.Insert(transacao);
            col.EnsureIndex(x => x.Data);
        }

        public void Update(Transacao transacao)
        {
            try
            {
                var col = _liteDatabase.GetCollection<Transacao>("transacoes");
                col.Update(transacao);
            }
            catch (Exception ex) { 
                var x = ex.Message;
            }
        }
        public Transacao Get(int id)
        {
            var col = _liteDatabase.GetCollection<Transacao>("transacoes")
                .Query().Where(a=>a.Id == id);
            return col.FirstOrDefault();
        }

        public void Delete(Transacao transacao)
        {
            var col = _liteDatabase.GetCollection<Transacao>("transacoes");
            col.Delete(transacao.Id);
        }

    }
}
