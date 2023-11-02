

using KanbanBoard.Api.Domain.Models;
using KanbanBoard.Api.Domain.Repository;

namespace OasTools.Domain.Services
{
    public class CardService : ICardService
    {
        private readonly ICardRepository _cardRepository;

        public CardService(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }

        public Guid AddCard(Card card)
        {
            try
            {
                var cardResponse = _cardRepository.GetCard(card.Id);
                if (cardResponse == null)
                    return _cardRepository.AddCard(card);

                throw new ArgumentException("Card already exists");
            }
            catch
            {
                throw;
            }
        }

        public Card? UpdateCard(Card card)
        {
            try
            {
                var cardResponse = _cardRepository.GetCard(card.Id);
                if (cardResponse == null)
                    return _cardRepository.UpdateCard(card);

                return null;
            }
            catch
            {
                throw;
            }
        }

        public IList<Card> GetCards()
        {
            return _cardRepository.GetCards().ToList();
        }

        public IQueryable<Card> GetCardsQuery()
        {
            return _cardRepository.GetCards();
        }
    }
}