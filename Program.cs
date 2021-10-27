using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqFaroShuffle
{
    class Program
    {
        public static void Main(string[] args)
        {
            var startingDeck = (from s in Suits().LogQuery("Suit Geeneration")
                                from r in Ranks().LogQuery("Rank Geeneration")
                                select new { Suit = s, Rank = r })
                                .LogQuery("Starting Deck")
                                .ToArray();
            // 上の3行（1行）をクエリ構文というらしくて、これは次のメソッド構文とまったく同じ意味（コンパイラで次のように変換される）らしい。
            // var startingDeck = Suits().SelectMany(suit => Ranks().Select(rank => new { Suit = suit, Rank = rank }));

            Console.WriteLine("Start Output startingDeck"); // debugtest

            // 生成したstartingDeckに配置される各カードをコンソールに表示する
            foreach (var c in startingDeck)
            {
                Console.WriteLine(c);
            }
            Console.WriteLine("End Output stertingDeck"); // debugtest
            Console.WriteLine(); // debugtest

            // 52枚のカードデッキを26枚ずつ分ける
            
            var top = startingDeck.Take(26);
            // Take は指定された数の要素を返す
            var bottom = startingDeck.Skip(26);
            // Skip は指定された数の要素をバイパスした残りの要素を返す
            var shuffle = top.InterleaveSequenceWith(bottom);
            // 親クラスのように第1引数 top. から InterleaveSequenceWith が呼び出されて、第2引数が普通に引数に入っているのか、まったく理解できない。ツイッターで識者に尋ねてみたが、どうやらこれは拡張メソッドというものの、独特の呼び出し方らしい。どうしてこういう構文なのかは結局わからない。けど動いている。要するに構文のシンタクスシュガーらしいが、誰が得するんだよこれは

            Console.WriteLine("Start Output FaroshuffledDeck"); // debugtest

            foreach (var c in shuffle)
            {
                Console.WriteLine(c);
            }
            Console.WriteLine("End Output FaroshuffledDeck"); // debugtest
            Console.WriteLine(); // debugtest

            var times = 0;
            // 回数を数える変数を0で初期化
            shuffle = startingDeck;
            do
            {
                /*
                // Out shuffle
                shuffle = shuffle.Take(26)
                    .LogQuery("Top Half")
                    .InterleaveSequenceWith(shuffle.Skip(26)
                    .LogQuery("Bottom Half"))
                    .LogQuery("Shuffle")
                    .ToArray();
                */

                // In shuffle
                shuffle = shuffle.Skip(26)
                    .LogQuery("Bottom Half")
                    .InterleaveSequenceWith(shuffle.Take(26)
                    .LogQuery("Top Half"))
                    .LogQuery("Shuffle")
                    .ToArray();

                Console.WriteLine($"Start Output {times + 1} times FaroshuffledDeck"); // debugtest
                foreach (var card in shuffle)
                {
                    Console.WriteLine(card);
                }
                times++;
                Console.WriteLine($"End Output {times} times FaroshuffledDeck"); // debugtest
                Console.WriteLine();
            } while (!startingDeck.SequenceEquals(shuffle));

            Console.WriteLine($"元通りになるのに {times} times かかりました");
        }

        static IEnumerable<string> Suits()
        // ※IEnumerableとはInterface Enumerable、直訳すると数えられるインターフェース、つまりは『列挙型インターフェース』のことらしい。returnひとつで実行を終了しないで、yield return でリターンをいくつも受け取るメソッドの宣言らしい。
        // ※読みは『アイ イニューメラブル』
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


/* .ToArray によって先行評価するか、普通に遅延評価するかは場合によるのでうまくやれとのこと。この場合は .ToArray による先行評価で実行の回数を大幅に減らせられたが、データベースにアクセスするクリエとかは普通に遅延評価のほうが、クリエを送った後そのまま実行を続けられるのでパフォーマンスが上がったりもする。

※ 以下、 https://docs.microsoft.com/ja-jp/dotnet/csharp/tutorials/working-with-linq#optimizations から引用

"この例は、遅延評価がパフォーマンス問題を引き起こす可能性があるユース ケースを強調することを 意図 していることに注意してください。 遅延評価がどこでコードのパフォーマンスに影響を与える可能性があるかを確認することは重要ですが、すべてのクエリを先行評価で実行する必要があるわけではないことを理解することも同じように重要です。 ToArray の無使用によってパフォーマンス ヒットが発生するのは、カード デッキの新しい並びが、毎回、直前の並びから作成されるためです。 遅延評価を使用すると、startingDeck を作成したコードを実行するときでさえも、新しいデッキ構成が毎回最初のデッキから作成されることになります。 これでは、余分な作業が多く発生してしまいます。
実際には、先行評価を使用すると効率的に動作するアルゴリズムもあれば、遅延評価を使用したほうがよいアルゴリズムもあります。 日常の使用では、データ ソースがデータベース エンジンのように個別のプロセスである場合は、遅延評価のほうが通常は適しています。 データベースでは、遅延評価を使用すると、より複雑なクエリがデータベース プロセスに対して 1 往復だけ実行された後、残りのコードに戻ることができます。 LINQ には遅延評価と先行評価のどちらを利用するかを選択できる柔軟性があるため、プロセスを測定して、最善のパフォーマンスをもたらすほうの種類の評価を選択してください。"

だそうです。わからん。
*/