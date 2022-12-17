using System.Linq.Expressions;

namespace VoucherCK.SharedKernel.Specification
{
    public abstract class Specification<TEntity> : ISpecification<TEntity>
    {
        public abstract Expression<Func<TEntity, bool>> GetExpression();
        public abstract bool IsSatisfiedBy(TEntity entity);

        public ISpecification<TEntity> And(ISpecification<TEntity> specification)
        {
            return new AndSpecification<TEntity>(this, specification);
        }

        public ISpecification<TEntity> Or(ISpecification<TEntity> specification)
        {
            return new OrSpecification<TEntity>(this, specification);
        }

        public ISpecification<TEntity> Not(ISpecification<TEntity> specification)
        {
            return new AndSpecification<TEntity>(this, new NotSpecification<TEntity>(specification));
        }
    }
}
