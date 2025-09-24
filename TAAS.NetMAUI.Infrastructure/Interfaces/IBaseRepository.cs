using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TAAS.NetMAUI.Core;
using TAAS.NetMAUI.Core.Entities;

namespace TAAS.NetMAUI.Infrastructure.Interfaces {
    public interface IBaseRepository<T> where T : BaseEntity {
        IQueryable<T> FindAll( bool trackChanges );
        IQueryable<T> FindByCondition( Expression<Func<T, bool>> expression, bool trackChanges );
        void Create( T entity );
        void Update( T entity );
        void Delete( T entity );
    }
}
