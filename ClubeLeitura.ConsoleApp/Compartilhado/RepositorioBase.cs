using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace ClubeLeitura.ConsoleApp.Compartilhado
{
    public abstract class RepositorioBase<T> where T : EntidadeBase
    {
        protected readonly List<T> registros;
        protected int contadorNumero;
        //protected readonly string caminho; // tentativa antiga
        //protected readonly string pasta;

        public string Diretorio { get; init; }
        public string Arquivo { get; init; }

        public RepositorioBase()
        {
            registros = new List<T>();

            Diretorio = Environment.CurrentDirectory + "\\Dados";

            Arquivo = Diretorio + $"\\{typeof(T).Name}.json";

            if (!Directory.Exists(Diretorio))
                Directory.CreateDirectory(Diretorio);

            if (!File.Exists(Arquivo))
                File.Create(Arquivo).Close();
        }

        public virtual string Inserir(T entidade)
        {
            entidade.numero = ++contadorNumero;

            registros.Add(entidade);

            SerializarListaDeObjetos(entidade.NomeDaClasse);

            return "REGISTRO_VALIDO";
        }

        public void Editar(int numeroSelecionado, T entidade)
        {
            entidade.numero = numeroSelecionado;
            if (Excluir(numeroSelecionado))
            {
                registros.Add(entidade);
                SerializarListaDeObjetos(entidade.NomeDaClasse);
            }
        }

        public bool Excluir(int numeroSelecionado)
        {
            string nomeDaClasse = "";

            if (registros.Count > 0)
            {
                nomeDaClasse = registros[0].GetType().Name;
            }
            if (registros.RemoveAll(x => x.numero == numeroSelecionado) > 0)
            {
                SerializarListaDeObjetos(nomeDaClasse);
                return true;
            }
            else
                return false;
        }

        public T SelecionarRegistro(int numeroRegistro)
        {
            return registros.Find(x => x.numero == numeroRegistro);
        }

        public List<T> SelecionarTodos()
        {
            DeserializarListaDeObjetos();
            return registros;
        }

        public bool ExisteRegistro(int numeroRegistro)
        {
            DeserializarListaDeObjetos();
            return registros.Exists(x => x.numero == numeroRegistro);
        }

        public void SerializarListaDeObjetos(string nomeDaClasse)
        {
            string listaSerializada = JsonConvert.SerializeObject(registros, Formatting.Indented);
            StreamWriter sw = new StreamWriter(nomeDaClasse + ".json", append: true);
            sw.WriteLine(listaSerializada);
            sw.Close();
        }

        public List<T> DeserializarListaDeObjetos()
        {
            string jsonString = File.ReadAllText(Arquivo);

            if (string.IsNullOrEmpty(jsonString))
                return new List<T>();

            return JsonConvert.DeserializeObject<List<T>>(jsonString);

        }

    }
}
