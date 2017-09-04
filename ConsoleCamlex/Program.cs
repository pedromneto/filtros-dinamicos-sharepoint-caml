using CamlexNET;
using CamlexNET.Interfaces;
using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleCamlex
{
    class Program
    {
        static void Main(string[] args)
        {

            var url = "https://meusite.sharepoint.com/";

            using (ClientContext context = new ClientContext(url))
            {
                context.Credentials = new SharePointOnlineCredentials(Credenciais.UserName, Credenciais.Password());
                var web = context.Web;
                var lstAlunos = web.GetList("/Lists/Alunos");

                context.Load(lstAlunos);
                context.ExecuteQuery();

                var query = Camlex.Query();

                //Filtrar por nome
                var filtroNome = FiltroAlunos.PorNome("Pedro");
                var pedros = lstAlunos.GetItems(query.Where(filtroNome).ToCamlQuery());


                //Filtrar os alunos que começam com a letra M
                var filtroAlunosLetraM = FiltroAlunos.InicioNome("M");
                var alunosLetraM = lstAlunos.GetItems(query.Where(filtroAlunosLetraM).ToCamlQuery());

                //Filtrar alunos pelo curso de Administração
                var filtroCurso = FiltroAlunos.Curso("Administração - Manhã");
                var alunosAdm = lstAlunos.GetItems(query.Where(filtroCurso).ToCamlQuery());

                //Filtrar alunos que não estão matriculados
                var filtroNaoMatriculados = FiltroAlunos.Matriculados(false);
                var alunosNaoMatriculados = lstAlunos.GetItems(query.Where(filtroNaoMatriculados).ToCamlQuery());

                //Filtrar alunos pelos nomes
                string[] nomes = { "Joaquina", "Samara" };
                var filtroNomes = FiltroAlunos.Nomes(nomes);
                var alunosDiversosNomes = lstAlunos.GetItems(query.Where(filtroNomes).ToCamlQuery());

                //Filtrar pelo campo Cidade = Nova Lima
                var filtroTexto = FiltroAlunos.FiltroTexto("Cidade", "Nova Lima");
                var alunosNovaLima = lstAlunos.GetItems(query.Where(filtroTexto).ToCamlQuery());

                //Filtrar os alunos que se chamam pedro ou começam com a letra M
                var filtroPedroM = new List<Expression<Func<ListItem, bool>>>();
                filtroPedroM.Add(FiltroAlunos.PorNome("Pedro"));
                filtroPedroM.Add(FiltroAlunos.InicioNome("M"));
                var alunosPedroOuM = lstAlunos.GetItems(query.WhereAny(filtroPedroM).ToCamlQuery());

                //Filtrar os alunos que começam com a letra M e que estão matriculados
                var filtroMEMatriculados = new List<Expression<Func<ListItem, bool>>>();
                filtroMEMatriculados.Add(FiltroAlunos.InicioNome("m"));
                filtroMEMatriculados.Add(FiltroAlunos.Matriculados(true));
                var alunosMeMatriculados = lstAlunos.GetItems(query.WhereAll(filtroMEMatriculados).ToCamlQuery());


                context.Load(pedros);
                context.Load(alunosLetraM);
                context.Load(alunosPedroOuM);
                context.Load(alunosNaoMatriculados);
                context.Load(alunosAdm);
                context.Load(alunosDiversosNomes);
                context.Load(alunosNovaLima);
                context.Load(alunosMeMatriculados);

                context.ExecuteQuery();

                ExibeTitulo("Apenas Pedros");
                FiltroAlunos.ExibirResultado(pedros);

                ExibeTitulo("Apenas alunos que começam com a letra M");
                FiltroAlunos.ExibirResultado(alunosLetraM);

                ExibeTitulo("Apenas alunos de administração manhã");
                FiltroAlunos.ExibirResultado(alunosAdm);

                ExibeTitulo("Apenas alunos matriculados");
                FiltroAlunos.ExibirResultado(alunosNaoMatriculados);

                ExibeTitulo("Apenas alunos com os nomes Samara ou Joaquina");
                FiltroAlunos.ExibirResultado(alunosDiversosNomes);

                ExibeTitulo("Filtro por campo dinâmico (Cidade =  Nova Lima)");
                FiltroAlunos.ExibirResultado(alunosNovaLima);

                ExibeTitulo("Apenas alunos que se chamam Pedro ou começam com a letra M");
                FiltroAlunos.ExibirResultado(alunosPedroOuM);

                ExibeTitulo("Apenas alunos que começam com a letra M e que estejam matriculados");
                FiltroAlunos.ExibirResultado(alunosMeMatriculados);
            }
        }

        static void ExibeTitulo(string titulo)
        {
            var corPadrao = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.Blue;

            Console.WriteLine(titulo.ToUpper());
            Console.Write("\n");

            Console.ForegroundColor = corPadrao;
        }

    }
}

