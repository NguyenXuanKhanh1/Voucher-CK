using System.Linq.Expressions;

namespace VoucherCK.SharedKernel.Specification
{
    internal class OrSpecification<T> : Specification<T>
    {
        private readonly ISpecification<T> _leftSpecification;
        private readonly ISpecification<T> _rightSpecification;

        public OrSpecification(ISpecification<T> leftSpec, ISpecification<T> rightSpec)
        {
            _leftSpecification = leftSpec;
            _rightSpecification = rightSpec;
        }

        public override Expression<Func<T, bool>> GetExpression()
        {
            var leftExpression = _leftSpecification.GetExpression();
            var rightExpression = _rightSpecification.GetExpression();
            var parameterExpression = leftExpression.Parameters.Single();
            var orExpression = Expression.OrElse(leftExpression.Body, rightExpression.Body);

            orExpression = (BinaryExpression)new ParameterReplacer(parameterExpression).Visit(orExpression);
            return Expression.Lambda<Func<T, bool>>(orExpression, parameterExpression);
        }

        public override bool IsSatisfiedBy(T entity)
        {
            return _leftSpecification.IsSatisfiedBy(entity) && _rightSpecification.IsSatisfiedBy(entity);
        }
    }
}
