﻿using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceiroPessoal.Models
{
    public class Transacao
    {
        [BsonId]
        public int Id { get; set; }
        public TipoTransacao Tipo { get; set; }
        public string Nome { get; set; }
        public DateTimeOffset Data { get; set; }
        public decimal Valor { get; set; }
    }
    public enum TipoTransacao
    {
        Receita,
        Despesa
    }
}
