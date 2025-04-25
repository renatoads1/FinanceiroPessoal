using FinanceiroPessoal.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceiroPessoal.Auxiliar
{
    class ConverteValorTransacao : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            
            Transacao transacao = (Transacao)value;
            if (transacao == null) {
                return"";
            }
            if (transacao.Tipo == TipoTransacao.Receita)
            {
                return transacao.Valor.ToString("C", culture);
            }
            else {
                return $"{transacao.Valor.ToString("C", culture)}";
            }
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
