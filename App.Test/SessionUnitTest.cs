using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
//using App.Utility;
using App.Entity;
using System.Web;
using System.Web.SessionState;
using System.Reflection;
using System.IO;
using App.Utility;

namespace App.Test
{
    [TestClass]
    public class SessionUnitTest
    {
        AppSessionManager appSessionManager;
        APPCacheManager aPPCacheManager;
        public SessionUnitTest()
        {
            appSessionManager = new AppSessionManager();
            aPPCacheManager = new APPCacheManager();
        }
        [TestInitialize]
        public void SessionUnitTestSetup()
        {
            // We need to setup the Current HTTP Context as follows:            

            // Step 1: Setup the HTTP Request
            var httpRequest = new HttpRequest("", "http://localhost/", "");

            // Step 2: Setup the HTTP Response
            var httpResponce = new HttpResponse(new StringWriter());

            // Step 3: Setup the Http Context
            var httpContext = new HttpContext(httpRequest, httpResponce);
            var sessionContainer =
                new HttpSessionStateContainer("id",
                                               new SessionStateItemCollection(),
                                               new HttpStaticObjectsCollection(),
                                               10,
                                               true,
                                               HttpCookieMode.AutoDetect,
                                               SessionStateMode.InProc,
                                               false);
            httpContext.Items["AspSession"] =
                typeof(HttpSessionState)
                .GetConstructor(
                                    BindingFlags.NonPublic | BindingFlags.Instance,
                                    null,
                                    CallingConventions.Standard,
                                    new[] { typeof(HttpSessionStateContainer) },
                                    null)
                .Invoke(new object[] { sessionContainer });

            // Step 4: Assign the Context
            HttpContext.Current = httpContext;
        }


        [TestMethod]
        public void SessionDataSetTest()
        {
            var sdata = new SessionData();
            sdata.testID = 99;
            appSessionManager.SetSessionData(sdata);
            var sessData = appSessionManager.GetSessionData();
            Assert.AreEqual(sessData.testID, 99);
        }

        [TestMethod]
        public void CashDataSetTest()
        {
            var rst = aPPCacheManager.GetReferenceDataListByType(ReferenceDataType.TEXT_DATA_LIST);
            Assert.AreEqual(true, rst != null && rst.GetEnumerator().MoveNext());
        }

    }
}
