//Author Minh Nguyen
//description: methods of modify and query of Department table
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace HelpdeskDAL
{
    public class DepartmentModel
    {
        IRepository<Departments> repository;
        public DepartmentModel()
        {
            repository = new HelpdeskRepository<Departments>();
        }
        public Departments GetByDepartmentName(string DepartmentName)
        {
            List<Departments> selectedDep = null;
            try
            {
                selectedDep = repository.GetByExpression(dep => dep.DepartmentName == DepartmentName);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return selectedDep.FirstOrDefault();
        }
        public Departments GetById(int id)
        {
            List<Departments> selectedDep = null;
            try
            {
                selectedDep = repository.GetByExpression(dep => dep.Id == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return selectedDep.FirstOrDefault();
        }
        public List<Departments> GetAll()
        {
            List<Departments> allDep = new List<Departments>();
            try
            {
                allDep = repository.GetAll();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return allDep;
        }
        public int ADD(Departments newDep)
        {
            try
            {
                return repository.Add(newDep).Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
        }
        public UpdateStatus Update(Departments updatedDep)
        {
            UpdateStatus operationStatus = UpdateStatus.Failed;
            try
            {
                operationStatus = repository.Update(updatedDep);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return operationStatus;
        }
        public int Delete(int id)
        {
            try
            {
                return repository.Delete(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
        }
    }
}
