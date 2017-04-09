using App.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Rules.Abstract
{
    public interface IBaseRules
    {
        IEnumerable<Text> GetText(out ErrorObject err, Text text);
    }
}
