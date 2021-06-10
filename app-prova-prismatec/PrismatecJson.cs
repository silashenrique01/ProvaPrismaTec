using app_prova_prismatec.Models;
using Newtonsoft.Json;
using System.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using static System.Console;
using Newtonsoft.Json.Linq;

namespace app_prova_prismatec
{
    public class PrismatecJson
    {
        //incluir empresa
        public void AdicionarEmpresa(string arquivoJson)
        {
            WriteLine("Informe o CNPJ da empresa : ");
            var CNPJ = Console.ReadLine();

            WriteLine("\nQual a Razão Social da Empresa : ");
            var RazaoSocial = Console.ReadLine();

            WriteLine("\nQual o nome fantasia da Empresa : ");
            var NomeFantasia = Console.ReadLine();

            WriteLine("\nQual o telefone da Empresa : ");
            var Telefone = Console.ReadLine();

            var empresa = new Empresa(CNPJ, RazaoSocial, NomeFantasia, Telefone);

            var novaEmpresaMembro = JsonConvert.SerializeObject(empresa);
            try
            {
                var json = File.ReadAllText(arquivoJson);
                var jsonObj = JObject.Parse(json);
                var arrayEmpresas = jsonObj.GetValue("empresas") as JArray;
                var novaEmpresa = JObject.Parse(novaEmpresaMembro);
                arrayEmpresas.Add(novaEmpresa);
                jsonObj["empresas"] = arrayEmpresas;
                string novoJsonResult = JsonConvert.SerializeObject(jsonObj, Formatting.Indented);
                File.WriteAllText(arquivoJson, novoJsonResult);
            }
            catch (Exception ex)
            {
                WriteLine("Erro ao adicionar : " + ex.Message.ToString());
            }
        }

        public void AdicionarFuncionario(string arquivoJson)
        {

            WriteLine("Informe o CPF do Funcionario: ");
            var CPF = Console.ReadLine();

            WriteLine("\nQual o nome do funcionario: ");
            var Nome = Console.ReadLine();

            var empresaId = BuscarEmpresa(arquivoJson);

            var funcionario = new Funcionario(CPF, Nome, empresaId);

            var novoFuncionarioMembro = JsonConvert.SerializeObject(funcionario);
            try
            {
                string json = File.ReadAllText(arquivoJson);
                var jObject = JObject.Parse(json);
                JArray arrayEmpresas = (JArray)jObject["empresas"];
                var novoFuncionario = JObject.Parse(novoFuncionarioMembro);
                var empresaDoFuncionario = arrayEmpresas.FirstOrDefault(obj => obj["Id"].Value<string>() == novoFuncionario["IdEmpresa"].Value<string>()) as JObject;

                var arrayFuncionarios = empresaDoFuncionario.GetValue("Funcionarios") as JArray;
                arrayFuncionarios.Add(novoFuncionario);

                jObject["empresas"] = arrayEmpresas;
                string novoJsonResult = JsonConvert.SerializeObject(jObject, Formatting.Indented);
                File.WriteAllText(arquivoJson, novoJsonResult);
            }
            catch (Exception ex)
            {
                WriteLine("Erro ao adicionar : " + ex.Message.ToString());
            }
        }

        //deletar empresa
        public void DeletarEmpresa(string arquivoJson)
        {
            var json = File.ReadAllText(arquivoJson);
            try
            {
                var jObject = JObject.Parse(json);
                JArray arrayExperiencias = (JArray)jObject["experiencias"];
                Write("Informe o ID da Empresa a deletar : ");
                var empresaId = Convert.ToInt32(Console.ReadLine());

                if (empresaId > 0)
                {
                    var nomeEmpresa = string.Empty;
                    var empresaADeletar = arrayExperiencias.FirstOrDefault(obj => obj["empresaid"].Value<int>() == empresaId);

                    arrayExperiencias.Remove(empresaADeletar);

                    string saida = JsonConvert.SerializeObject(jObject, Formatting.Indented);
                    File.WriteAllText(arquivoJson, saida);
                }
                else
                {
                    Write("O ID da empresa é inválido, tente novamente!");
                    AtualizarEmpresa(arquivoJson);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        //atualizar empresa
        public void AtualizarEmpresa(string arquivoJson)
        {

            string json = File.ReadAllText(arquivoJson);
            try
            {
                var jObject = JObject.Parse(json);
                JArray arrayExperiencias = (JArray)jObject["experiencias"];
                Write("Informe o ID da empresa a atualizar : ");
                var empresaId = Convert.ToInt32(Console.ReadLine());

                if (empresaId > 0)
                {
                    Write("Informe o nome da empresa: ");
                    var nomeEmpresa = Convert.ToString(Console.ReadLine());

                    foreach (var empresa in arrayExperiencias.Where(obj => obj["empresaid"].Value<int>() == empresaId))
                    {
                        empresa["empresanome"] = !string.IsNullOrEmpty(nomeEmpresa) ? nomeEmpresa : "";
                    }

                    jObject["experiencias"] = arrayExperiencias;
                    string saida = JsonConvert.SerializeObject(jObject, Formatting.Indented);
                    File.WriteAllText(arquivoJson, saida);
                }
                else
                {
                    Write("O ID da empresa é inválido, tente novamente!");
                    AtualizarEmpresa(arquivoJson);
                }
            }
            catch (Exception ex)
            {
                WriteLine("Erro de Atualização : " + ex.Message.ToString());
            }
        }

        public Guid BuscarEmpresa(string arquivoJson)
        {
            var guid = new Guid();
            string json = File.ReadAllText(arquivoJson);
            try
            {
                var jObject = JObject.Parse(json);
                JArray arrayEmpresas = (JArray)jObject["empresas"];
                Write("Informe o CNPJ da empresa do funcionario: ");
                var cnpj = Convert.ToInt32(Console.ReadLine());

                if (cnpj > 0)
                {
                    foreach (var empresa in arrayEmpresas.Where(obj => obj["Cnpj"].Value<int>() == cnpj))
                    {
                        guid = Guid.ParseExact(empresa["Id"].ToString(), "D");
                    }
                }
                else
                {
                    Write("O CNPJ da empresa é inválido, tente novamente!");
                    AdicionarFuncionario(arquivoJson);
                }
            }
            catch (Exception ex)
            {
                WriteLine("Erro de Atualização : " + ex.Message.ToString());
            }
            return guid;
        }

        //fim
    }
}
