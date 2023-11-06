using System.Net;
using System.Text;
using KanbanBoard.Api.Domain.Models;
using KanbanBoard.Api.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace OasFacadeService.Middleware
{
    public class RequestHandlerFilter : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var httpContext = context.HttpContext;
            var urlID = httpContext.Request.RouteValues["id"]?.ToString();
            var dateTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            try
            {
                if (string.IsNullOrEmpty(urlID))
                {
                    context.Result = new BadRequestResult();
                    return;
                }

                var card = GetCard(httpContext, urlID);

                if (card == null)
                {
                    context.Result = new NotFoundResult();
                    return;
                }

                if (httpContext.Request.Method == HttpMethods.Put)
                {

                    using var reader = new StreamReader(httpContext.Request.Body, Encoding.UTF8);
                    var body = await reader.ReadToEndAsync();
                    var cardBody = JsonConvert.DeserializeObject<Card>(body);

                    if (urlID != cardBody.Id.ToString())
                    {
                        context.Result = new BadRequestObjectResult(new { error = "ids não correspondem" });
                        return;
                    }

                    if (string.IsNullOrEmpty(cardBody.Titulo) || string.IsNullOrEmpty(cardBody.Conteudo) || string.IsNullOrEmpty(cardBody.Lista) || string.IsNullOrEmpty(cardBody.Id.ToString()))
                    {
                        context.Result = new BadRequestResult();
                        return;
                    }

                    Console.WriteLine($"{dateTime} - Card {urlID} - {card.Titulo} - Alterar");

                }
                else if (httpContext.Request.Method == HttpMethods.Delete)
                {
                    Console.WriteLine($"{dateTime} - Card {urlID} - {card.Titulo} - Remover");
                }

            }
            catch (System.Exception)
            {

                throw;
            }
            await next();
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is Exception exception)
            {
                context.Result = new ObjectResult(exception.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                };
                context.ExceptionHandled = true;
            }
        }


        private static Card? GetCard(HttpContext context, string cardId)
        {
            var cardService = context.RequestServices.GetRequiredService<ICardService>();
            Card? card = cardService.GetCard(Guid.Parse(cardId!));
            return card;
        }


    }
}