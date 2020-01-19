using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Life.Utilities.Extensions;

public partial class Expressable<TExpr>
{

    private readonly Dictionary<Type, ParameterExpression> _replaceMembers = new Dictionary<Type, ParameterExpression>();
    public Expressable<TExpr> Replace<TFrom, TTo, TOut>(Expression<Func<TFrom, TOut>> from,
                                                         Expression<Func<TTo, TOut>> to)
    {
        if (typeof (TFrom) != typeof (TTo))
        {
            Replace<MethodCallExpression>(
                expr => expr.Method.GetGenericArguments().Contains(typeof (TFrom)),
                expr => Expression.Call(Visit(expr.Object), Replace(expr.Method, typeof (TFrom), typeof (TTo)),
                                        expr.Arguments.Select(Visit)));

            // replace parameter with original parameter replacement
            if (_replaceMembers.ContainsKey(typeof (TFrom)))
            {
                Replace<ParameterExpression>(
                    expr => expr == to.Parameters[0],
                    expr => Replace(expr, _replaceMembers[typeof(TFrom)]));
            }
            else
            {
                _replaceMembers.Add(typeof(TFrom), to.Parameters[0]);
                Replace<ParameterExpression>(
                    expr => expr.Type == typeof (TFrom),
                    expr => Replace(expr, to.Parameters[0]));

                Replace<ParameterExpression>(
                    expr => expr.Type.IsGenericType && expr.Type.GetGenericArguments().Contains(typeof (TFrom)),
                    expr =>
                    Replace(expr, Expression.Parameter(Replace(expr.Type, typeof (TFrom), typeof (TTo)), expr.Name)));
            }

            var body = from.Body as MemberExpression;
            // TODO: check for unnecessary convert method
            if (body != null)
            {
                Replace<MemberExpression>(
                    expr => expr.Member == body.Member,
                    expr => Visit(to.Body));
            }
        }

        return this;
    }

    private readonly Dictionary<ParameterExpression, ParameterExpression> _params = new Dictionary<ParameterExpression, ParameterExpression>();
    private ParameterExpression Replace(ParameterExpression search, ParameterExpression replace)
    {
        if (_params.ContainsKey(search))
            return _params[search];

        _params.Add(search, replace);
        return replace;
    }

    private Type Replace(Type type, Type search, Type replace)
    {
        return type.GetGenericTypeDefinition()
                   .MakeGenericType(type.GetGenericArguments()
                                        .Replace(search, replace)
                                        .ToArray());
    }

    private MethodInfo Replace(MethodInfo method, Type search, Type replace)
    {
        return method.GetGenericMethodDefinition()
                     .MakeGenericMethod(method.GetGenericArguments()
                                              .Replace(search, replace)
                                              .ToArray());
    }

    private Expressable<TExpr> Replace<TFrom>(Expression<Func<TFrom, bool>> condition, Expression<Func<TFrom, Expression>> from)
        where TFrom : Expression
    {
        var tuple = new Tuple<LambdaExpression, LambdaExpression>(condition, from);
        if (_replacements.ContainsKey(typeof (TFrom)))
            _replacements[typeof (TFrom)].Add(tuple);
        else
            _replacements.Add(typeof (TFrom), new List<Tuple<LambdaExpression, LambdaExpression>> {tuple});

        return this;
    }
}
