using System;
using System.Collections.Generic;
using System.Text;

namespace Predicates.Operators
{
    public class LessThan<T> : PredicateBase<T>
        where T : IComparable
    {
        public T Value { get; private set; }

        public LessThan(T value)
        {
            Value = value;
        }

        public override Predicate<T> Build()
        {
            return e => Value.CompareTo(e) > 0;
        }
    }
}
