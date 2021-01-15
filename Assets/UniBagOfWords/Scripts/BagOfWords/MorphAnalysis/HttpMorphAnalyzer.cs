﻿using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace UniBagOfWords.MorphAnalyzer
{
    /// <summary>
    /// 任意のMeCab Web APIに対応した形態素解析器
    /// </summary>
    public class HttpMorphAnalyzer : MorphAnalyzer
    {
        private static HttpClient _client = new HttpClient();
        public string Url { get; }

        public HttpMorphAnalyzer(string url)
        {
            if (string.IsNullOrEmpty(url)) throw new ArgumentException("urlが正しく設定されていません");

            Url = url;
        }

        /// <summary>
        /// 形態素解析を行う
        /// </summary>
        public override async Task<Morpheme[]> AnalyzerAsync(string sentence, CancellationToken token = default)
        {
            var url = $"{Url}?sentence={sentence}";
            Debug.Log(url);
            var response = await _client.GetAsync(url, token);

            if (!response.IsSuccessStatusCode) throw new HttpRequestException();

            var text = await response.Content.ReadAsStringAsync();

            var tokens = JsonConvert.DeserializeObject<List<MeCabResopnseToken>>(text);

            return tokens.Select(t => new Morpheme(t.surface, t.pos, t.baseform, t.reading)).ToArray();
        }

        public override void Dispose() { }
    }

    /// <summary>
    /// このクラスは以下のレスポンスにのみ対応
    /// </summary>
    /// <remarks>自分の好きなようにカスタマイズしてください</remarks>
    [Serializable]
    public struct MeCabResopnseToken
    {
        public string surface;
        public string pos;
        public string pos1;
        public string pos2;
        public string baseform;
        public string reading;
        public string pronounciation;
    }
}