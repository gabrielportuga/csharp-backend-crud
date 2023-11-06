using AutoMapper;
using KanbanBoard.Api.Domain.Dtos;
using KanbanBoard.Api.Domain.Models;
using KanbanBoard.Api.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using KanbanBoard.Api.Domain.Services;
using OasFacadeService.Middleware;

namespace KanbanBoard.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CardController : ControllerBase
    {
        private readonly ICardService _cardService;
        private readonly IMapper _mapper;

        public CardController(ICardService CardService, IMapper mapper)
        {
            _cardService = CardService;
            _mapper = mapper;
        }

        // GET: api/cards
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetPaginated(int? pageNumber)
        {
            var result = _cardService.GetCardsQuery();

            var paginatedResult = await PaginatedList<Card>.CreateAsync(result, pageNumber ?? 1, 10);

            return Ok(paginatedResult);
        }

        // // POST api/cards
        [Authorize]
        [HttpPost]
        public void Post([FromBody] CardDto cardDto)
        {
            var cardRequest = _mapper.Map<Card>(cardDto);
            var cardId = _cardService.AddCard(cardRequest);
            Ok(new { id = cardId });
        }

        [Authorize]
        [RequestHandlerFilter]
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] Card card)
        {
            return Ok(_cardService.UpdateCard(card));
        }

        // // POST api/cards
        [Authorize]
        [RequestHandlerFilter]
        [HttpDelete("{id}")]
        public ActionResult<List<Card>?> Delete(Guid id)
        {
            return Ok(_cardService.DeleteCard(id));
        }

        [Authorize]
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return $"value {id}";
        }

    }
}
