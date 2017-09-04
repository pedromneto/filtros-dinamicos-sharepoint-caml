using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCamlex
{
    static class FiltroAlunos
    {
        //Filtro pelo nome do aluno
        internal static Expression<Func<ListItem, bool>> PorNome(string nome) =>
            (item => ((string)item["Title"]) == nome);

        //Filtra pelo inicio do nome do aluno
        internal static Expression<Func<ListItem, bool>> InicioNome(string nome) =>
            (item => ((string)item["Title"]).StartsWith(nome));

        //Filtra os alunos de um determinado curso
        internal static Expression<Func<ListItem, bool>> Curso(string nome) =>
            (item => (item["Curso"] == (CamlexNET.DataTypes.LookupValue)nome));

        //Filtra os alunos matriculados
        internal static Expression<Func<ListItem, bool>> Matriculados(bool matriculado) =>
            (item => (((Boolean)item["Matriculado"])) == matriculado);

        //Filtra pelos nomes enviados
        internal static Expression<Func<ListItem, bool>> Nomes(string[] nome) =>
            (item => (nome.Contains((string)item["Title"])));

        //Filtro por campo do tipo texto
        internal static Expression<Func<ListItem, bool>> FiltroTexto(string nomeCampo, string valorFiltro) =>
            (item => (string)item[nomeCampo] == valorFiltro);

        internal static void ExibirResultado(ListItemCollection itens)
        {
            foreach (var item in itens)
            {
                Console.WriteLine($" * Nome: {item["Title"]}, Curso: {((FieldLookupValue)item["Curso"]).LookupValue}, Cidade: {item["gnhm"]}");
            }

            Console.WriteLine("=================================\n");
        }
    }
}
