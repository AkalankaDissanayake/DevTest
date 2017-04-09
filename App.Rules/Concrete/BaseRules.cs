using App.Domain.Repo.Concrete;
using App.Entity;
using App.Rules.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Rules.Concrete
{
    public class BaseRules : IBaseRules
    {
        BaseRepository ibrepo;
        public BaseRules()
        {
            this.ibrepo = new BaseRepository();
        }
        public IEnumerable<Text> GetText(out ErrorObject err, Text text)
        {
            return ibrepo.GetText(out err, text);
        }
    }
}
