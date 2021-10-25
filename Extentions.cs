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
            // 第1引数のクラスを受け取り
            var secondIter = second.GetEnumerator();
            // 第2引数のクラスを受け取り
            while (firstIter.MoveNext() && secondIter.MoveNext())
            // topとbottomのひとつ次のカードを参照を繰り返しながら
            {
                yield return firstIter.Current;
                // topのひとつ次のカードを吐き
                yield return secondIter.Current;
                // bottomのひとつ次のカードを吐き
            } // 終わりまで繰り返す
        }

        public static bool SequenceEquals<T> (this IEnumerable<T> first, IEnumerable<T> second)
        {
            var firstIter = first.GetEnumerator();
            var secondIter = second.GetEnumerator();
            
            while (firstIter.MoveNext() && secondIter.MoveNext())
            // Faroshuffle（topとbottomのひとつ次のカードを吐く）を繰り返しながら
            {
                if (!firstIter.Current.Equals(secondIter.Current))
                
                {
                    return false;
                }
            }
            return true;
        }
    }
}