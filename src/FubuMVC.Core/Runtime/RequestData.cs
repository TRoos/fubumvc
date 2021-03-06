using System;
using System.Collections.Generic;

namespace FubuMVC.Core.Runtime
{
    public class RequestData : IRequestData
    {
        protected AggregateDictionary _dictionary;

        public RequestData(AggregateDictionary dictionary)
        {
            _dictionary = dictionary;
        }

        public object Value(string key)
        {
            object output = null;

            Value(key, val => output = val);

            return output;
        }

        public bool Value(string key, Action<object> callback)
        {
            var found = false;

            _dictionary.Value(key, (s, o) =>
            {
                found = true;
                record(key, s, o);
                callback(o);
            });

            return found;
        }

        public static RequestData ForDictionary(IDictionary<string, object> dictionary)
        {
            AggregateDictionary dict = new AggregateDictionary().AddDictionary(dictionary);
            return new RequestData(dict);
        }

        protected virtual void record(string key, RequestDataSource source, object @object)
        {
        }
    }
}