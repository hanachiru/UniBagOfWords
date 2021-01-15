using System;
using UnityEngine;

namespace UniBagOfWords.MorphAnalyzer
{
    [CreateAssetMenu(fileName = nameof(MorphAnalysisSetting), menuName = "UniBagOfWords/" + nameof(MorphAnalysisSetting))]
    public sealed class MorphAnalysisSetting : ScriptableObject
    {
        [SerializeField, Header("どの形態素解析器を使うか")]
        private MorphAnalyzerOption _option;
        public MorphAnalyzerOption Option => _option;

        [SerializeField, Header("NMeCabを使う時の設定")]
        private NMeCabSetting _nMeCabSetting;
        public NMeCabSetting NMeCabSetting => _nMeCabSetting;

        [SerializeField, Header("HttpMeCabを使う時の設定")]
        private HttpMeCabSetting _httpMeCabSetting;
        public HttpMeCabSetting HttpMeCabSetting => _httpMeCabSetting;

        public static MorphAnalysisSetting Create(MorphAnalyzerOption option = MorphAnalyzerOption.NMeCab, NMeCabSetting nMeCabSetting = default, HttpMeCabSetting httpMeCabSetting = default)
        {
            return new MorphAnalysisSetting
            {
                _option = option,
                _nMeCabSetting = nMeCabSetting,
                _httpMeCabSetting = httpMeCabSetting,
            };
        }
    }

    /// <summary>
    /// 形態素解析器の種類
    /// </summary>
    public enum MorphAnalyzerOption
    {
        NMeCab,
        Http,
    }

    [Serializable]
    public struct NMeCabSetting
    {
        [SerializeField, Header("NMeCabで使用する辞書のパス(例. Assets/dic/ipadic)")]
        private string dicDir;
        public string DicDir => dicDir;

        public NMeCabSetting(string dicDir)
        {
            this.dicDir = dicDir;
        }
    }

    [Serializable]
    public struct HttpMeCabSetting
    {
        [SerializeField, Header("WebAPIのMeCabを使う時のリクエスト先(例. http://maapi.net/apis/mecapi)")]
        private string url;
        public string Url => url;

        public HttpMeCabSetting(string url)
        {
            this.url = url;
        }
    }
}