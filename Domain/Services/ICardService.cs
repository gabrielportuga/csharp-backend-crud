
using KanbanBoard.Api.Domain.Models;

namespace KanbanBoard.Api.Domain.Services
{
    public interface ICardService
    {
        public Card? GetCard(Guid cardId);

        public Card? GetCard(string titulo);

        public List<Card>? GetCards();

        public IQueryable<Card> GetCardsQuery();

        public Card AddCard(Card card);

        public Card UpdateCard(Card card);

        public List<Card>? DeleteCard(Guid cardId);
    }
}