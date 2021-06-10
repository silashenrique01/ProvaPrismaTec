using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app_prova_prismatec.Models
{
    public class Funcionario
    {
        public Guid Id { get; private set; }

        public string Cpf { get; private set; }

        public string Nome { get; private set; }

        public Guid IdEmpresa { get; private set; }

        public Funcionario()
        {
            Id = Guid.NewGuid();
        }

        public Funcionario(string cpf, string nome, Guid idEmpresa)
        {
            Id = Guid.NewGuid();
            Cpf = cpf;
            Nome = nome;
            IdEmpresa = idEmpresa;
        }
    }
}
