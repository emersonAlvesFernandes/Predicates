using System;

namespace Predicates.Utilities
{
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;

    public static class AllExtensions
    {
        public static string ToPropertyPath<TB, T>(this Expression<Func<TB, T>> memberAccessExpression)
        {
            var propertyInfo = memberAccessExpression.GetPropertyInfo();

            var name = propertyInfo.Name;

            return name;            
        }

        private static PropertyInfo GetPropertyInfo<TSource, TProperty>(this Expression<Func<TSource, TProperty>> propertyLambda)
        {
            Type type = typeof(TSource);

            MemberExpression member = propertyLambda.Body as MemberExpression;
            if (member == null)
                throw new ArgumentException(string.Format("Expression '{0}' refers to a method, not a property.", propertyLambda.ToString()));

            PropertyInfo propInfo = member.Member as PropertyInfo;

            if (propInfo == null)
                throw new ArgumentException(string.Format("Expression '{0}' refers to a field, not a property.", propertyLambda.ToString()));

            if (type != propInfo.ReflectedType && !type.IsSubclassOf(propInfo.ReflectedType))
                throw new ArgumentException(string.Format("Expression '{0}' refers to a property that is not from type {1}.", propertyLambda.ToString(), type));

            return propInfo;
        }
    }
}

// Soluction GetPropertyInfo in https://stackoverflow.com/questions/671968/retrieving-property-name-from-lambda-expression