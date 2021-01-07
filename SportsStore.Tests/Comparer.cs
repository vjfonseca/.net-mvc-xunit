
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace SportsStore.Tests
{
    public class Comparer
    {
        public static Comparer<T> Get<T>(Func<T, T, bool> func)
        {
            return new Comparer<T>(func);
        }
    }
    public class Comparer<T> : Comparer, IEqualityComparer<T>
    {
        private Func<T, T, bool> compFunction;
        public Comparer(Func<T, T, bool> func)
        {
            compFunction = func;
        }
        public bool Equals([AllowNull] T x, [AllowNull] T y)
        {
            return compFunction(x, y);
        }
        public int GetHashCode([DisallowNull] T obj)
        {
            return obj.GetHashCode();
        }
    }
}