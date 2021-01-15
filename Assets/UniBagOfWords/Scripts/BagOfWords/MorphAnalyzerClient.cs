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

        /// <summary>
        /// 初期化
        /// </summary>
        /// <remarks>Resources.Loadで設定ファイルを読み込むので，必ずメインスレッドにより一度呼び出してください</remarks>
        public static void Init() { }

        /// <summary>
        /// 形態素解析を行う
        /// </summary>
        public static async Task<Morpheme[]> AnalyzeAsync(string sentence, CancellationToken token = default)
            => await _analyzer.AnalyzeAsync(sentence, token);
    }
}