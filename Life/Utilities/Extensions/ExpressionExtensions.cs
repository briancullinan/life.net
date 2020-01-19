using System.Linq.Expressions;

public static class ExpressionExtensions
{

    public static Expressable<TExpr> AsExpressable<TExpr>(this TExpr expression)
        where TExpr : Expression
    {
        return new Expressable<TExpr>(expression);
    }
}