using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoFinancas
{
    class Conta
    {
        public string Nome { get; set; }
        public decimal SaldoConta { get; set; }

        public Conta(string nome)
        {
            Nome = nome;
            SaldoConta = 0;
        }
    }
}
