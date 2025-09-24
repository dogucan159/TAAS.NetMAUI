using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TAAS.NetMAUI.Core;
using TAAS.NetMAUI.Core.Entities;
using TAAS.NetMAUI.Infrastructure.Data;
using TAAS.NetMAUI.Infrastructure.Interfaces;

namespace TAAS.NetMAUI.Infrastructure.Repositories {
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity {

        protected readonly TaasDbContext _context;

        public BaseRepository( TaasDbContext context ) {
            _context = context;
        }

        public void Create( T entity ) => _context.Set<T>().Add( entity );

        public void Delete( T entity ) => _context.Set<T>().Remove( entity );

        public IQueryable<T> FindAll( bool trackChanges ) =>
            !trackChanges ?
            _context.Set<T>().AsNoTracking() :
            _context.Set<T>();


        public IQueryable<T> FindByCondition( Expression<Func<T, bool>> expression,
            bool trackChanges ) =>
            !trackChanges ?
            _context.Set<T>().Where( expression ).AsNoTracking() :
            _context.Set<T>().Where( expression );

        public void Update( T entity ) => _context.Set<T>().Update( entity );
    }
}
