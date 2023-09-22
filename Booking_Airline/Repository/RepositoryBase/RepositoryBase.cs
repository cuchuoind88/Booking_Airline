﻿using Booking_Airline.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Linq.Expressions;

namespace Booking_Airline.Repository.RepositoryBase
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected ApplicationDbContext RepositoryContext;
        
        public RepositoryBase (ApplicationDbContext repositoryContext)
        {
            RepositoryContext = repositoryContext;
            
        }
        public void Create(T entity)
        {
            RepositoryContext.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            RepositoryContext.Set<T>().Add(entity);
        }

        public IQueryable<T> FindAll(bool trackChanges) =>
        !trackChanges ?
        RepositoryContext.Set<T>()
        .AsNoTracking() :
        RepositoryContext.Set<T>();

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression,
          bool trackChanges) =>
          !trackChanges ?
          RepositoryContext.Set<T>()
          .Where(expression)
          .AsNoTracking() :
          RepositoryContext.Set<T>()
          .Where(expression);
        
        public void Update(T entity)
        {
            RepositoryContext.Set<T>().Add(entity);
        }
    }
}
