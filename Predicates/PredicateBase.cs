using System;
using System.Collections.Generic;
using System.Text;

namespace Predicates
{
    public abstract class PredicateBase<T>
    {
        public abstract Predicate<T> Build();

        public static implicit operator Predicate<T>(PredicateBase<T> source)
        {
            return source.Build();
        }

        public static implicit operator Func<T, bool>(PredicateBase<T> source)
        {
            var predicate = source.Build();
            return e => predicate(e);
        }
    }
}
