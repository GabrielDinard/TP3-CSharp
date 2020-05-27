using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Aniversario.Model;

namespace Aniversario.Dados
{
    public class BancoDeDadosEmMemoria : BancoDeDados
    {
        private static List<Pessoa> pessoasCadastradas = new List<Pessoa>();

        public override void Salvar(Pessoa pessoa)
        {
            bool pessoaJaExisteNaListaDeCadastrados = false;

            foreach (var pess in pessoasCadastradas)
            {
                if (pess == pessoa)
                {
                    pessoaJaExisteNaListaDeCadastrados = true;
                }
            }

            if (pessoaJaExisteNaListaDeCadastrados == false)
            {
                pessoasCadastradas.Add(pessoa);
            }
        }

        public override IEnumerable<Pessoa> BuscarTodasAsPessoas()
        {
            return pessoasCadastradas;
        }

        public override IEnumerable<Pessoa> BuscarTodasAsPessoas(string nome)
        {
            //dica: nas consultas retornas ienumerable
            return pessoasCadastradas
                   .Where(pessoa => pessoa.Nome.Contains(nome, StringComparison.InvariantCultureIgnoreCase));
        }

        public override IEnumerable<Pessoa> BuscarTodasAsPessoas(DateTime dataDeAniversario)
        {
            return pessoasCadastradas
                   .Where(pessoa => pessoa.DataDeAniversario.Date == dataDeAniversario);
        }

        public override IEnumerable<Pessoa> BuscarPessoaPelo(string sobrenome)
        {
            return pessoasCadastradas
                    .Where(pessoa => pessoa.Sobrenome.Contains(sobrenome, StringComparison.InvariantCultureIgnoreCase));
        }

        public override void Excluir(Pessoa pessoa)
        {
            pessoasCadastradas.Remove(pessoa);
        }       
    }
}
