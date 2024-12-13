namespace PJM1
{
    using System.IO;
    using System.Threading.Tasks.Dataflow;
    using System.Globalization;

    internal class Program
    { //Feito por João Gabriel F. Souza, Ricardo Amaro e Diego Toscano.

        static void Linha2()

        {
            Console.WriteLine("----------------------------------------------------------------------------------|");
        }
        static void Linha()

        {
            Console.WriteLine("------------------------------------------------------------------------------------|");
        }
        static void CupomFormatado(double total, List<ItemCupom> itemCupom, DateTime dataEmissao)
        {
            int numero = 001;
            Console.Clear();
            Console.WriteLine("{0, -81} {1, 0:C}", "IFSP Campus Cubatão S/A", "|");
            Console.WriteLine("{0, -81} {1, 0:C}", "R. Maria Cristina, 50 - Casqueiro", "|");
            Console.WriteLine("{0, -81} {1, 0:C}", "82.630-490 Cubatão - São Paulo", "|");
            Console.WriteLine("{0, -81} {1, 0:C}", "CNPJ: 82.373.077/0001-71", "|");
            Console.WriteLine("{0, -75} {1, 0:C}", "IE: 3512-7800", "UF: SP|");
            Console.WriteLine("{0, -81} {1, 0:C}", "IM: Isento", "|");
            Linha2();
            Console.WriteLine("{0, -81} {1}", DateTime.Now, "|");
            Console.Write("{0, -81} {1, 1}", "CCF:000003 COO:000006", "|");
            Console.WriteLine();
            Linha2();
            Console.WriteLine("{0, -81} {1, 0:C}", "CUPOM FISCAL", "|");
            Console.WriteLine("{0, -81} {1, 0:C}", "ITEM CÓDIGO DESCRICÃO QTD.UN.YL_UNIT( RE) ST VL_", "|");
            Console.WriteLine("{0, -81} {1, 0:C}", "ITEM(R$)", "|");
            foreach (ItemCupom item in itemCupom)
            {
                if (item != null)
                {
                    Console.WriteLine("{0:D3}  {1}  {2, -20} {3, 5} {4, 15:C} {5, 18:C} {6}",
                        numero.ToString("D3"),
                        item.Produto.Codigo,
                        item.Produto.Descricao,
                        item.Quantidade,
                        item.Produto.Preco,
                        item.Valor,
                        "|");
                }
                numero++;
            }
            Linha2();
            Console.WriteLine("{0" +
            "} {1, 73}", "Dinheiro:", total.ToString("C2") + " |");
            Linha2();
            Console.WriteLine("{0, -81} {1, 0:C}", "F4675HJU67788654F8789F45H", "|");
            Console.WriteLine("{0, -81} {1, 0:C}", "Nome: VENDA AO CONSUMIDOR", "|");
            Console.WriteLine("{0, 0} {1, -3} {2, -1} {3, -3} {4, -3} {5, -55} {6}", "Nota CF", "5", "Usu", "43", "Caixa", "C00401-8", "|");
            Linha2();
            Console.WriteLine("{0, 0}, {1, -9}, {2, -41} {3}", "IFSP Campus Cubatão S/A", "SP-5000 TH F5", "ECF-IF", "|");
            Console.WriteLine("{0, 0}, {1, -64} {2}", "VERSÃO: 1.0.0.4", "ECF:001/LJ:0001", "|");
            Console.WriteLine("{0, 0}, {1, -61} {2}", "QQQQQQQQQQQQQWQREI", dataEmissao, "|");
            Console.WriteLine("{0, 0}, {1, -67} {2}", "FAB EMULADOR", "BR", "|");
            Console.WriteLine("__________________________________________________________________________________|");
        }


        static void Main(string[] args)
        {
            DAL dal = new DAL();
           
            string codProd = "";
            Cupom cupom = new Cupom();
            int QuantProd;
            float total = 0.0f;
            string CpfCliente;
            var dataemissao = DateTime.Now;
            List<Produtos> tabela = DAL.ObterTabelaProdutos();
            static void TabelaPrincipal(List<Produtos> listaprodutos)
            {
                Console.Clear();
                Linha();
                Console.WriteLine("|    Código    |     Descrição     |   Preço   |  Quant.Estoque   | Valor em Estoque|");
                Linha();

                foreach (Produtos produto in listaprodutos)
                {
                    string AlertaProduto = produto.Valor;

                    if (AlertaProduto == "?????")
                    {
                        Console.ForegroundColor = ConsoleColor.Red; //Sinalização gráfica de que há um erro com relação à revisão do estoque
                        Console.WriteLine("{0, -15} {1,-20} {2, -13:C} {3, 0} {4, -14} {5, 15} {6}",
                            produto.Codigo,
                            produto.Descricao,
                            produto.Preco,
                            produto.Quantidade,
                            produto.Unidade,
                            AlertaProduto,
                            "|");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine("{0, -15} {1,-20} {2, -13:C} {3, 4} {4, -14} {5, -12:C} {6}",
                            produto.Codigo,
                            produto.Descricao,
                            produto.Preco,
                            produto.Quantidade,
                            produto.Unidade,
                            AlertaProduto,
                            "|");
                    }
                }
                Linha();
            }
            TabelaPrincipal(tabela);

            bool validadorcpf = false;

            static bool ValidaCPF(string cpf)
            {
                if (cpf.Length != 11)
                {
                    return false; 
                }
                try
                {
                    Convert.ToInt64(cpf);
                }
                catch (FormatException)
                {
                    return false; 
                }

                return true;
            }
            do
            {
                try
                {
                    Console.Write("Insira o seu CPF (apenas números): ");
                    CpfCliente = Console.ReadLine();

                    if (ValidaCPF(CpfCliente) == false) 
                    {
                        throw new ApplicationException("Erro: O CPF deve conter 11 dígitos numéricos.");
                    }
                    validadorcpf = true;
                    cupom.SetCpfCliente(CpfCliente);
                }
                catch (ApplicationException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro inesperado: " + ex.Message);
                }
            } while (!validadorcpf);

            bool validadorCupom = false;
            do
            {
                try
                {
                    //Console.Write("Insira o número do cupom: ");
                    //NumeroCupom = Convert.ToInt32(Console.ReadLine());
                    validadorCupom = true;
            
                    cupom.SetNumeroCupom(DAL.ObterProxNumCupom());
                    DAL.AtualizarProxNumCupom(DAL.ObterProxNumCupom());
                    


                }
                catch (FormatException)
                {
                    Console.WriteLine("Erro: Formato inválido para o número do cupom. Insira um número inteiro.");
                }
            } while (!validadorCupom);

            Console.Clear();
            cupom.GetDataEmissaoCupom();
            int i = 0;
            ConsoleKeyInfo keyInfo = new ConsoleKeyInfo();
            bool validadorCodigo = false;
            static bool ValidaCodigo(string code)
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
            do
            {
                try
                {
                    Console.Clear();
                    TabelaPrincipal(tabela);
                    Console.Write("Insira o Código do Produto: ");
                    codProd = Console.ReadLine();

                    if (ValidaCodigo(codProd) == false)
                    {
                        throw new ApplicationException("Erro: O cóodigo do produto deve ter 13 caractéres numéricos.");
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

            do { 
            try
                {
                    bool produtoEncontrado = false;

                    foreach (Produtos produto in tabela)
                    {
                        if (codProd == produto.Codigo)
                        {
                            Console.Clear();
                            TabelaPrincipal(tabela);
                            Console.WriteLine("Qual a quantidade a ser inclusa ao cupom:");
                            try
                            {
                                QuantProd = Convert.ToInt32(Console.ReadLine());
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("Erro: Formato de valor inválido para a quantidade. Insira um número inteiro.");
                                return;
                            }

                            if (QuantProd > produto.Quantidade)
                            {
                                throw new ApplicationException("Quantidade em estoque é insuficiente para a quantidade solicitada.");
                            }

                            int novoTotal = i + QuantProd;

                            if (novoTotal > 20)
                            {
                                throw new ApplicationException("A quantidade total de produtos no cupom excede o limite máximo permitido de 20 para cada cupom.");
                            }
                            else
                            {
                                i = novoTotal; 
                            }
                            ItemCupom itemCupom = new ItemCupom(produto, QuantProd);
                            cupom.AdicionarItemCupom(itemCupom);
                            total += (float)itemCupom.Valor;
                            if (i == 20)
                            {
                                CupomFormatado(total, cupom.GetItemCupoms(), dataemissao);
                                break;
                            }
                            Console.Clear();
                            Console.WriteLine("Produto adicionado ao cupom!");
                            Console.WriteLine("Para imprimir a nota fiscal, pressione 'F'.");
                            Console.WriteLine("Caso queira inserir mais produtos, aperte qualquer outra tecla.");
                            keyInfo = Console.ReadKey();
                            produto.Quantidade -= QuantProd;
                            //DAL.AtualizarEstoqueProduto(codProd, QuantProd);
                            QuantProd = 0;
                        }
                    }
                }
                catch (ApplicationException e)
                {
                    Console.Clear();
                    Console.WriteLine("Erro: (" + e.Message + ").");
                    Console.WriteLine("Pressione qualquer tecla para tentar novamente.");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
            while (keyInfo.Key != ConsoleKey.F && i != 20);
            
            DAL.GravarCupom(cupom);
            CupomFormatado(total, cupom.GetItemCupoms(), dataemissao);
            Console.ReadLine();
            
        }
    }
}