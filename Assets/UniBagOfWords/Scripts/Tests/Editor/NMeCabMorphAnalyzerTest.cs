﻿using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using UniBagOfWords.MorphAnalyzer;

namespace UniBagOfWords.Tests
{
    public class NMeCabMorphAnalyzerTest
    {
        private NMeCabMorphAnalyzer _analyzer;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            // NOTE : フォルダ移動の可能性があるので，"UniBagOfWords"のパスから見つける
            var topDirectoryPath = Directory.GetDirectories("Assets", "*", SearchOption.AllDirectories)
                .FirstOrDefault(path => Path.GetFileName(path) == "UniBagOfWords");
            var dicDir = $"{topDirectoryPath}/Scripts/dic/ipadic";

            var setting = new NMeCabSetting(dicDir);
            _analyzer = new NMeCabMorphAnalyzer(setting);
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _analyzer.Dispose();
        }

        [Test]
        public void NMeCabMorphAnalyzerSimplePasses()
        {
            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(1));
            var sentence = "初めてUnityで形態素解析する";

            var morphemes = Task.Run(async () => await _analyzer.AnalyzeAsync(sentence, cts.Token)).Result;

            var expectedSurfaces = new[] { "初めて", "Unity", "で", "形態素", "解析", "する" };
            var expectedOriginalForm = new[] { "初めて", "*", "で", "形態素", "解析", "する" };
            var expectedPartsOfSpeech = new[] { "副詞", "名詞", "助詞", "名詞", "名詞", "動詞" };
            var expectedReading = new[] { "ハジメテ", "", "デ", "ケイタイソ", "カイセキ", "スル" };

            CollectionAssert.AreEqual(expectedSurfaces, morphemes.Select(t => t.Surface).ToArray());
            CollectionAssert.AreEqual(expectedOriginalForm, morphemes.Select(t => t.Originalform).ToArray());
            CollectionAssert.AreEqual(expectedPartsOfSpeech, morphemes.Select(t => t.Pos).ToArray());
            CollectionAssert.AreEqual(expectedReading, morphemes.Select(t => t.Reading).ToArray());
        }

        [Test]
        public void AnalyzeErrorCase()
        {
            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(1));

            try
            {
                var morphemes = Task.Run(async () => await _analyzer.AnalyzeAsync(null, cts.Token)).Result;
                Assert.Fail();
            }
            catch (AggregateException)
            {
                Assert.Pass();
            }
        }
    }
}
