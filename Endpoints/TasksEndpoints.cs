using Dapper.Contrib.Extensions;
using static ApiTarefa.Data.Task;
using static ApiTarefa.Data.TaskContext;

namespace ApiTarefa.Endpoints;

public static class TasksEndpoints
{
    /// <summary>
    /// Classe criada com os endpoints utilizados pela API.
    /// </summary>
    /// <param name="app"></param>
    public static void MapTasksEndpoints(this WebApplication app)
    {
        var apiStatus = $"Api de tarefas, Atual {DateTime.Now}";

        /*
         Endpoint teste de funcionamento da API.
         */
        app.MapGet("/", () => Results.Ok(apiStatus));

        /*
         Endpoint obter todas as tarefas.
         */
        app.MapGet("/tarefas", async (GetConnection connection) =>
        {
            using var conn = await connection();
            return conn.GetAll<Tarefa>().ToList() is 
                List<Tarefa> tarefas ? Results.Ok(tarefas) : Results.NotFound();
        });

        /*
         Endpoint obter tarefa por id específico.
         */
        app.MapGet("/tarefas/{id}", async (int id, GetConnection connection) =>
        {
            using var conn = await connection();
            return conn.Get<Tarefa>(id) is 
                Tarefa tarefa ? Results.Ok(tarefa) : Results.NotFound();
        });

        /*
         Endpoint inserir nova tarefa.
         */
        app.MapPost("/tarefas", async(Tarefa tarefa, GetConnection connection) =>
        {
            using var conn = await connection();
            var id = conn.Insert(tarefa);
            return Results.Created($"tarefas/{id}", tarefa);
        });

        /*
         Endpoint editar uma tarefa existente.
         */
        app.MapPut("/tarefas", async (Tarefa tarefa,GetConnection connection) =>
        {
            using var conn = await connection();
            conn.Update(tarefa);
            return Results.Ok($"task id {tarefa.Id} is changed");
        });

        /*
         Endpoint excluir uma tarefa da base de dados.
         */
        app.MapDelete("/tarefas/{id}", async (int id, GetConnection connection) =>
        {
            using var conn = await connection();
            var tarefa = conn.Get<Tarefa>(id);
            if(tarefa is null)
                return Results.NotFound();
            conn.Delete(tarefa);
            return Results.Ok($"Task id {tarefa.Id} has been deleted!");
        });
    }

}
