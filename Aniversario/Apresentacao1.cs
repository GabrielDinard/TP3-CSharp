using System;
using System.Linq;
using Aniversario.Model;
using Aniversario.Dados;
using System.Collections.Generic;
using System.Text;
using System.IO;


namespace Aniversario.Apresentacao
{
    class Apresentacao1
    {
        private static void EscreverNaTela(string texto)
        {
            Console.WriteLine(texto);
        }

        public static void MenuPrincipal()
        {
            EscreverNaTela("Gerenciador de Aniversários");
            EscreverNaTela("1 - Consultar uma pessoa");
            EscreverNaTela("2 - Adicionar uma pessoa");
            EscreverNaTela("3 - Sair");

            char operacao = Console.ReadLine().ToCharArray()[0];

            switch (operacao)
            {
                case '1': ConsultarPessoa(); break;
                case '2': AdicionarPessoa(); break;
                case '3': break;
            }
        }

        private static void ConsultarPessoa()
        {
            Console.Clear();

            OpcoesDeConsulta();
        }

        private static void AdicionarPessoa()
        {
            Console.Clear();

            Console.WriteLine("Informe o nome da pessoa");
            string nome = Console.ReadLine();

            Console.WriteLine("Informe o sobrenome da pessoa");
            string sobrenome = Console.ReadLine();

            Console.WriteLine("Informe a data de aniversário da pessoa");
            var dataDeAniversario = DateTime.Parse(Console.ReadLine());

            var pessoa = new Pessoa(nome, sobrenome, dataDeAniversario);            

            BancoDeDadosEmMemoria bancodadosmemoria = new BancoDeDadosEmMemoria();

            bancodadosmemoria.Salvar(pessoa);

            Console.WriteLine("A pessoa foi cadastrada com sucesso!");
            Console.WriteLine("Pressione qualquer tecla para continuar");
            Console.ReadKey();
            Console.Clear();

            MenuPrincipal();
        }

        private static void OpcoesDeConsulta()
        {
            Console.WriteLine("Escolha uma das opções de consulta:");
            Console.WriteLine("1 - Consultar pelo nome");
            Console.WriteLine("2 - Consultar pelo sobrenome");
            Console.WriteLine("3 - Consultar pela data de aniversário");

            string tipoDeConsulta = Console.ReadLine();

            switch (tipoDeConsulta)
            {
                case "1":
                    ConsultarPeloNome();
                    break;

                case "2":
                    ConsultarPeloSobrenome();
                    break;

                case "3":
                    ConsultarPelaData();
                    break;

                default:
                    Console.WriteLine("Consulta incorreta");
                    OpcoesDeConsulta();
                    break;
            }
        }

        private static void ConsultarPeloNome()
        {
            Console.WriteLine("Digite o nome da pessoa:");
            string nome = Console.ReadLine();

            BancoDeDadosEmMemoria bancodadosmemoria = new BancoDeDadosEmMemoria();           

            var pessoasEncontradas = bancodadosmemoria.BuscarTodasAsPessoas(nome);

            int quantidadeDePessoasEncontradas = pessoasEncontradas.Count();

            if (quantidadeDePessoasEncontradas > 0)
            {
                Console.WriteLine("Pessoas encontradas:");

                foreach (var pessoa in bancodadosmemoria.BuscarTodasAsPessoas(nome))
                {
                    Console.WriteLine($"Nome: {pessoa.Nome} \nSobrenome: {pessoa.Sobrenome} \nData de aniversário: {pessoa.DataDeAniversario}");
                    var dataDeAniversario = pessoa.DataDeAniversario;
                    Console.WriteLine($"Faltam {DiasRestantesAniversario(dataDeAniversario)} dias para o aniversário dessa pessoa");
                }
            }
            else
            {
                Console.WriteLine("Nenhuma pessoa encontrada para o nome: " + nome);
            }

            MenuPrincipal();
        }
       
        private static void ConsultarPeloSobrenome()
        {
            Console.WriteLine("Digite o sobrenome da pessoa:");
            string sobrenome = Console.ReadLine();

            BancoDeDadosEmMemoria bancodadosmemoria = new BancoDeDadosEmMemoria();

            var pessoasEncontradas = bancodadosmemoria.BuscarPessoaPelo(sobrenome);

            int quantidadeDePessoasEncontradas = pessoasEncontradas.Count();

            if (quantidadeDePessoasEncontradas > 0)
            {
                Console.WriteLine("Pessoas encontradas:");

                foreach (var pessoa in bancodadosmemoria.BuscarPessoaPelo(sobrenome))
                {
                    Console.WriteLine($"Nome: {pessoa.Nome} \nSobrenome: {pessoa.Sobrenome} \nData de aniversário: {pessoa.DataDeAniversario}");
                    var dataDeAniversario = pessoa.DataDeAniversario;
                    Console.WriteLine($"Faltam {DiasRestantesAniversario(dataDeAniversario)} dias para o aniversário dessa pessoa");                   
                }
            }
            else
            {
                Console.WriteLine("Nenhuma pessoa encontrada para o nome: " + sobrenome);
            }           

            MenuPrincipal();
        }

        private static void ConsultarPelaData()
        {
            Console.WriteLine("Digite a data de aniversário da pessoa:");
            var dataDeAniversario = DateTime.Parse(Console.ReadLine());

            BancoDeDadosEmMemoria bancodadosmemoria = new BancoDeDadosEmMemoria();

            var pessoasEncontradas = bancodadosmemoria.BuscarTodasAsPessoas(dataDeAniversario);

            int quantidadeDePessoasEncontradas = pessoasEncontradas.Count();

            if (quantidadeDePessoasEncontradas > 0)
            {
                Console.WriteLine("Pessoas encontradas");

                foreach (var pessoa in bancodadosmemoria.BuscarTodasAsPessoas(dataDeAniversario))
                {
                    Console.WriteLine($"Nome: {pessoa.Nome} \nSobrenome: {pessoa.Sobrenome} \nData de aniversário: {pessoa.DataDeAniversario}");
                }
            }
            else
            {
                Console.WriteLine("Nenhuma pessoa encontrada para o nome: " + dataDeAniversario);
            }

            Console.WriteLine($"Faltam {DiasRestantesAniversario(dataDeAniversario)} dias para o aniversário dessa pessoa");

            MenuPrincipal();
        }
        private static int DiasRestantesAniversario(DateTime dataDeAniversario)
        {
            var proximoAniversario = dataDeAniversario.AddYears(DateTime.Today.Year - dataDeAniversario.Year);
            if (proximoAniversario < DateTime.Today)
            {
                proximoAniversario = proximoAniversario.AddYears(1);                
            }

            if (dataDeAniversario == DateTime.Today)
            {
                Console.WriteLine("Hoje é o aniversário dessa pessoa!");               
            }

            return (proximoAniversario - DateTime.Today).Days;
        }
    }
}
