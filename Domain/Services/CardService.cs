using KanbanBoard.Api.Domain.Models;
using KanbanBoard.Api.Domain.Repository;

namespace KanbanBoard.Api.Domain.Services
{
    public class CardService : ICardService
    {
        private readonly ICardRepository _cardRepository;

        public CardService(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }

        public Card? GetCard(Guid cardId)
        {
            return _cardRepository.GetCard(cardId);
        }

        public Card? GetCard(string titulo)
        {
            return _cardRepository.GetCard(titulo);
        }

        public List<Card>? GetCards()
        {
            return _cardRepository.GetCards().ToList();
        }

        public IQueryable<Card> GetCardsQuery()
        {
            return _cardRepository.GetCardsQuery();
        }

        public Card AddCard(Card card)
        {
            var cardDb = _cardRepository.GetCard(card.Titulo);
            if (cardDb == null)
                return _cardRepository.AddCard(card);

            throw new ArgumentException($"Titulo {card.Titulo} já existente!", nameof(card));
        }

        public Card UpdateCard(Card card)
        {
            var cardDb = _cardRepository.GetCard(card.Id)!;

            cardDb.Titulo = card.Titulo;
            cardDb.Conteudo = card.Conteudo;
            cardDb.Lista = card.Lista;

            return _cardRepository.UpdateCard(cardDb);
        }

        public List<Card>? DeleteCard(Guid cardId)
        {
            var cardDb = _cardRepository.GetCard(cardId)!;

            _cardRepository.DeleteCard(cardDb);
            return _cardRepository.GetCards();

        }
    }
}