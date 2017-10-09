using System.Linq.Expressions;

namespace SSO.Demo.Toolkits.Helper
{
    public class ExpressionModifier : ExpressionVisitor
    {
        public ExpressionModifier(Expression newExpression, Expression oldExpression)
        {
            _newExpression = newExpression;
            _oldExpression = oldExpression;
        }

        private readonly Expression _newExpression;
        private readonly Expression _oldExpression;

        public static Expression Replace(Expression e, Expression oldExpression, Expression newExpression)
        {
            return new ExpressionModifier(newExpression, oldExpression).Replace(e);
        }

        public Expression Replace(Expression e)
        {
            return Visit(e);
        }

        public override Expression Visit(Expression node)
        {
            if (node == _oldExpression)
                return base.Visit(_newExpression);

            return base.Visit(node);
        }
    }
}
