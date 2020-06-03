using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Aniversario.Model;
using System.Security.Principal;
using System.Net.NetworkInformation;

namespace Aniversario.Dados
{
    public class BancoDeDadosEmArquivo : BancoDeDados
    {

        public override IEnumerable<Pessoa> BuscarTodasAsPessoas()
        {
            string nomeDoArquivo = ObterNomeArquivo();

            FileStream arquivo;
            if (!File.Exists(nomeDoArquivo))
            {
                arquivo = File.Create(nomeDoArquivo);
                arquivo.Close();
            }

            string resultado = File.ReadAllText(nomeDoArquivo);

            string[] pessoas = resultado.Split(';');

            List<Pessoa> pessoasList = new List<Pessoa>();           

            for (int i = 0; i < pessoas.Length - 1; i++)
            {
                string[] dadosDaPessoa = pessoas[i].Split(',');

                string nome = dadosDaPessoa[0];
                string sobrenome = dadosDaPessoa[1];
                DateTime dataDeAniversario = Convert.ToDateTime(dadosDaPessoa[2]);

                Pessoa pessoa = new Pessoa(nome, sobrenome, dataDeAniversario);

                pessoasList.Add(pessoa);               
            }
              return pessoasList;
        }

        public static string ObterNomeArquivo()
        {
            var pastaDesktop = Environment.SpecialFolder.Desktop;

            string localDaPastaDesktop = Environment.GetFolderPath(pastaDesktop);
            string nomeDoArquivo = @"\dadosDasPessoas.txt";

            return localDaPastaDesktop + nomeDoArquivo;
        }

        public override void Salvar(Pessoa pessoa)
        {
            string nomeDoArquivo = ObterNomeArquivo();

            string formato = $"{pessoa.Nome},{pessoa.Sobrenome},{pessoa.DataDeAniversario.ToString()};";

            File.AppendAllText(nomeDoArquivo, formato);
        }

        public override IEnumerable<Pessoa> BuscarPessoaPelo(string sobrenome)
        {
            var pessoas = this.BuscarTodasAsPessoas();

            foreach (Pessoa pessoa in pessoas)
            {
                if (pessoa.Sobrenome == sobrenome)
                   yield return pessoa;
            }

              yield return null;
        }

        public override IEnumerable<Pessoa> BuscarTodasAsPessoas(string nome)
        {
            var pessoas = this.BuscarTodasAsPessoas();

            foreach (Pessoa pessoa in pessoas)
            {
                if (pessoa.Nome == nome)
                    yield return pessoa;
            }

             yield return null;
        }

        public override IEnumerable<Pessoa> BuscarTodasAsPessoas(DateTime dataDeAniversario)
        {
            var pessoas = this.BuscarTodasAsPessoas();

            foreach (Pessoa pessoa in pessoas)
            {
                if (pessoa.DataDeAniversario == dataDeAniversario)
                     yield return pessoa;
            }

             yield return null;
        }

        public override void Excluir(Pessoa pessoa)
        {
            throw new NotImplementedException();
        }

        protected override void Alterar(Pessoa pessoa)
        {
            throw new NotImplementedException();
        }
    }
}
