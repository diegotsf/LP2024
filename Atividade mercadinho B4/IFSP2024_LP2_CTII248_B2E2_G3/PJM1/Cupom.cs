using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PJM1
{ //Feito por João Gabriel F. Souza, Ricardo Amaro e Diego Toscano.
    internal class Cupom
    {
        public double Total { get; private set; } = 0;
        private int _numeroCupom;
        private DateTime _dataEmissaoCupom;
        private string _cpfCliente;
        private List<ItemCupom> _itemCupom = new List<ItemCupom>();
        int i = 0;
        public List<ItemCupom> GetItemCupom()
        {
            return this._itemCupom;
        }


        public void setNumeroCupom(int e)
        {
            this._numeroCupom = e;
        }
        public DateTime GetDataEmissao()
        {

            return DateTime.Now;
        }

        public void setCpfCliente(string e)
        {
            this._cpfCliente = e;
        }

        public void setItemCupom(ItemCupom item)
        {
            if (_itemCupom.Count < 20)
            {
                this._itemCupom.Add(item);
                Total += item.Valor;
            }
            else
            {
                Console.WriteLine("Não é possível adicionar mais itens ao cupom. Capacidade máxima atingida.");
            }
        }

    }
    internal class ItemCupom
    {
        public Produtos Produto { get; set; }
        public float Quantidade { get; set; }
        public double Valor { get; set; }


        public ItemCupom()
        {
        }
        public ItemCupom(Produtos produto, float quantidade)
        {
            Produto = produto;
            Quantidade = quantidade;
            Valor = produto.Preco * quantidade;
        }
    }
}

