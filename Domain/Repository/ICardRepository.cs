using KanbanBoard.Api.Domain.Models;

namespace KanbanBoard.Api.Domain.Repository
{
    public interface ICardRepository
    {
        public IQueryable<Card> GetCards();

        public Card? GetCard(Guid id);

        public Guid AddCard(Card card);

        public Card UpdateCard(Card card);
    }
}