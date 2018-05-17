using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Reflection;

namespace Predicates
{
    public class Map<T, TB> : PredicateBase<TB>
    {
        public string PropertyPath { get; }
        public PredicateBase<T> Then { get; }

        public Map(Expression<Func<TB, T>> memberAccessExpression, PredicateBase<T> then)
        {
            Then = then;
            PropertyPath = memberAccessExpression.ToPropertyPath();
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

            var parameter = Expression.Parameter(typeof(TB), "e");
            var members = PropertyPath.Split('.');

            var mi = typeof(TB).GetMember(members[0]).First();
            var ma = Expression.MakeMemberAccess(parameter, mi);

            for (var i = 1; i < members.Length; i++)
            {
                mi = mi.GetMemberType().GetMember(members[i]).First();
                ma = Expression.MakeMemberAccess(ma, mi);
            }

            var expression = Expression.Lambda<Func<TB, T>>(
                ma, parameter);

            return e => buildedThen(expression.Compile()(e));
        }
    }
}
