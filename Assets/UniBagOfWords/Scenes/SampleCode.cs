using UniBagOfWords;
using UnityEngine;

public class SampleCode : MonoBehaviour
{
    async void Start()
    {
        // テキストデータセット
        string[] sentences = new[]
        {
            "私はラーメンが好きです。",
            "私は餃子が好きです。",
            "私はラーメンが嫌いです。"
        };

        // 1. まずは単語辞書を作る必要があります
        Vocabulary vocabulary = await Vocabulary.Create(sentences);

        // 2. 先程生成した単語辞書を使ったBoWベクトル変換器を作ります
        BagOfWordsConverter converter = new BagOfWordsConverter(vocabulary);

        // 3. 好きな文章をBoW変換します
        int[] bowVec = await converter.ConvertAsync("私はラーメンが嫌いです。");

        // BoWベクトル（テキスト中に単語が出現した回数を並べたもの）
        // 1,1,1,1,0,1,1,0,1
        Debug.Log(string.Join(",", bowVec));

        // 単語辞書の単語と順番
        // 私,は,ラーメン,が,好き,です,。,餃子,嫌い
        Debug.Log(string.Join(",", vocabulary.Words));
    }
}

