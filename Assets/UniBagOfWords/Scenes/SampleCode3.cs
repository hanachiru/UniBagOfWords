using System.Threading.Tasks;
using UniBagOfWords;
using UniBagOfWords.MorphAnalyzer;
using UnityEngine;

public class SampleCode3 : MonoBehaviour
{
    private async void Start()
    {
        // テキストデータセット
        string[] sentences = new[]
        {
            "私はラーメンが好きです。",
            "私は餃子が好きです。",
            "私はラーメンが嫌いです。"
        };

        // Vocabulary.Create もしくは BagOfWordsConverter もくしは MorphAnalyzerClient.AnalyzeAsync　を初回時別スレッドで呼ぶ場合
        // メインスレッドにてMirphAnalyzerClient.Initを呼んでください (静的コンストラクタで設定ファイルをResources.Loadするため)
        MorphAnalyzerClient.Init();

        int[] bowVec = await Task.Run(async () =>
        {
            // 自身で形態素解析を行い，その結果を用いてVacabularyを生成することもできます。
            Morpheme[] morphemes = await MorphAnalyzerClient.AnalyzeAsync(string.Join("", sentences));
            Vocabulary vocabulary = new Vocabulary(morphemes);

            BagOfWordsConverter converter = new BagOfWordsConverter(vocabulary);
            return await converter.ConvertAsync("私はラーメンが嫌いです。");
        });

        // 1,1,1,1,0,1,1,0,1
        Debug.Log(string.Join(",", bowVec));
    }
}
