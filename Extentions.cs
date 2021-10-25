// Extensions.cs
using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqFaroShuffle
{
    public static class Extensions
    {
        public static IEnumerable<T> InterleaveSequenceWith<T>(this IEnumerable<T> first, IEnumerable<T> second)
        {
            // あなたの実装はすぐにここに行く
            var firstIter = first.GetEnumerator();
            var secondIter = second.GetEnumerator();
            
            while (firstIter.MoveNext() && secondIter.MoveNext())
            {
                yield return firstIter.Current;
                yield return secondIter.Current;
// 比較を行うので yield return しないで、FaroShuffleを繰り返して、元の順序に戻った時に true を吐くプログラムに直す
//                yield return firstIter.Current;
//                yield return secondIter.Current;
            }
        }
    }
}