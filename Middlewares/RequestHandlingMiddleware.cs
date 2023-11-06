using System.Net;
using System.Text;
using KanbanBoard.Api.Domain.Models;
using KanbanBoard.Api.Domain.Services;
using Newtonsoft.Json;

namespace OasFacadeService.Middleware
{
    /// <summary>
    /// Request timestamp middleware
    /// </summary>
    public class RequestHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                if (context.Request.Method == HttpMethods.Put)
                {
                    string? cardId = context.Request.Query["id"];

                    if (context.Request.Body != null)
                    {
                        var reader = new StreamReader(context.Request.Body, Encoding.UTF8);
                        var body = await reader.ReadToEndAsync();
                        var card = JsonConvert.DeserializeObject<Card>(body);


                        if (card == null || card!.Titulo == null || card!.Conteudo == null || card!.Lista == null)
                        {
                            context.Response.StatusCode = 400;
                        }
                        else if (cardId != card?.Id.ToString())
                        {
                            context.Response.StatusCode = 404;
                        }
                        else
                        {

                            Card? cardReturned = GetCard(context, cardId);

                            if (cardReturned == null)
                            {
                                context.Response.StatusCode = 404;
                            }
                            else
                            {
                                WriteRequest(card, "Alterar");
                            }
                        }
                    }
                    else
                    {
                        context.Response.StatusCode = 400;
                    }
                }
                else if (context.Request.Method == HttpMethods.Delete)
                {
                    string? cardId = context.Request.Query["id"];

                    Card? card = GetCard(context, cardId);
                    if (card == null)
                    {
                        context.Response.StatusCode = 404;
                    }

                    WriteRequest(card, "Remover");
                }

                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Card? GetCard(HttpContext context, string cardId)
        {
            var cardService = context.RequestServices.GetRequiredService<ICardService>();
            Card? card = cardService.GetCard(Guid.Parse(cardId!));
            return card;
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsync(ex.Message);
        }

        private static void WriteRequest(Card? card, string requestType)
        {
            if (card != null)
            {
                Console.WriteLine($"{DateTime.UtcNow} . Card {card.Id} - {card.Titulo} - {requestType}");
            }
        }
    }
}
