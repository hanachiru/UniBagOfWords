using System;

namespace UniBagOfWords.MorphAnalyzer
{
    /// <summary>
    /// 形態素
    /// </summary>
    public class Morpheme : IEquatable<Morpheme>
    {
        public string Surface { get; }
        public string Pos { get; }
        public string Originalform { get; }
        public string Reading { get; }

        public Morpheme(string surface, string pos, string originalform, string reading)
        {
            Surface = surface;
            Pos = pos;
            Originalform = originalform;
            Reading = reading;
        }

        public bool Equals(Morpheme other)
            => Surface == other.Surface;

        public override bool Equals(object obj)
            => Equals(obj as Morpheme);

        public override int GetHashCode()
            => Surface.GetHashCode();

        public static bool operator ==(Morpheme a, Morpheme b)
            => a.Equals(b);

        public static bool operator !=(Morpheme a, Morpheme b)
            => !(a == b);
    }
}