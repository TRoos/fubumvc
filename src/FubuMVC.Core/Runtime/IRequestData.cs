using System;

namespace FubuMVC.Core.Runtime
{
    public interface IRequestData
    {
        object Value(string key);
        bool Value(string key, Action<object> callback);
    }

    public class PrefixedRequestData : IRequestData
    {
        private readonly IRequestData _inner;
        private readonly string _prefix;

        public PrefixedRequestData(IRequestData inner, string prefix)
        {
            _inner = inner;
            _prefix = prefix;
        }

        public object Value(string key)
        {
            return _inner.Value(_prefix + key);
        }

        public bool Value(string key, Action<object> callback)
        {
            return _inner.Value(_prefix + key, callback);
        }
    }
}