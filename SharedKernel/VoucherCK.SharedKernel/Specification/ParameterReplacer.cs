using System.Linq.Expressions;


namespace VoucherCK.SharedKernel.Specification
{
    public class ParameterReplacer : ExpressionVisitor
    {
        private readonly ParameterExpression _parameterExpression;

        internal ParameterReplacer(ParameterExpression parameterExpression)
        {
            _parameterExpression = parameterExpression;
        }

        protected override Expression VisitParameter(ParameterExpression node)
            => base.VisitParameter(_parameterExpression);
    }
}
