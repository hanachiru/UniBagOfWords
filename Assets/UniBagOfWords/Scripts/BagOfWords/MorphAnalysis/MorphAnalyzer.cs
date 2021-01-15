using System;
using System.Threading;
using System.Threading.Tasks;

namespace UniBagOfWords.MorphAnalyzer
{
    /// <summary>
    /// 形態素解析器
    /// </summary>
    public abstract class MorphAnalyzer : IDisposable
    {
        public abstract Task<Morpheme[]> AnalyzerAsync(string sentence, CancellationToken token = default);

        public static MorphAnalyzer Create(MorphAnalysisSetting setting)
        {
            switch (setting.Option)
            {
                case MorphAnalyzerOption.NMeCab:
                    return new NMeCabMorphAnalyzer(setting.NMeCabSetting.DicDir);

                default:
                    throw new ArgumentException("setting.Optionが不正な値です");
            }
        }

        public abstract void Dispose();
    }
}