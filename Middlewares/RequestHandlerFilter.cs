using System.Net;
using KanbanBoard.Api.Domain.Models;
using KanbanBoard.Api.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace OasFacadeService.Middleware
{
    public class RequestHandlerFilter : Attribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var httpContext = context.HttpContext;
            var cardId = httpContext.Request.RouteValues["id"]?.ToString();
            var dateTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");


            if (string.IsNullOrEmpty(cardId))
            {
                context.Result = new BadRequestResult();
                return;
            }

            var card = GetCard(httpContext, cardId);

            if (card == null)
            {
                context.Result = new NotFoundResult();
                return;
            }

            if (httpContext.Request.Method == HttpMethods.Put)
            {
                var cardBody = context.ActionArguments["card"] as Card;

                if (cardId != cardBody.Id.ToString())
                {
                    context.Result = new BadRequestObjectResult(new { error = "ids não correspondem" });
                    return;
                }

                if (string.IsNullOrEmpty(cardBody.Titulo) || string.IsNullOrEmpty(cardBody.Conteudo) || string.IsNullOrEmpty(cardBody.Lista) || string.IsNullOrEmpty(cardBody.Id.ToString()))
                {
                    context.Result = new BadRequestResult();
                    return;
                }


                Console.WriteLine($"{dateTime} - Card {cardId} - {card.Titulo} - Alterar");

            }
            else if (httpContext.Request.Method == HttpMethods.Delete)
            {
                Console.WriteLine($"{dateTime} - Card {cardId} - {card.Titulo} - Remover");
            }
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