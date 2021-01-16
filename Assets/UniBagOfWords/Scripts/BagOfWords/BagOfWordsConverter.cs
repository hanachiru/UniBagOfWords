using System;
using System.Threading;
using System.Threading.Tasks;

namespace UniBagOfWords
{
    public class BagOfWordsConverter
    {
        private Vocabulary _vocabulary;
        
        public BagOfWordsConverter(Vocabulary vocabulary)
        {
            if (vocabulary == null) throw new ArgumentException("vocabularyがNullになっています。");

            _vocabulary = vocabulary;
        }

        /// <summary>
        /// Bog Of Wordsを用いてベクトルに変換する
        /// </summary>
        public async Task<int[]> ConvertAsync(string sentence, CancellationToken token = default)
        {
            if (string.IsNullOrEmpty(sentence)) throw new ArgumentException("sentenceがNullもしくはEmptyになっています");

            var morphemes = await MorphAnalyzerClient.AnalyzeAsync(sentence, token);

            var vec = new int[_vocabulary.Count];
            foreach(var morpheme in morphemes)
            {
                var index = _vocabulary.GetId(morpheme.Surface);

                if (index != -1) vec[index]++;
            }

            return vec;
        }

        /// <summary>
        /// ConvertAsyncまたはVocabulary.Createを別スレッドから呼ぶときは，必ず一度メインスレッドによりこのメソッドを呼んだください
        /// メインスレッドのみで作業する場合は必要ありません
        /// </summary>
        /// <remarks>初回時に設定ファイルをResources.Loadで読み込むため</remarks>
        public static void InitAnalyzer()
            => MorphAnalyzerClient.Init();
    }
}