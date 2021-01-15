using System.IO;
using System.Linq;
using NMeCab.Specialized;
using NUnit.Framework;

namespace Tests
{
    public class NMecabTest
    {
        private MeCabIpaDicTagger _tagger;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            // NOTE : フォルダ移動の可能性があるので，"UniBagOfWords"のパスから見つける
            var topDirectoryPath = Directory.GetDirectories("Assets", "*", SearchOption.AllDirectories)
                .FirstOrDefault(path => Path.GetFileName(path) == "UniBagOfWords");
            var dicDir = $"{topDirectoryPath}/Scripts/dic/ipadic";

            _tagger = MeCabIpaDicTagger.Create(dicDir);
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _tagger.Dispose();
        }

        [Test]
        public void NMecabTestSimplePasses()
        {
            var sentence = "初めてUnityで形態素解析する";

            var nodes = _tagger.Parse(sentence);

            var expectedSurfaces = new[] { "初めて", "Unity", "で", "形態素", "解析", "する" };
            var expectedOriginalForm = new[] { "初めて", "*", "で", "形態素", "解析", "する" };
            var expectedPartsOfSpeech = new[] { "副詞", "名詞", "助詞", "名詞", "名詞", "動詞" };
            var expectedInflection = new[] { "*", "*", "*", "*", "*", "基本形" };
            var expectedReading = new[] { "ハジメテ", "", "デ", "ケイタイソ", "カイセキ", "スル" };

            CollectionAssert.AreEqual(expectedSurfaces, nodes.Select(t => t.Surface).ToArray());
            CollectionAssert.AreEqual(expectedOriginalForm, nodes.Select(t => t.OriginalForm).ToArray());
            CollectionAssert.AreEqual(expectedPartsOfSpeech, nodes.Select(t => t.PartsOfSpeech).ToArray());
            CollectionAssert.AreEqual(expectedInflection, nodes.Select(t => t.Inflection).ToArray());
            CollectionAssert.AreEqual(expectedReading, nodes.Select(t => t.Reading).ToArray());
        }
    }
}
