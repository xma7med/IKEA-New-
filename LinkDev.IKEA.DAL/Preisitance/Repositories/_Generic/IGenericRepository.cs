﻿using LinkDev.IKEA.DAL.Entities;
using LinkDev.IKEA.DAL.Entities.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL.Preisitance.Repositories._Generic
{
    public interface IGenericRepository<T> where T :ModelBase
    {
        T? Get(int id);
        IEnumerable<T> GetAll(bool withAsNoTracking = true);

        IQueryable<T> GetIQueryable();
        //IEnumerable<T> GetIEnumerable();

        int Add(T entity);
        int Update(T entity);
        int Delete(T entity);
    }
}
