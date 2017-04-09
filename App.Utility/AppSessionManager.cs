using App.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace App.Utility
{
    public interface IAppSessionManager
    {
        SessionData GetSessionData();
        void SetSessionData(SessionData sessionData);

        void ClearSessionData();
    }
    public class AppSessionManager : IAppSessionManager
    {
        public IAppLogManager iAppLogManager;
        public  AppSessionManager()
        {
            iAppLogManager =  new AppLogManager();
        }
        public SessionData GetSessionData()
        {
            try
            {

                return HttpContext.Current.Session[AppManager.SESSION_USER_DATA] as SessionData;

            }
            catch (Exception ex)
            {
                iAppLogManager.WriteLog(ex);
                return new SessionData();
            }
        }

        public void SetSessionData(SessionData sessionData)
        {
            try
            {

                HttpContext.Current.Session[AppManager.SESSION_USER_DATA] = sessionData;

            }
            catch (Exception ex)
            {
                iAppLogManager.WriteLog(ex);
            }
        }

        public void ClearSessionData()
        {
            try
            {
                HttpContext.Current.Session[AppManager.SESSION_USER_DATA] = null;

            }
            catch (Exception ex)
            {
                iAppLogManager.WriteLog(ex);
            }
        }
    }

}
