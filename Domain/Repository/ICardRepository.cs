using KanbanBoard.Api.Domain.Models;

namespace KanbanBoard.Api.Domain.Repository
{
    public interface ICardRepository
    {
        public Card? GetCard(Guid cardId);

        Card? GetCard(string titulo);

        public List<Card> GetCards();

        public IQueryable<Card> GetCardsQuery();

        public Card AddCard(Card card);

        public Card UpdateCard(Card card);

        public void DeleteCard(Card cardId);
    }
}