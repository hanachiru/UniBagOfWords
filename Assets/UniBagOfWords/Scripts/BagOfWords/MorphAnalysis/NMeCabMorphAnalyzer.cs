using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NMeCab.Specialized;

namespace UniBagOfWords.MorphAnalyzer
{
    /// <summary>
    /// NMeCabを用いた形態素解析器
    /// </summary>
    public class NMeCabMorphAnalyzer : MorphAnalyzer
    {
        private MeCabIpaDicTagger _tagger;
        public string DicDir { get; }

        public NMeCabMorphAnalyzer(NMeCabSetting setting)
        {
            if (string.IsNullOrEmpty(setting.DicDir)) throw new ArgumentException("dicDirが正しく設定されていません");

            DicDir = setting.DicDir;
            _tagger = MeCabIpaDicTagger.Create(DicDir);
        }

        /// <summary>
        /// 形態素解析を行う
        /// </summary>
        public override Task<Morpheme[]> AnalyzeAsync(string sentence, CancellationToken token = default)
        {
            if (string.IsNullOrEmpty(sentence)) throw new ArgumentException("sentenceがNullもしくはemptyになっています。");
            return Task.Run(() => _tagger.Parse(sentence).Select(node => Convert(node)).ToArray(), token);
        }
        /// <summary>
        /// MeCabIpaDicNodeからMorphemeに変換する
        /// </summary>
        private Morpheme Convert(MeCabIpaDicNode node)
            => new Morpheme(node.Surface, node.PartsOfSpeech, node.OriginalForm, node.Reading);

        public override void Dispose()
            => _tagger.Dispose();
    }
}
