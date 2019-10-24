//Author Minh Nguyen
//description: provide data and functionality for Department with out access context class
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using HelpdeskDAL;

namespace HelpdeskViewModels
{
    public class DepartmentViewModel
    {
        private DepartmentModel _model;
        public string Timer { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }

        public DepartmentViewModel()
        {
            _model = new DepartmentModel();
        }
        public void GetByEmail()
        {
            try
            {
                Departments dep = _model.GetByDepartmentName(DepartmentName);
                DepartmentId = dep.Id;
                DepartmentName = dep.DepartmentName;
                Timer = Convert.ToBase64String(dep.Timer);
            }
            catch (NullReferenceException)
            {
                DepartmentName = "not found";
            }
            catch (Exception ex)
            {
                DepartmentName = "not found";
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
        }
        public void GetById()
        {
            try
            {
                Departments dep = _model.GetById(DepartmentId);
                DepartmentId = dep.Id;
                DepartmentName = dep.DepartmentName;
                Timer = Convert.ToBase64String(dep.Timer);
            }
            catch (NullReferenceException)
            {
                DepartmentName = "not found";
            }
            catch (Exception ex)
            {
                DepartmentName = "not found";
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
        }
        public List<DepartmentViewModel> GetAll()
        {
            List<DepartmentViewModel> allVmd = new List<DepartmentViewModel>();
            try
            {
                List<Departments> allDep = _model.GetAll();
                foreach (Departments dep in allDep)
                {
                    DepartmentViewModel depVm = new DepartmentViewModel();
                    depVm.DepartmentId = dep.Id;
                    depVm.DepartmentName = dep.DepartmentName;
                    depVm.Timer = Convert.ToBase64String(dep.Timer);
                    allVmd.Add(depVm);
                }
            }
            catch (Exception ex)
            {
                DepartmentName = "not found";
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return allVmd;
        }
        public void Add()
        {
            DepartmentId = -1;
            try
            {
                Departments dep = new Departments();
                dep.DepartmentName = DepartmentName;
                DepartmentId = _model.ADD(dep);
            }
            catch (Exception ex)
            {
                DepartmentName = "not found";
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
        }
        public int Update()
        {
            UpdateStatus operationStatus = UpdateStatus.Failed;
            try
            {
                Departments dep = new Departments();
                dep.Id = DepartmentId;
                dep.DepartmentName = DepartmentName;               
                
                dep.Timer = Convert.FromBase64String(Timer);

                operationStatus = _model.Update(dep);
            }
            catch (Exception ex)
            {
                DepartmentName = "not found";
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return (int)operationStatus;
        }
        public int Delete()
        {
            int depDeleted = -1;
            try
            {
                depDeleted = _model.Delete(DepartmentId);
            }
            catch (Exception ex)
            {
                DepartmentName = "not found";
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return depDeleted;
        }
    }
}

