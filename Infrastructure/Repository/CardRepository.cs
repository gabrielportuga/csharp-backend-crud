using KanbanBoard.Api.Domain.Models;
using KanbanBoard.Api.Infrastructure.Repository;
using KanbanBoard.Api.Infrastructure.Repository.common;

namespace KanbanBoard.Api.Domain.Repository
{
    public class CardRepository : CommonRepository<RepositoryContext, Card>, ICardRepository
    {
        private readonly RepositoryContext _repository;

        public CardRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
            _repository = repositoryContext;
        }

        public Card? GetCard(Guid cardId) => FindByCondition(c => c.Id == cardId).FirstOrDefault();

        public Card? GetCard(string titulo) => FindByCondition(c => c.Titulo == titulo).FirstOrDefault();

        public IQueryable<Card> GetCardsQuery() => FindAll();

        public List<Card> GetCards() => FindAll().ToList();

        public Card AddCard(Card card)
        {
            Create(card);
            SaveChanges();
            return card;
        }

        public Card UpdateCard(Card card)
        {
            Update(card);
            SaveChanges();
            return card;
        }

        public void DeleteCard(Card card)
        {
            Delete(card);
            SaveChanges();
        }
    }
}