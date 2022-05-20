using System.Data;

namespace ApiTarefa.Data;

public class TaskContext
{
    public delegate Task<IDbConnection> GetConnection();
}
