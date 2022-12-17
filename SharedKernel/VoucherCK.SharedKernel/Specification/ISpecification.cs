using System.Linq.Expressions;

namespace VoucherCK.SharedKernel.Specification
{
    public interface ISpecification<TEntity>
    {
        Expression<Func<TEntity, bool>> GetExpression();
        bool IsSatisfiedBy(TEntity entity);
        ISpecification<TEntity> And(ISpecification<TEntity> specification);
        ISpecification<TEntity> Or(ISpecification<TEntity> specification);
        ISpecification<TEntity> Not(ISpecification<TEntity> specification);
    }
}
