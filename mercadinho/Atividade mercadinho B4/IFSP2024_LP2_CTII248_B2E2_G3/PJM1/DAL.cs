using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PJM1
{
    internal class DAL
    {
        const string arqListaProd = @"C:\matar\LISTAPROD.CSV"; //Essa é a pasta que utilizamos nos testes de PL, sendo o nome definido pelo prof Asenjo.
        const string arqNumCupom = @"C:\matar\NUMCUPOM.TXT";

        public static int ObterProxNumCupom()
        {
            try
            {
                using (StreamReader sr = new StreamReader(arqNumCupom))
                {
                    string conteudo = sr.ReadToEnd();
                    int numCupom = Convert.ToInt32(conteudo);
                    return numCupom;
                }
            }
            catch (IOException erro)
            {
                Console.WriteLine("Erro: " + erro.Message);
                return 1;
            }
            catch (FormatException erro)
            {
                Console.WriteLine("Erro de formatação: " + erro.Message);
                return 1;
            }
            catch (Exception erro)
            {
                Console.WriteLine("Erro inesperado: " + erro.Message);
                return 1;
            }
        }

        public static void AtualizarProxNumCupom(int proxNum)
        {
            using (StreamWriter sw = new StreamWriter(arqNumCupom, false))
            {
                sw.Write(proxNum + 1);
            }
        }

        public static void GravarCupom(Cupom cupom)
        {
            string arqCupom = @"C:\matar\CUPOM" + cupom.NumeroCupom.ToString("D6") + ".TXT";
            try
            {
                using (StreamWriter sw = new StreamWriter(arqCupom))
                {
                    sw.WriteLine(cupom.NumeroCupom + "," + cupom.DataEmissaoCupom.ToString() + "," + cupom.CpfCliente);
                    foreach (ItemCupom item in cupom.ItensCupom)
                    {
                        if (item != null)
                        {
                            sw.WriteLine(item.Produto.Codigo + "," + item.Quantidade + "," + item.Produto.Preco.ToString("F2"));
                        }
                    }
                }
            }
            catch (IOException erro)
            {
                Console.WriteLine("Erro: " + erro.Message);
            }
            catch (Exception erro)
            {
                Console.WriteLine("Erro: " + erro.Message);
            }
        }

        public static List<Produtos> ObterTabelaProdutos()
        {
            List<Produtos> leitura = new List<Produtos>();
            try
            {
                using (StreamReader sr = new StreamReader(arqListaProd))
                {
                    string linha;
                    while ((linha = sr.ReadLine()) != null) //Acho que assim fica bem melhor
                    {
                        string[] info = linha.Split(",");
                        /*if (Convert.ToDouble(info[2]) > 0)
                        {
                            double num = Convert.ToDouble(info[2]) / 100;
                            Produtos produto = new Produtos(info[0], info[1], num, Convert.ToInt32(info[3]), info[4]);
                            leitura.Add(produto);
                        }
                        else
                        {
                            Produtos produto = new Produtos(info[0], info[1], Convert.ToDouble(info[2]), Convert.ToInt32(info[3]), info[4]);
                            leitura.Add(produto);
                        }*/
                        
                        double y = double.Parse(info[2], CultureInfo.InvariantCulture);
                        Console.WriteLine(y);

                        Produtos produto = new Produtos(info[0], info[1], y, Convert.ToInt32(info[3]), info[4]);
                        leitura.Add(produto);

                    }
                }
            }
            catch (IOException erro)
            {
                Console.WriteLine("Erro: " + erro.Message);
            }
            catch (FormatException erro)
            {
                Console.WriteLine("Erro de formatação: " + erro.Message);
            }
            catch (Exception erro)
            {
                Console.WriteLine("Erro inesperado: " + erro.Message);
            }
            return leitura;
        }

        public static void AtualizarTabelaProdutos(List<Produtos> produtos)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(arqListaProd, false))
                {
                    foreach (var produto in produtos)
                    {
                        sw.WriteLine(produto.Codigo + "," + produto.Descricao + "," + produto.Preco + "," + produto.Quantidade + "," + produto.Unidade);
                    }
                }
            }
            catch (IOException erro)
            {
                Console.WriteLine("Erro de E/S: " + erro.Message);
            }
            catch (Exception erro)
            {
                Console.WriteLine("Erro inesperado: " + erro.Message);
            }
        }
        public static void AtualizarEstoqueProduto(string codigoProduto, int quantidadeComprada)
        {
            List<Produtos> produtos = DAL.ObterTabelaProdutos();
            Produtos produto = null;
            foreach (var p in produtos)
            {
                if (p.Codigo == codigoProduto)
                {
                    produto = p;
                    break;
                }
            }
            if (produto != null)
            {
                if (produto.Quantidade >= quantidadeComprada)
                {
                    produto.Quantidade -= quantidadeComprada; DAL.AtualizarTabelaProdutos(produtos);
                }
                else
                {
                    Console.WriteLine("Quantidade em estoque insuficiente.");
                }
            }
            else
            {
                Console.WriteLine("Produto não encontrado.");
            }
        }
    }
}