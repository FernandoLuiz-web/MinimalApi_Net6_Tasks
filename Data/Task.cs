using System.ComponentModel.DataAnnotations.Schema;

namespace ApiTarefa.Data;

public class Task
{
    [Table("Tarefa")]
    public record Tarefa(int Id, string Atividade, string Status);

}
