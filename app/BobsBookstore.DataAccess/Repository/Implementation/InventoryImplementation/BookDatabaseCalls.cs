﻿using System;
using System.Linq;
using System.Linq.Expressions;
using BobsBookstore.DataAccess.Data;
using BobsBookstore.DataAccess.Repository.Interface.InventoryInterface;
using BobsBookstore.Models.Books;
using BookstoreBackend.Database;

namespace BobsBookstore.DataAccess.Repository.Implementation.InventoryImplementation
{
    public class BookDatabaseCalls : IBookDatabaseCalls
    {
        private readonly ApplicationDbContext _context;

        public BookDatabaseCalls(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable GetBaseQuery(string objPath)
        {
            return _context.Query(objPath);
        }

        public IQueryable<Price> ReturnBasePriceQuery(IQueryable query, string[] includes)
        {
            var result = (IQueryable<Price>)query;
            result = result.Include(includes);

            return result;
        }

        public IQueryable<Price> ReturnFilterPriceQuery(IQueryable<Price> query, Expression<Func<Price, bool>> lambda)
        {
            return query.Where(lambda);
        }

        public IQueryable<Book> ReturnBaseBookQuery(IQueryable query, string[] includes)
        {
            var result = (IQueryable<Book>)query;
            result = result.Include(includes);

            return result;
        }

        public IQueryable<Book> ReturnFilterBookQuery(IQueryable<Book> query, Expression<Func<Book, bool>> lambda)
        {
            return query.Where(lambda);
        }

        public IQueryable<Condition> ReturnBaseConditionQuery(IQueryable query, string[] includes)
        {
            var result = (IQueryable<Condition>)query;
            result = result.Include(includes);

            return result;
        }

        public IQueryable<Condition> ReturnFilterConditionQuery(IQueryable<Condition> query,
            Expression<Func<Condition, bool>> lambda)
        {
            return query.Where(lambda);
        }
    }
}