using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace PJM1
{ //Feito por João Gabriel F. Souza, Ricardo Amaro e Diego Toscano.
    internal class Cupom
    {

        public double Total { get; private set; } = 0;
        public int NumeroCupom { get; set; }
        public DateTime DataEmissaoCupom { get; } = DateTime.Now;
        public string CpfCliente { get; set; }
        public List<ItemCupom> ItensCupom { get; private set; } = new List<ItemCupom>();

        public List<ItemCupom> GetItemCupoms()
        {
            return ItensCupom; 
        }
        public void SetCpfCliente(string _cpfCliente)
        {
            this.CpfCliente = _cpfCliente;
        }
        public string GetCpfCliente()
        {
            return CpfCliente;
        }

        public DateTime GetDataEmissaoCupom()
        {
            return DataEmissaoCupom;
        }

        public void SetNumeroCupom(int _numroCupom)
        {
            this.NumeroCupom = _numroCupom;
        }
        public int GetNumeroCupom()
        {
            return NumeroCupom;
        }

        public double CalcularValorTotal()
        {
            double total = 0;
            foreach (var item in ItensCupom)
            {
                total += item.Valor;
            }
            return total;
        }

        public void AdicionarItemCupom(ItemCupom item)
        {
            try
            {
                if (ItensCupom.Count < 20)
                {
                    ItensCupom.Add(item);
                    Total = CalcularValorTotal();
                }
                else
                {
                    Console.WriteLine("Não é possível adicionar mais itens ao cupom. Capacidade máxima atingida.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao adicionar item ao cupom: " + ex.Message);
            }
        }
        public static ItemCupom ObterItemCupom(int numeroCupom, List<Produtos> tabela)
        {
            bool validadorCodigo = false;
            string codProd = "";
            float QuantProd = 0;
            do
            {
                try
                {
                    Console.Write("Insira o Código do Produto: ");
                    codProd = Console.ReadLine();

                    if (!ValidaCodigo(codProd))
                    {
                        throw new ApplicationException("Erro: O código do produto deve ter 13 caracteres numéricos.");
                    }
                    validadorCodigo = true;
                }
                catch (ApplicationException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Erro inesperado: " + e.Message);
                }
            } while (!validadorCodigo);

            do
            {
                try
                {
                    Produtos produto = null;
                    foreach (var p in tabela)
                    {
                        if (p.Codigo.Equals(codProd))
                        {
                            produto = p;
                            break;
                        }
                    }
                    if (produto == null)
                    {
                        throw new ApplicationException("Produto não encontrado.");
                    }

                    Console.Clear();
                    Console.WriteLine("Qual a quantidade a ser inclusa ao cupom:");
                    try
                    {
                        QuantProd = Convert.ToInt32(Console.ReadLine());
                    }
                    catch (FormatException erro)
                    {
                        Console.WriteLine("Erro de formatação: " + erro.Message);
                        return null;
                    }
                    return new ItemCupom(produto, QuantProd);
                }
                catch (ApplicationException e)
                {
                    Console.Clear();
                    Console.WriteLine("Erro: " + e.Message);
                    Console.WriteLine("Pressione qualquer tecla para tentar novamente.");
                    Console.ReadKey();
                    Console.Clear();
                }
                catch (Exception e)
                {
                    Console.Clear();
                    Console.WriteLine("Erro inesperado: " + e.Message);
                    Console.WriteLine("Pressione qualquer tecla para tentar novamente.");
                    Console.ReadKey();
                    Console.Clear();
                }
            } while (true);
        }
        public static bool ValidaCodigo(string code)
        {
            if (code.Length != 13) //EAN 13
            {
                return false;
            }
            try
            {
                Convert.ToInt64(code);
            }
            catch (FormatException)
            {
                return false;
            }
            return true;
        }
        public static void ImprimirCupom(double total, List<ItemCupom> itemCupom, DateTime dataEmissao)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("{0, -81} {1, 0:C}", "IFSP Campus Cubatão S/A", "|");
                Console.WriteLine("{0, -81} {1, 0:C}", "R. Maria Cristina, 50 - Casqueiro", "|");
                Console.WriteLine("{0, -81} {1, 0:C}", "82.630-490 Cubatão - São Paulo", "|");
                Console.WriteLine("{0, -81} {1, 0:C}", "CNPJ: 82.373.077/0001-71", "|");
                Console.WriteLine("{0, -75} {1, 0:C}", "IE: 3512-7800", "UF: SP|");
                Console.WriteLine("{0, -81} {1, 0:C}", "IM: Isento", "|");
                Linha();
                Console.WriteLine("{0, -81} {1}", DateTime.Now, "|");
                Console.Write("{0, -81} {1, 1}", "CCF:000003 COO:000006", "|");
                Console.WriteLine();
                Linha();
                Console.WriteLine("{0, -81} {1, 0:C}", "CUPOM FISCAL", "|");
                Console.WriteLine("{0, -81} {1, 0:C}", "ITEM CÓDIGO DESCRIÇÃO QTD.UN.YL_UNIT( RE) ST VL_", "|");
                Console.WriteLine("{0, -81} {1, 0:C}", "ITEM(R$)", "|");
                foreach (ItemCupom item in itemCupom)
                {
                    if (item != null)
                    {
                        Console.WriteLine("{0:D3} {1} {2, -20} {3, 5} {4, 15:C} {5, 18:C} {6}",
                            DAL.ObterProxNumCupom().ToString("D3"),
                            item.Produto.Codigo,
                            item.Produto.Descricao,
                            item.Quantidade,
                            item.Produto.Preco,
                            item.Valor,
                            "|");
                    }
                }
                Linha();
                Console.WriteLine("{0} {1, 73}", "Dinheiro:", total.ToString("C2") + " |");
                Linha();
                Console.WriteLine("{0, -81} {1, 0:C}", "F4675HJU67788654F8789F45H", "|");
                Console.WriteLine("{0, -81} {1, 0:C}", "Nome: VENDA AO CONSUMIDOR", "|");
                Console.WriteLine("{0, 0} {1, -3} {2, -1} {3, -3} {4, -3} {5, -55} {6}", "Nota CF", "5", "Usu", "43", "Caixa", "C00401-8", "|");
                Linha();
                Console.WriteLine("{0, 0}, {1, -9}, {2, -41} {3}", "IFSP Campus Cubatão S/A", "SP-5000 TH F5", "ECF-IF", "|");
                Console.WriteLine("{0, 0}, {1, -64} {2}", "VERSÃO: 1.0.0.4", "ECF:001/LJ:0001", "|");
                Console.WriteLine("{0, 0}, {1, -61} {2}", "QQQQQQQQQQQQQWQREI", dataEmissao, "|");
                Console.WriteLine("{0, 0}, {1, -67} {2}", "FAB EMULADOR", "BR", "|");
                Console.WriteLine("__________________________________________________________________________________|");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao imprimir o cupom: " + ex.Message);
            }
        }

        static void Linha()
        {
            Console.WriteLine("----------------------------------------------------------------------------------|");
        }

        public void FecharCupom()
        {
            try
            {
                DAL.AtualizarProxNumCupom(NumeroCupom);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao fechar o cupom: " + ex.Message);
            }
        }
    }
    internal class ItemCupom
    {
        public Produtos Produto { get; set; }
        public float Quantidade { get; set; }
        public double Valor { get; set; }

        public ItemCupom() { }

        public ItemCupom(Produtos produto, float quantidade)
        {
            Produto = produto;
            Quantidade = quantidade;
            Valor = CalcularValorItem();
        }
        public double CalcularValorItem()
        {
            try
            {
                if (Produto == null)
                {
                    throw new InvalidOperationException("Impossível calcular o valor de um produto inexistente.");
                }
                return Quantidade * Produto.Preco;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao calcular o valor do item: " + ex.Message);
                return 0;
            }
        }
    }
}