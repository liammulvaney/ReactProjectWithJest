using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryPattern.Interface
{
    public interface IUnitOfWork: IDisposable
    {
        IRepository _Repository
        {
            get;
            set;
        }
    }
}
