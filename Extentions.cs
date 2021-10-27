// Extensions.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO; // "debug.log" を吐くためのクラス

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

        // "debug.log" を出力するメソッド
        public static IEnumerable<T> LogQuery<T> (this IEnumerable<T> sequence, string tag)
        {
            // File.AppendText creates a new file if the file doesn't exist.
            using (var writer = File.AppendText("debug.log"))
            {
                writer.WriteLine($"Executing Query {tag}");
            }

            return sequence;
        }
    }
}