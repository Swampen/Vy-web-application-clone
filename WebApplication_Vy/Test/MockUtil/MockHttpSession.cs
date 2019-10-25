using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Moq;

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
        
        public static ControllerContext getHttpSessionContext()
        {
            var context = new Mock<ControllerContext>();
            var session = new MockHttpSession();
            context.Setup(s => s.HttpContext.Session).Returns(session);
            return context.Object;
        }
    }
}