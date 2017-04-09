using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Entity;
using App.Domain.Repo.Abstract;
using System.Data.Entity.Core.Objects;
using System.Reflection;
using App.Utility;
namespace App.Domain.Repo.Concrete
{
    public class BaseRepository : IBaseRepository
    {
        Entities context;
        ObjectParameter retMsg;
        ObjectParameter retValue;
        IAppLogManager al;
        public BaseRepository()
        {
            context = new Entities();
            retMsg = new ObjectParameter("RetMsg", "");
            retValue = new ObjectParameter("RetVal", 0);
            al = new AppLogManager();
        }

        public IEnumerable<Text> GetText(out ErrorObject err, Text text)
        {
            err = new ErrorObject();
            IEnumerable<Text> textList = new List<Text>();
            try
            {
                textList = context.AppTestGet(text.ID, retValue, retMsg).Select(a => new Text { ID = a.TextID, Name = a.Name }).ToList();
                err.ErrID = int.Parse(retValue.Value.ToString());
                err.ErrMsg = retMsg.Value.ToString();
            }
            catch(Exception e)
            {
                err.ErrID = -99999;
                err.ErrMsg = "Domain error during" + string.Format("{0}.{1}", MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name);
                al.WriteLog(e);
            }
            return textList;
        }
        //public int UpdateArticle(out ErrorObject err, DefaultArticle ar)
        //{
        //    var retMsg = new ObjectParameter("retMsg", "");
        //    var retValue = new ObjectParameter("retValue", 0);
        //    context.usp_T101_Article_Update(ar.CommandId, ar.ContentId, ar.Subject, ar.ArticleBody, ar.Type, ar.Status, ar.Rank, ar.Price, ar.Discount, ar.OpenDT, ar.CloseDT, null, null, null, null, null, null, null, null, null, null, null, retValue, retMsg);
        //    err = new ErrorObject();
        //    err.ErrId = int.Parse(retValue.Value.ToString());
        //    err.ErrMsg = retMsg.Value.ToString();
        //    return int.Parse(retValue.Value.ToString());
        //}

    }
}
