using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using UniBagOfWords.MorphAnalyzer;
using UnityEngine;

namespace UniBagOfWords.Tests
{
    public class HttpMorphAnalyzerTest
    {
        private HttpMorphAnalyzer _analyzer;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            // NOTE : ScriptableObjectよりURLを取得する
            var setting = Resources.Load<MorphAnalysisSetting>("MorphAnalysisSetting");
            _analyzer = new HttpMorphAnalyzer(setting.HttpMeCabSetting);
        }

        [Ignore("MecabWebAPIは常に使えるわけでないため")]
        public void HttpMorphAnalyzerTestSimplePasses()
        {
            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(1));
            var sentence = "初めてUnityで形態素解析する";

            var morphemes = Task.Run(async () => await _analyzer.AnalyzeAsync(sentence, cts.Token)).Result;

            var expectedSurfaces = new[] { "初めて", "Unity", "で", "形態素", "解析", "する" };
            var expectedOriginalForm = new[] { "初めて", "Unity", "で", "形態素", "解析", "する" };
            var expectedPartsOfSpeech = new[] { "副詞", "名詞", "助詞", "名詞", "名詞", "動詞" };
            var expectedReading = new[] { "ハジメテ", "ユニティ", "デ", "ケイタイソ", "カイセキ", "スル" };

            CollectionAssert.AreEqual(expectedSurfaces, morphemes.Select(t => t.Surface).ToArray());
            CollectionAssert.AreEqual(expectedOriginalForm, morphemes.Select(t => t.Originalform).ToArray());
            CollectionAssert.AreEqual(expectedPartsOfSpeech, morphemes.Select(t => t.Pos).ToArray());
            CollectionAssert.AreEqual(expectedReading, morphemes.Select(t => t.Reading).ToArray());
        }
    }
}
