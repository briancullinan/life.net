using System;
using System.Collections.Generic;
using System.Linq.Expressions;

public partial class Expressable<TExpr> : ExpressionVisitor
    where TExpr : Expression
{
    private readonly Dictionary<Type, List<Tuple<LambdaExpression, LambdaExpression>>> _replacements;
    private readonly TExpr _expression;

    internal Expressable(TExpr expression)
    {
        _expression = expression;
        _replacements = new Dictionary<Type, List<Tuple<LambdaExpression, LambdaExpression>>>();
    }

    public TExpr AsExpression()
    {
        return VisitAndConvert(_expression, "Modify");
    }

    private Expression GetMatch<TFrom>(TFrom node)
        where TFrom : Expression
    {
        if (_replacements.ContainsKey(typeof(TFrom)))
        {
            foreach (var tuple in _replacements[typeof(TFrom)])
            {
                var condition = tuple.Item1.Compile();
                if ((bool)condition.DynamicInvoke(node))
                {
                    var express = tuple.Item2.Compile();
                    return (Expression)express.DynamicInvoke(node);
                }
            }
        }
        return null;
    }

    protected override Expression VisitParameter(ParameterExpression node)
    {
        Expression express;
        if ((express = GetMatch(node)) != null)
        {
            return express;
        }
        return base.VisitParameter(node);
    }

    protected override Expression VisitMethodCall(MethodCallExpression node)
    {
        Expression express;
        if ((express = GetMatch(node)) != null)
        {
            return express;
        }
        return base.VisitMethodCall(node);
    }

    protected override Expression VisitMember(MemberExpression node)
    {
        Expression express;
        if ((express = GetMatch(node)) != null)
        {
            return express;
        }
        return base.VisitMember(node);
    }

    protected override Expression VisitLambda<T>(Expression<T> node)
    {
        var body = Visit(node.Body);
        var param = VisitAndConvert(node.Parameters, "VisitLambda");
        var expression = Expression.Lambda(body, node.Name, node.TailCall, param);
        return expression;
    }

}