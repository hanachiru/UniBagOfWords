using System.Threading;
using System.Threading.Tasks;
using UniBagOfWords.MorphAnalyzer;
using UnityEngine;

namespace UniBagOfWords
{
    /// <summary>
    /// 形態素解析器を使うクライアント
    /// </summary>
    public static class MorphAnalyzerClient
    {
        private static MorphAnalyzer.MorphAnalyzer _analyzer;

        static MorphAnalyzerClient()
        {
            var setting = Resources.Load<MorphAnalysisSetting>("MorphAnalysisSetting");
            _analyzer = MorphAnalyzer.MorphAnalyzer.Create(setting);
        }

        /// <summary>
        /// 静的コンストラクタを強制的に呼ぶ
        /// </summary>
        /// <remarks>
        /// 初回時に設定ファイルをResources.Loadで読み込むため
        /// </remarks>
        public static void Init() { }

        /// <summary>
        /// 形態素解析を行う
        /// </summary>
        public static async Task<Morpheme[]> AnalyzeAsync(string sentence, CancellationToken token = default)
            => await _analyzer.AnalyzeAsync(sentence, token);
    }
}