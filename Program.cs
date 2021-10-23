using System;
using System.Collections.Generic;
using System.Linq;

namespace faroshuffle
{
    class Program
    {
        static void Main(string[] args)
        {
//            var startingDeck = from s in Suits()
//                               from r in Ranks()
//                               select new { Suit = s, Rank = r };
            // 上の3行（1行）をクエリ構文というらしくて、これは次のメソッド構文とまったく同じ意味（コンパイラで次のように変換される）らしい。
            // var startingDeck = Suits().SelectMany(suit => Ranks().Select(rank => new { Suit = suit, Rank = rank }));
            // ので、上のクリエ構文をコメントアウトしてメソッド構文に置き換えてみるブランチを作ってみる
            var startingDeck = Suits().SelectMany(suit => Ranks().Select(rank => new { Suit = suit, Rank = rank }));

            // 生成したstartingDeckに配置される各カードをコンソールに表示します
            foreach (var card in startingDeck)
            {
                Console.WriteLine(card);
            }

            // 52枚のカードデッキを26枚ずつ分ける
            var top = startingDeck.Take(26);
            // Take は指定された数の要素を返す
            var bottom = startingDeck.Skip(26);
            // Skip は指定された数の要素をバイパスした残りの要素を返す
        }
        static IEnumerable<string> Suits()
        {
            yield return "clubs";
            yield return "diamonds";
            yield return "hearts";
            yield return "spades";
        }
        static IEnumerable<string> Ranks()
        {
            yield return "two";
            yield return "three";
            yield return "four";
            yield return "five";
            yield return "six";
            yield return "seven";
            yield return "eight";
            yield return "nine";
            yield return "ten";
            yield return "jack";
            yield return "queen";
            yield return "king";
            yield return "ace";
        }
    }
}
