using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Reflection;
using Predicates.Utilities;

namespace Predicates
{
    public class Map<T, TB> : PredicateBase<TB>
    {
        public string PropertyPath { get; }
        public PredicateBase<T> Then { get; }
        public string ParameterName { get; }

        public Map(Expression<Func<TB, T>> memberAccessExpression, PredicateBase<T> then)
        {
            Then = then;
            PropertyPath = memberAccessExpression.ToPropertyPath();
            
            // TODO: Adapt for expression with more than one parameter.
            ParameterName = memberAccessExpression.Parameters.FirstOrDefault()?.Name;
        }

        [JsonConstructor]
        public Map(string propertyPath, PredicateBase<T> then)
        {
            Then = then;
            PropertyPath = propertyPath;
        }

        public override Predicate<TB> Build()
        {
            var buildedThen = Then.Build();

            var parameter = Expression.Parameter(typeof(TB), ParameterName);
            var members = PropertyPath.Split('.');

            var mi = typeof(TB).GetMember(members[0]).First();
            var ma = Expression.MakeMemberAccess(parameter, mi);

            for (var i = 1; i < members.Length; i++)
            {
                mi = mi.GetType().GetMember(members[i]).First();
                ma = Expression.MakeMemberAccess(ma, mi);
            }

            var expression = Expression.Lambda<Func<TB, T>>(
                ma, parameter);

            return e => buildedThen(expression.Compile()(e));
        }
    }
}
