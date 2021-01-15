using System.Threading;
using System.Threading.Tasks;
using UniBagOfWords.MorphAnalyzer;
using UnityEngine;

namespace UniBagOfWords
{
    /// <summary>
    /// 形態素解析器を使うクライアント
    /// </summary>
    internal static class MorphAnalyzerClient
    {
        private static MorphAnalyzer.MorphAnalyzer _analyzer;

        static MorphAnalyzerClient()
        {
            var setting = Resources.Load<MorphAnalysisSetting>("MorphAnalysisSetting");
            _analyzer = MorphAnalyzer.MorphAnalyzer.Create(setting);
        }

        public static async Task<Morpheme[]> AnalyzeAsync(string sentence, CancellationToken token = default)
            => await _analyzer.AnalyzeAsync(sentence, token);
    }
}