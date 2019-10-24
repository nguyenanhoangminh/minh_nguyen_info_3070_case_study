//Author Minh Nguyen
//description: methods of modify and query database
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;

namespace HelpdeskDAL
{
    class HelpdeskRepository<T> : IRepository<T> where T : Entity
    {
        private HelpdeskContext _db = null;
        public HelpdeskRepository(HelpdeskContext context = null)
        {
            _db = context != null ? context : new HelpdeskContext();
        }
        public List<T> GetAll()
        {
            return _db.Set<T>().ToList();
        }
        public List<T> GetByExpression(Expression<Func<T, bool>> match)
        {
            return _db.Set<T>().Where(match).ToList();
        }
        public T Add(T entity)
        {
            _db.Set<T>().Add(entity);
            _db.SaveChanges();
            return entity;
        }
        public UpdateStatus Update(T updatedEntity)
        {
            UpdateStatus operationStatus = UpdateStatus.Failed;
            try
            {
                Entity currentEntity = GetByExpression(ent => ent.Id == updatedEntity.Id).FirstOrDefault();
                _db.Entry(currentEntity).OriginalValues["Timer"] = updatedEntity.Timer;
                _db.Entry(currentEntity).CurrentValues.SetValues(updatedEntity);
                if (_db.SaveChanges() == 1)
                    operationStatus = UpdateStatus.Ok;
            }
            catch (DbUpdateConcurrencyException dbx)
            {
                operationStatus = UpdateStatus.Stale;
                Console.WriteLine("Problem in " + MethodBase.GetCurrentMethod().Name + dbx.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + MethodBase.GetCurrentMethod().Name + ex.Message);
            }
            return operationStatus;
        }
        public int Delete(int id)
        {
            T currencyEntity = GetByExpression(emt => emt.Id == id).FirstOrDefault();
            _db.Set<T>().Remove(currencyEntity);
            return _db.SaveChanges();
        }
    }
}

