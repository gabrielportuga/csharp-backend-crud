namespace KanbanBoard.Api.Utils.Security
{
    public interface IJwtToken
    {
        string GenerateJwtToken(string username);
    }
}