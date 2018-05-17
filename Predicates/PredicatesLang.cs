using Predicates.Operators;
using Predicates.RelationalOperators;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Predicates
{
    public static class PredicatesLang
    {
        public static PredicateBase<T> And<T>(this PredicateBase<T> left, PredicateBase<T> right)
        {
            return new All<T>(left, right);
        }

        public static PredicateBase<T> Or<T>(this PredicateBase<T> left, PredicateBase<T> right)
        {
            return new Any<T>(left, right);
        }

        public static LessThan<T> LessThan<T>(T value)
            where T : IComparable
        {
            return new LessThan<T>(value);
        }

        public static GreaterThan<T> GreaterThan<T>(T value)
            where T : IComparable
        {
            return new GreaterThan<T>(value);
        }

        public static Equals<T> Equals<T>(T value)
            where T : IComparable
        {
            return new Equals<T>(value);
        }

        public static WhenIs<T, TB> When<T, TB>(Expression<Func<TB, T>> input)
        {
            return new WhenIs<T, TB>(input);
        }


        public class WhenIs<T, TB>
        {
            private readonly Expression<Func<TB, T>> _source;

            public WhenIs(Expression<Func<TB, T>> source)
            {
                _source = source;
            }

            public PredicateBase<TB> Is(PredicateBase<T> predicate)
            {
                return new Map<T, TB>(_source, predicate);
            }
        }
    }
}
