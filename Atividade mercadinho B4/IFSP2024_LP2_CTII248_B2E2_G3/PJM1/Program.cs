namespace PJM1
{
    using System.IO;
    internal class Program
    { //Feito por João Gabriel F. Souza, Ricardo Amaro e Diego Toscano.


        static void Linha()
        {
            Console.WriteLine("----------------------------------------------------------------------------------|");
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
            Linha();
            Console.WriteLine("{0, -81} {1}", DateTime.Now, "|");
            Console.Write("{0, -81} {1, 1}", "CCF:000003 COO:000006", "|");
            Console.WriteLine();
            Linha();
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
            Linha();
            Console.WriteLine("{0" +
            "} {1, 73}", "Dinheiro:", total.ToString("C2") + " |");
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
        static void Main(string[] args)
        {
            Cupom cupom = new Cupom();
            int NumeroCupom, QuantProd;
            float total = 0.0f;
            string CpfCliente;
            List<Produtos> tabela = new List<Produtos>
            {
            new Produtos("7891025101604", "Leite", 3.0, 15, "l"),
            new Produtos("7891000105016", "Barra de cereal", 3.40, 28, "lt"),
            new Produtos("7891000120101", "Creme de leite", 3.70, 23, "cx"),
            new Produtos("7891000100103", "Leite condensado", 4.5, 18, "und"),
            new Produtos("7891999000538", "Iogurte", 1.98, 32, "gf"),
            new Produtos("7896051126041", "Leite fermentado" ,2.12, 12, "cx"),
            new Produtos("7897236904805", "Água", 1.5, 48, "cp"),
            new Produtos("7622300830083", "Biscoito recheado", 1.80, 35, "pct"),
            new Produtos("7891150036567", "Caldo de galinha", 1.90, 16, "cx"),
            new Produtos("4005900036728", "Desodorante", 11.10, 25, "und"),
            new Produtos("7896185989819", "Vitamina C", 35.20, 26, "und"),
            new Produtos("7898113811452", "Lanche", 9.5, 37, "und")
            };
            var dataemissao = DateTime.Now;
            static void tabelaprincipal(List<Produtos> tabela)
            {
                Linha();
                Console.WriteLine("|   Código   |    Descrição    |   Preço   |  Quant.Estoque   | Valor em Estoque  |");
                Linha();


                foreach (Produtos produto in tabela)
                {
                    Console.WriteLine("{0, -13} {1,-17} {2, -16:C} {3, 0} {4, -10} {5, 15:C}", produto.Codigo, produto.Descricao, "R$    " + produto.Preco.ToString("F2"), produto.Quantidade, produto.Unidade, "R$          " + produto.Valor.ToString("F2"));
                }
                Console.WriteLine("__________________________________________________________________________________");
                Console.WriteLine();
            }
            tabelaprincipal(tabela);
            
            int arqN = 1;
            int iLaco = 0;
            //string arqProdutos = @"E:\Disciplinas Técnicas\LP2024\BD_Mercadinho";
            string arqProdutos1 = @"C:\FEDERAL\LP\mercadinho";
            try
            {
                using (FileStream fs = new FileStream(arqProdutos1, FileMode.CreateNew))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        sw.WriteLine("7891025101604, Leite, 3.0, 15, l" +
                                     "\r\n7891000105016, Barra de cereal, 3.40, 28, lt" +
                                     "\r\n7891000120101, Creme de leite, 3.70, 23, cx" +
                                     "\r\n7891000100103, Leite condensado, 4.5, 18, und" +
                                     "\r\n7891999000538, Iogurte, 1.98, 32, gf" +
                                     "\r\n7896051126041, Leite fermentado ,2.12, 12, cx" +
                                     "\r\n7897236904805, Água, 1.5, 48, cp" +
                                     "\r\n7622300830083, Biscoito recheado, 1.80, 35, pct" +
                                     "\r\n7891150036567, Caldo de galinha, 1.90, 16, cx" +
                                     "\r\n4005900036728, Desodorante, 11.10, 25, und" +
                                     "\r\n7896185989819, Vitamina C, 35.20, 26, und" +
                                     "\r\n7898113811452, Lanche, 9.5, 37, und");
                    }
                }
            }
            catch (IOException e)
            {
                
                while (iLaco == 0)
                {
                    string arqProdutos2 = @"C:\FEDERAL\LP\mercadinho";
                    Console.WriteLine("O arquivo já existes, desejas criares um novo? Y / N");
                    string resposta = Console.ReadLine().ToUpper();
                    char respostaFinal = char.Parse(resposta);
                    switch (respostaFinal)
                    {
                        case 'N':
                            using (FileStream fs = new FileStream(arqProdutos2, FileMode.Create))
                            {
                                using (StreamWriter sw = new StreamWriter(fs))
                                {
                                    
                                    sw.WriteLine("7891025101604, Leite, 3.0, 15, l" +
                                                 "\r\n7891000105016, Barra de cereal, 3.40, 28, lt" +
                                                 "\r\n7891000120101, Creme de leite, 3.70, 23, cx" +
                                                 "\r\n7891000100103, Leite condensado, 4.5, 18, und" +
                                                 "\r\n7891999000538, Iogurte, 1.98, 32, gf" +
                                                 "\r\n7896051126041, Leite fermentado ,2.12, 12, cx" +
                                                 "\r\n7897236904805, Água, 1.5, 48, cp" +
                                                 "\r\n7622300830083, Biscoito recheado, 1.80, 35, pct" +
                                                 "\r\n7891150036567, Caldo de galinha, 1.90, 16, cx" +
                                                 "\r\n4005900036728, Desodorante, 11.10, 25, und" +
                                                 "\r\n7896185989819, Vitamina C, 35.20, 26, und" +
                                                 "\r\n7898113811452, Lanche, 9.5, 37, und");
                                }
                                iLaco++;
                            }
                            break;
                        case 'Y': 
                            //string arqProdutos = @"E:\Disciplinas Técnicas\LP2024\BD_Mercadinho(" + arqN + ")";
                            string arqProdutos3 = @"C:\FEDERAL\LP\mercadinho(" + arqN + ")";
                            try
                            {
                                using (FileStream fs = new FileStream(arqProdutos3, FileMode.Create))
                                {

                                    using (StreamWriter sw = new StreamWriter(fs))
                                    {
                                        
                                        sw.WriteLine("7891025101604, Leite, 3.0, 15, l" +
                                            "\r\n7891000105016, Barra de cereal, 3.40, 28, lt" +
                                            "\r\n7891000120101, Creme de leite, 3.70, 23, cx" +
                                            "\r\n7891000100103, Leite condensado, 4.5, 18, und" +
                                            "\r\n7891999000538, Iogurte, 1.98, 32, gf" +
                                            "\r\n7896051126041, Leite fermentado ,2.12, 12, cx" +
                                            "\r\n7897236904805, Água, 1.5, 48, cp" +
                                            "\r\n7622300830083, Biscoito recheado, 1.80, 35, pct" +
                                            "\r\n7891150036567, Caldo de galinha, 1.90, 16, cx" +
                                            "\r\n4005900036728, Desodorante, 11.10, 25, und" +
                                            "\r\n7896185989819, Vitamina C, 35.20, 26, und" +
                                            "\r\n7898113811452, Lanche, 9.5, 37, und");
                                    }
                                }
                                iLaco++;
                                arqN++;
                            }
                            catch (Exception b)
                            { 
                                Console.WriteLine("Erro: " + b); 
                            }
                            break;
                        case '\0':
                            Console.WriteLine("Por favor, insira uma das devidas letras senhor(a).");
                            break;
                    }
                }
            }
            
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
                    cupom.setCpfCliente(CpfCliente);
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
                    Console.Write("Insira o número do cupom: ");
                    NumeroCupom = Convert.ToInt32(Console.ReadLine());
                    validadorCupom = true;
                    cupom.setNumeroCupom(NumeroCupom);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Erro: Formato inválido para o número do cupom. Insira um número inteiro.");
                }
            } while (!validadorCupom);

            Console.Clear();
            cupom.GetDataEmissao();
            int i = 0;
            string codProd = "";
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
                    tabelaprincipal(tabela);
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
                            cupom.setItemCupom(itemCupom);
                            total += (float)itemCupom.Valor;
                            if (i == 20)
                            {
                                CupomFormatado(total, cupom.GetItemCupom(), dataemissao);
                                break;
                            }
                            Console.Clear();
                            Console.WriteLine("Produto adicionado ao cupom!");
                            Console.WriteLine("Para imprimir a nota fiscal, aperte a tecla 'F'.");
                            Console.WriteLine("Caso queira inserir mais produtos, aperte qualquer outra tecla.");
                            keyInfo = Console.ReadKey();
                            produto.Quantidade -= QuantProd;
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
            CupomFormatado(total, cupom.GetItemCupom(), dataemissao);
        }
    }
}