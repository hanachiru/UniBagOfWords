# UniBagOfWords
Unity用の形態素解析器付きBagOfWordsベクトル変換ライブラリ  
![Demo](https://user-images.githubusercontent.com/46705432/104748490-27898680-5795-11eb-98e9-92a05e2ec5ac.gif)

## Dependency
Unity 2019.4.0f1(以上)  
[Newtonsoft.Json v12.0.3](https://github.com/JamesNK/Newtonsoft.Json/)  
[NMeCab v0.10.1](https://github.com/komutan/NMeCab)

## Setup
[Releaseページ](https://github.com/hanachiru/UniBagOfWords/releases/)から`.unitypackage`をインストールする

## Usage
Bog of Wordsについては[こちらの記事](https://qiita.com/kazukiii/items/d717add45bbc76a71430)が参考になると思います。

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
```
## ClassDiagram
![ClassDiagram](https://user-images.githubusercontent.com/46705432/104749276-f5c4ef80-5795-11eb-8d19-067ba16dc6cc.png)

## Note
UniBagOfWordsフォルダをルートから移動する場合は，UniBagOfWords/Resources/MorphAnalysisSettingにあるNMeCabSetting/DicDirを任意のパスに設定してください。　


![Note](https://user-images.githubusercontent.com/46705432/104748864-82bb7900-5795-11eb-9322-9d8a70f29537.png)

## License
This software is released under the MIT License, see LICENSE.

## Authors
Hanachiru([@hanaaaaaachiru](https://twitter.com/hanaaaaaachiru))
