using System;
using System.Linq;
using System.Collections.Generic;

namespace Aniversario.Model

{
    public class Pessoa
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public DateTime DataDeAniversario { get; set; }

        public Pessoa(string nome, string sobrenome, DateTime dataDeAniversario)
        {
            Nome = nome;
            Sobrenome = sobrenome;
            DataDeAniversario = dataDeAniversario;
        }
    }
}
