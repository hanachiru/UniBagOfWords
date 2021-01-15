using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UniBagOfWords.MorphAnalyzer;

namespace UniBagOfWords
{
    public class Vocabulary
    {
        private List<string> _words;
        public IReadOnlyList<string> Words => _words;
        public int Count => _words.Count;

        public Vocabulary(IEnumerable<Morpheme> morphemes)
        {
            // 重複のないコレクション作成
            var vocabulary = new HashSet<string>(morphemes.Select(word => word.Surface));
            _words = vocabulary.ToList();
        }

        /// <summary>
        /// IDを返す (なけらば-1)
        /// </summary>
        public int GetId(string word)
            => _words.IndexOf(word);

        /// <summary>
        /// Vovabularyを生成する
        /// </summary>
        public static async Task<Vocabulary> Create(IEnumerable<string> sentences, CancellationToken token = default)
        {
            var morphemes = new List<Morpheme>();
            foreach(var sentence in sentences)
            {
                var result = await MorphAnalyzerClient.AnalyzeAsync(sentence, token);

                foreach (var item in result)
                    morphemes.Add(item);
            }

            return new Vocabulary(morphemes);
        }
    }
}