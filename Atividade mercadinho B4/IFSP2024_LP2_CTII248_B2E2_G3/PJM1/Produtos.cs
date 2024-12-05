using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PJM1
{ //Feito por João Gabriel F. Souza, Ricardo Amaro e Diego Toscano.
    internal class Produtos
    {
        public string Descricao;
        public double Preco;
        public string Codigo;
        public string Unidade;
        public int Quantidade;
        public double Valor
        {
            get { return Preco * Quantidade; }
        }


        internal Produtos(string cod, string desc, double pre, int quan, string uni)
        {
            Codigo = cod;
            Descricao = desc;
            Preco = pre;
            Quantidade = quan;
            Unidade = uni;

        }
    }
}
