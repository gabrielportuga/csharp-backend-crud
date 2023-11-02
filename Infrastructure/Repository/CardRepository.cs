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

        public Guid AddCard(Card card)
        {
            try
            {
                Create(card);
                SaveChanges();
                return card.Id;
            }
            catch
            {
                throw;
            }
        }

        public Card UpdateCard(Card card)
        {
            try
            {
                Update(card);
                SaveChanges();
                return card;
            }
            catch
            {
                throw;
            }
        }

        public IQueryable<Card> GetCards() =>
            FindAll();

        public Card? GetCard(Guid id) =>
            FindByCondition(c => c.Id == id).FirstOrDefault();
    }
}