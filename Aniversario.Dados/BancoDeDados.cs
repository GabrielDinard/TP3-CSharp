using System;
using System.Collections.Generic;
using Aniversario.Model;

namespace Aniversario.Dados
{
    public abstract class BancoDeDados
    {
        public abstract void Salvar(Pessoa pessoa);
        public abstract void Excluir(Pessoa pessoa);
        public abstract IEnumerable<Pessoa> BuscarTodasAsPessoas();
        public abstract IEnumerable<Pessoa> BuscarTodasAsPessoas(string nome);
        public abstract IEnumerable<Pessoa> BuscarTodasAsPessoas(DateTime dataDeAniversario);
        public abstract IEnumerable<Pessoa> BuscarPessoaPelo(string sobrenome);
    }
}
