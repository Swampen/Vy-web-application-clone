using System;
using System.Collections.Generic;
using System.Web;

namespace Test.MockUtil
{
    public class MockHttpSession : HttpSessionStateBase
    {
        Dictionary<string, object> sessionDictionary = new Dictionary<string, object>();

        public override object this[string name]
        {
            get => sessionDictionary[name];
            set => sessionDictionary[name] = value;
        }
    }
}