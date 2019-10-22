using System.Collections.Generic;
using System.Web;

namespace Test.MockUtil
{
    public class MockHttpSession : HttpSessionStateBase
    {
        private readonly Dictionary<string, object> _sessionDictionary = new Dictionary<string, object>();

        public override object this[string name]
        {
            get => _sessionDictionary[name];
            set => _sessionDictionary[name] = value;
        }
    }
}