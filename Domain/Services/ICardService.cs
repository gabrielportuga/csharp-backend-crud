
using KanbanBoard.Api.Domain.Models;

namespace OasTools.Domain.Services
{
    public interface ICardService
    {
        public IList<Card> GetCards();

        public IQueryable<Card> GetCardsQuery();

        public Guid AddCard(Card card);

        public Card? UpdateCard(Card card);
    }
}