public class JwtTokenValidationMiddleware
{
    private readonly RequestDelegate _next;

    public JwtTokenValidationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        // Verifique se o token está presente no cabeçalho da solicitação
        string token = context.Request.Headers["Authorization"];

        if (!string.IsNullOrEmpty(token))
        {
            // Aqui você deve implementar a lógica de validação do token.
            // Verifique se o token é correto, válido e não expirou.
            bool tokenValid = ValidateToken(token);

            if (tokenValid)
            {
                // Se o token for válido, prossiga para o próximo middleware
                await _next(context);
            }
            else
            {
                // Token inválido, retorne o status 401 Unauthorized
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Token inválido.");
            }
        }
        else
        {
            // Token não fornecido, retorne o status 401 Unauthorized
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Token não fornecido.");
        }
    }

    private bool ValidateToken(string token)
    {
        // Implemente a validação do token aqui, por exemplo, verifique se o token é válido e não expirou.
        // Você pode usar bibliotecas como JWT para validar tokens JWT.
        // Retorne true se o token for válido, caso contrário, retorne false.
        // Exemplo simplificado:
        return token == "token_valido";
    }
}
