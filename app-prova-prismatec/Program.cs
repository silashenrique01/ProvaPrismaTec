using app_prova_prismatec.Models;
using static System.Console;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;

namespace app_prova_prismatec
{
    class Program
    {
        static void Main(string[] args)
        {
            //TODO: Conforme a abordagem Domain Driven Design, crie uma instância e utilize a entidade "Empresa" e "Funcionario", populando suas propriedades.

            //TODO: Salve o objeto criado em formato .json em um arquivo local (pasta configurada no app settings).

            //TODO: Altere os dados das propriedades e salve novamente o arquivo (o mesmo não pode ser sobrescrito).

            //TODO: Crie um método que verifica se no telefone da empresa, se o código da região é do RS (Utilize LINQ).

            //TODO: Imprima no console logs dos eventos de todas as ações realizadas no sistema.

            Home();
        }

        public static void Home()
        {
            string arquivoJson = Environment.CurrentDirectory;
            arquivoJson = arquivoJson.Replace("\\bin\\Debug", "");
            arquivoJson = arquivoJson + "\\appsettings.json";
            PrismatecJson prismatecJson = new PrismatecJson();

            WriteLine("-- Menu Principal --");
            WriteLine("");
            while (true)
            {
                WriteLine("Opções : 1 - Cadastrar empresa, 2 - Cadastrar funcionario, 3 - Atualizar empresa, 4 - Atualizar funcionario - Selecionar \n");
                try
                {
                    var option = ReadLine();
                    switch (option)
                    {
                        case "1":
                            prismatecJson.AdicionarEmpresa(arquivoJson);
                            break;
                        case "2":
                            prismatecJson.AdicionarFuncionario(arquivoJson);
                            break;
                        case "3":
                            prismatecJson.AtualizarEmpresa(arquivoJson);
                            break;
                        case "4":
                            prismatecJson.AtualizarFuncionario(arquivoJson);
                            break;
                        default:
                            Main(null);
                            break;
                    }
                }
                catch (Exception ex)
                {
                    WriteLine("Erro : " + ex.Message);
                }
            }
        }
    }
}
