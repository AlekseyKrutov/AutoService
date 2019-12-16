using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapper
{
    public interface IDbMapperCommand<T>
    {
        T Get(string id);
        void Insert();
        void Update();
        void Delete();
    }
}
