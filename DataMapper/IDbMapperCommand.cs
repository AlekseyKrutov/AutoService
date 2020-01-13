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
        T Insert(T id);
        void Update(T id);
        void Delete(T id);
    }
}
