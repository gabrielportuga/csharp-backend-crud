using AutoMapper;
using KanbanBoard.Api.Domain.Dtos;
using KanbanBoard.Api.Domain.Models;
using KanbanBoard.Api.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OasTools.Domain.Services;

namespace OasTools.Controllers
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


        [Authorize]
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return $"value {id}";
        }

        // // POST api/cards
        [Authorize]
        [HttpPost]
        public void Post(CardDto cardDto)
        {
            try
            {
                var cardRequest = _mapper.Map<Card>(cardDto);
                var cardId = _cardService.AddCard(cardRequest);
                Created(Url.ToString() ?? "", new { id = cardId });
            }
            catch
            {
                BadRequest();
            }

        }


    }
}
