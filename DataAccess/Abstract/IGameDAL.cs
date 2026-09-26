using Core.Entities.Concrete;
using Core.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IGameDAL : IRepositoryBase<Game>
    {
    }
}
