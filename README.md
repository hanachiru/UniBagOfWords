# UniBagOfWords
Unity用の形態素解析器付きBagOfWords変換ライブラリ  
![Demo](https://user-images.githubusercontent.com/46705432/102689875-ec9f3c00-4244-11eb-9b16-c82edb7565f9.gif)

## Dependency
Unity 2019.4.0f1(以上)  
[Newtonsoft.Json v12.0.3](https://github.com/JamesNK/Newtonsoft.Json/)  
[NMeCab](https://github.com/komutan/NMeCab)

## Setup
[Releaseページ]()から`.unitypackage`をインストールする

## Usage
```cs:sample.cs
// 使用例
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
    Vocabulary vocabraryu = await Vocabulary.Create(sentences);

    // 2. 先程生成した単語辞書を使ったBoWベクトル変換器を作ります
    BagOfWordsConverter converter = new BagOfWordsConverter(vocabraryu);

    // 3. 好きな文章をBoW変換します
    int[] bowVec = await converter.ConvertAsync("私はラーメンが嫌いです。");

    // BoWベクトル（テキスト中に単語が出現した回数を並べたもの）
    // 1,1,1,1,0,1,1,0,1
    Debug.Log(string.Join(",", bowVec));

    // 単語辞書の単語とIDの対応
    // 私,は,ラーメン,が,好き,です,。,餃子,嫌い
    Debug.Log(string.Join(",", vocabraryu.Words));
}
```

## Note
UniBagOfWordsフォルダをルートから移動する場合は，UniBagOfWords/Resources/MorphAnalysisSettingにあるNMeCabSetting/DicDirを任意のパスに設定してください。

## License
This software is released under the MIT License, see LICENSE.

## Authors
Hanachiru([@hanaaaaaachiru](https://twitter.com/hanaaaaaachiru))
