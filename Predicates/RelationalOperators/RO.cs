using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Predicates.RelationalOperators
{
    public class Any<T> : PredicateBase<T>
    {
        public IEnumerable<PredicateBase<T>> Elements { get; private set; }

        [JsonConstructor]
        public Any(IEnumerable<PredicateBase<T>> elements)
        {
            Elements = elements;
        }

        public Any(params PredicateBase<T>[] elements)
        {
            Elements = elements;
        }

        public override Predicate<T> Build()
        {
            var buildedElements = Elements.Select(e => e.Build());
            return e => buildedElements.Any(be => be?.Invoke(e) ?? default(bool));
        }
    }

    public class All<T> : PredicateBase<T>
    {
        public IEnumerable<PredicateBase<T>> Elements { get; private set; }

        [JsonConstructor]
        public All(IEnumerable<PredicateBase<T>> elements)
        {
            Elements = elements;
        }

        public All(params PredicateBase<T>[] elements)
        {
            Elements = elements;
        }

        public override Predicate<T> Build()
        {
            var buildedElements = Elements.Select(e => e.Build());
            return e => buildedElements.All(be => be?.Invoke(e) ?? default(bool));
        }
    }
}
