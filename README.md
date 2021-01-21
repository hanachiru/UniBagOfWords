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
  
![Bag of Wordsの概要](https://user-images.githubusercontent.com/46705432/105355933-1bd70d80-5c36-11eb-995f-93a8114f26d8.png)
  
```cs:sample1.cs
// 使用例1
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
  
```cs:sample2.cs
// 使用例2
private async void Start()
{
    // キャンセル用のトークン生成して利用することもできます
    CancellationTokenSource cts = new CancellationTokenSource();

    string sentene = "私はラーメンが嫌いです。";

    // 形態素解析も行えます
    Morpheme[] morphemes = await MorphAnalyzerClient.AnalyzeAsync(sentene, cts.Token);

    // 私,は,ラーメン,が,嫌い,です,。
    Debug.Log(string.Join(",", morphemes.Select(morpheme => morpheme.Surface).ToArray()));

    // 自身で作成した形態素のコレクションを用いて，Vocabularyを作成することもできます(特定の品詞に限定したりはmorphemesに対して処理をすれば良い)
    Vocabulary vocabulary = new Vocabulary(morphemes);

    // 作成したVocabularyでBoWベクトルに変換します
    BagOfWordsConverter converter = new BagOfWordsConverter(vocabulary);
    int[] bowVec = await converter.ConvertAsync("私は嫌い。", cts.Token);

    // 1,1,0,0,1,0,1
    Debug.Log(string.Join(",", bowVec));
}
```
## ClassDiagram
![ClassDiagram](https://user-images.githubusercontent.com/46705432/104749276-f5c4ef80-5795-11eb-8d19-067ba16dc6cc.png)

## Note
UniBagOfWordsフォルダをルートから移動する場合は`，UniBagOfWords/Resources/MorphAnalysisSetting`にある`NMeCabSetting/DicDir`を任意のパスに設定してください。　


![Note](https://user-images.githubusercontent.com/46705432/104748864-82bb7900-5795-11eb-9322-9d8a70f29537.png)

  

またMorphAnalyzerClientの静的コンストラクタで`Resources.Load`により設定ファイルを探しています。  
`Vocabulary.Create`, `BagOfWordsConverter.ConvertAsync`, `MorphAnalyzerClient.AnalyzeAsync`のいずれかを初回も別スレッドにより呼び出す場合は、あらかじめメインスレッドにより`MorphAnalyzerClient.Init()`を呼び出してください。

## License
This software is released under the MIT License, see LICENSE.

## Authors
Hanachiru([@hanaaaaaachiru](https://twitter.com/hanaaaaaachiru))
