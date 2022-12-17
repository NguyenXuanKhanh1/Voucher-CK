using System.Linq.Expressions;

namespace VoucherCK.SharedKernel.Specification
{
    internal class AndSpecification<T> : Specification<T>
    {
        private readonly ISpecification<T> _leftSpecification;
        private readonly ISpecification<T> _rightSpecification;

        public AndSpecification(ISpecification<T> leftSpec, ISpecification<T> rightSpec)
        {
            _leftSpecification = leftSpec;
            _rightSpecification = rightSpec;
        }

        public override Expression<Func<T, bool>> GetExpression()
        {
            var leftExpression = _leftSpecification.GetExpression();
            var rightExpression = _rightSpecification.GetExpression();
            var parameterExpression = leftExpression.Parameters.Single();
            var andExpression = Expression.AndAlso(leftExpression.Body, rightExpression.Body);

            andExpression = (BinaryExpression) new ParameterReplacer(parameterExpression).Visit(andExpression);
            return Expression.Lambda<Func<T, bool>>(andExpression, parameterExpression);
        }

        public override bool IsSatisfiedBy(T entity)
        {
            return _leftSpecification.IsSatisfiedBy(entity) && _rightSpecification.IsSatisfiedBy(entity);
        }
    }
}
