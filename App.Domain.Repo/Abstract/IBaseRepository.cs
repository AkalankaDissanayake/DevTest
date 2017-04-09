using App.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Repo.Abstract
{
    public interface IBaseRepository
    {
        IEnumerable<Text> GetText(out ErrorObject err, Text ar);
    }
}
