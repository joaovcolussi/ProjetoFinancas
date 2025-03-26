using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoFinancas
{
    class Transacao
    {
        public Conta Conta { get; set; }
        public string Descricao {  get; set; }
        public decimal Valor { get; set; }
        public DateTime Data {  get; set; }
        public TipoTransacao Tipo {  get; set; }

        public Transacao(Conta conta, string descricao, decimal valor, DateTime data, TipoTransacao tipo)
        {
            Conta = conta;
            Descricao = descricao;
            Valor = valor;
            Data = data;
            Tipo = tipo;
        }
    }
}
