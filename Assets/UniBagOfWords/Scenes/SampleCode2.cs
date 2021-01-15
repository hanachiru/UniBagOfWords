using UniBagOfWords;
using UnityEngine;
using UnityEngine.UI;

public class SampleCode2 : MonoBehaviour
{
    [SerializeField] private Text _text;
    [SerializeField] private InputField _inputField;

    private BagOfWordsConverter _converter;

    private async void Start()
    {
        // テキストデータセット
        string[] sentences = new[]
        {
            "私はラーメンが好きです。",
            "私は餃子が好きです。",
            "私はラーメンが嫌いです。"
        };

        // 1. 単語辞書を作成
        Vocabulary vocabraryu = await Vocabulary.Create(sentences);

        // 2. 生成した単語辞書を使ったBoWベクトル変換器を作成
        _converter = new BagOfWordsConverter(vocabraryu);
    }

    public async void Execute()
    {
        // 3. 好きな文章をBoW変換します
        int[] bowVec = await _converter.ConvertAsync(_inputField.text);

        Debug.Log("[" + string.Join(",", bowVec) + "]");
        _text.text = "[" + string.Join(",", bowVec) + "]";
    }
}
