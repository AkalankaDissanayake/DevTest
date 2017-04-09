using App.Domain;
using App.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace App.Utility
{
    public interface IAPPCacheManager
    {
        List<ReferenceData> GetReferenceDataListByType(ReferenceDataType type);
        void ClearCache(string CacheKey);
       
    }

    public class APPCacheManager:IAPPCacheManager
    {
        private const string CACHE_REFERENCE_DATA = "CACHE_REFERENCE_DATA";
        Entities context;
        ObjectParameter retMsg;
        ObjectParameter retValue;
        public APPCacheManager()
        {
            context = new Entities();
            retMsg = new ObjectParameter("RetMsg", "");
            retValue = new ObjectParameter("RetVal", 0);
        }

        private object GetObjectForKeyOf(string Key)
        {
            return HttpContext.Current.Cache[Key];

        }

        private void Insert(string key, object value, DateTime cacheTime)
        {
            var cache = GetObjectForKeyOf(key);

            if (cache == null)
                HttpContext.Current.Cache.Insert(key, value, null, cacheTime, System.Web.Caching.Cache.NoSlidingExpiration);
            else
                HttpContext.Current.Cache[key] = value;
        }
        public void Remove(string key)
        {
            HttpContext.Current.Cache.Remove(key);
        }

        public static void RemoveAll()
        {
            System.Collections.IDictionaryEnumerator CacheKeys = System.Web.HttpContext.Current.Cache.GetEnumerator();
            while (CacheKeys.MoveNext())
            {
                var currentKey = CacheKeys.Key.ToString();
                System.Web.HttpContext.Current.Cache.Remove(currentKey);
            }
        }


        public List<ReferenceData> GetReferenceDataListByType(ReferenceDataType type)
        {
            return GetReferenceData((int)type);
        }

        public void ClearCache(string CacheKey)
        {
            throw new NotImplementedException();
        }
        private List<ReferenceData> GetReferenceData(int deptDataType)
        {
            DateTime cacheExpireTime = DateTime.Now.AddDays(1);

            string cacheKey = CACHE_REFERENCE_DATA + deptDataType.ToString();

            try
            {
                var cashedData = GetObjectForKeyOf(cacheKey) as List<ReferenceData>;
                if (cashedData != null)
                {
                    return cashedData;
                }
                else
                {

                    var result = context.AppTestGet(deptDataType, retValue, retMsg).Select(a => new ReferenceData { ReferenceItemID = a.TextID, ReferenceItemData = a.Name }).ToList();

                    if ((int)retValue.Value > 0)
                    {
                        var dataFromAPI = result;
                        if (dataFromAPI != null)
                        {
                            Insert(cacheKey, dataFromAPI.ToList(), cacheExpireTime);
                            return dataFromAPI.ToList();
                        }
                    }
                    else
                    {
                        //write log
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                //write to log
                throw ex;
            }
        }

    }
    public enum ReferenceDataType
    {
        TEXT_DATA_LIST = 1,
      
    }

}
