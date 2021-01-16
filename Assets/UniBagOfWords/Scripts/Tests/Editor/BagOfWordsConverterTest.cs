using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace UniBagOfWords.Tests
{
    public class BagOfWordsConverterTest
    {
        [Test]
        public void BagOfWordsConverterTestSimplePasses()
        {
            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(1));

            var sentences = new[]
            {
                "私はラーメンが好きです。",
                "私は餃子が好きです。",
                "私はラーメンが嫌いです。"
            };

            BagOfWordsConverter.InitAnalyzer();
            var vocabulary = Task.Run(async () => await Vocabulary.Create(sentences, cts.Token)).Result;

            var expectedWords = new[] { "私", "は", "ラーメン", "が", "好き", "です", "。", "餃子", "嫌い" };

            Assert.AreEqual(vocabulary.Count, 9);
            CollectionAssert.AreEqual(vocabulary.Words, expectedWords);

            var converter = new BagOfWordsConverter(vocabulary);

            var sentence = "私は餃子が好きです。私は。";

            var bowVec = Task.Run(async () => await converter.ConvertAsync(sentence, cts.Token)).Result;

            var expectedVec = new int[] { 2, 2, 0, 1, 1, 1, 2, 1, 0 };

            CollectionAssert.AreEqual(bowVec, expectedVec);
        }

        [Test]
        public void VacabularyErrorCase()
        {
            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(1));

            try
            {
                var vocabulary = Task.Run(async () => await Vocabulary.Create(null, cts.Token)).Result;
                Assert.Fail();
            }
            catch(AggregateException)
            {
                Assert.Pass();
            }
        }

        [Test]
        public void BagOfWordsConverterCreateErrorCase()
        {
            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(1));

            try
            {
                var converter = new BagOfWordsConverter(null);
                Assert.Fail();
            }
            catch (ArgumentException)
            {
                Assert.Pass();
            }
        }

        [Test]
        public void BagOfWordsConverterConvertErrorCase()
        {
            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(1));

            var sentences = new[]
            {
                "私はラーメンが好きです。",
                "私は餃子が好きです。",
                "私はラーメンが嫌いです。"
            };

            BagOfWordsConverter.InitAnalyzer();
            var vocabulary = Task.Run(async () => await Vocabulary.Create(sentences, cts.Token)).Result;
            var converter = new BagOfWordsConverter(vocabulary);

            try
            {
                var bowVec = Task.Run(async () => await converter.ConvertAsync(null, cts.Token)).Result;
                Assert.Fail();
            }
            catch (AggregateException)
            {
                Assert.Pass();
            }
        }
    }
}
