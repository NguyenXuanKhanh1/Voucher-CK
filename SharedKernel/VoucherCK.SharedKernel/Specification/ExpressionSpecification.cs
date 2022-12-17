using System.Linq.Expressions;

namespace VoucherCK.SharedKernel.Specification
{
    public class ExpressionSpecification<T> : Specification<T>
    {
        private Expression<Func<T, bool>> Expression { get; }

        public ExpressionSpecification(Expression<Func<T, bool>> expression)
        {
            Expression = expression;
        }

        public override Expression<Func<T,bool>> GetExpression()
        {
            return Expression;
        }

        public override bool IsSatisfiedBy(T entity)
        {
            return Expression.Compile()(entity);
        }
    }
}
