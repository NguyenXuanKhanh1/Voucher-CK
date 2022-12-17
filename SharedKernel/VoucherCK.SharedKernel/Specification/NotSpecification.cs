using System.Linq.Expressions;

namespace VoucherCK.SharedKernel.Specification
{
    internal class NotSpecification<T> : Specification<T>
    {
        private readonly ISpecification<T> _specification;
        public NotSpecification(ISpecification<T> specification)
        {
            _specification = specification;
        }

        public override Expression<Func<T, bool>> GetExpression()
        {
            return Expression.Lambda<Func<T, bool>>(
                Expression.Not(_specification.GetExpression().Body), _specification.GetExpression().Parameters.Single());
        }

        public override bool IsSatisfiedBy(T entity)
        {
            return !_specification.IsSatisfiedBy(entity);
        }
    }
}
