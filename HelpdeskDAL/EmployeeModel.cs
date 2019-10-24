//Author Minh Nguyen
//description: methods of modify and query of employee table
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace HelpdeskDAL
{
    public class EmployeeModel
    {

        IRepository<Employees> repository;
        public EmployeeModel()
        {
            repository = new HelpdeskRepository<Employees>();
        }
        public Employees GetByEmail(string emailAddress)
        {
            List<Employees> selectedEmployee = null;
            try
            {
                selectedEmployee = repository.GetByExpression(emp => emp.Email == emailAddress);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return selectedEmployee.FirstOrDefault();
        }
        public Employees GetById(int id)
        {
            List<Employees> selectedEmployee = null;
            try
            {
                selectedEmployee = repository.GetByExpression(emp => emp.Id == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return selectedEmployee.FirstOrDefault();
        }
        public List<Employees> GetAll()
        {
            List<Employees> allempdent = new List<Employees>();
            try
            {
                allempdent = repository.GetAll();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return allempdent;
        }
        public int ADD(Employees newempdent)
        {
            try
            {
                return repository.Add(newempdent).Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
        }
        public UpdateStatus Update(Employees updatedEmp)
        {
            UpdateStatus operationStatus = UpdateStatus.Failed;
            try
            {
                operationStatus = repository.Update(updatedEmp);
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

