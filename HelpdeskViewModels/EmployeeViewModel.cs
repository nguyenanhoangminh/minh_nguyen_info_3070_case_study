//Author Minh Nguyen
//description: provide data and functionality for Employee with out access context class
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using HelpdeskDAL;

namespace HelpdeskViewModels
{
    public class EmployeeViewModel
    {
        private EmployeeModel _model;
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string Timer { get; set; }
        public int DepartmentId { get; set; }
        public int Id { get; set; }
        public bool IsTech { get; set; }
        public string StaffPicture64 { get; set; }
        public EmployeeViewModel()
        {
            _model = new EmployeeModel();
        }
        public void GetByEmail()
        {
            try
            {
                Employees emp = _model.GetByEmail(Email);
                Id = emp.Id;
                DepartmentId = emp.DepartmentId;
                Title = emp.Title;
                FirstName = emp.FirstName;
                LastName = emp.LastName;
                Email = emp.Email;
                PhoneNo = emp.PhoneNo;
                if (emp.StaffPicture != null)
                {
                    StaffPicture64 = Convert.ToBase64String(emp.StaffPicture);
                }
                Timer = Convert.ToBase64String(emp.Timer);
            }
            catch (NullReferenceException)
            {
                LastName = "not found";
            }
            catch (Exception ex)
            {
                LastName = "not found";
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
        }
        public void GetById()
        {
            try
            {
                Employees emp = _model.GetById(Id);
                DepartmentId = emp.DepartmentId;
                Title = emp.Title;
                FirstName = emp.FirstName;
                LastName = emp.LastName;
                Email = emp.Email;
                PhoneNo = emp.PhoneNo;
                if (emp.StaffPicture != null)
                {
                    StaffPicture64 = Convert.ToBase64String(emp.StaffPicture);
                }
                Timer = Convert.ToBase64String(emp.Timer);
            }
            catch (NullReferenceException)
            {
                LastName = "not found";
            }
            catch (Exception ex)
            {
                LastName = "not found";
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
        }
        public List<EmployeeViewModel> GetAll()
        {
            List<EmployeeViewModel> allVms = new List<EmployeeViewModel>();
            try
            {
                List<Employees> allemp = _model.GetAll();
                foreach (Employees emp in allemp)
                {
                    EmployeeViewModel empVm = new EmployeeViewModel();
                    empVm.Id = emp.Id;
                    empVm.LastName = emp.LastName;
                    empVm.DepartmentId = emp.DepartmentId;
                    empVm.Title = emp.Title;
                    empVm.FirstName = emp.FirstName;
                    empVm.LastName = emp.LastName;
                    empVm.Email = emp.Email;
                    empVm.PhoneNo = emp.PhoneNo;
                    empVm.Timer = Convert.ToBase64String(emp.Timer);
                    allVms.Add(empVm);
                }
            }
            catch (Exception ex)
            {
                LastName = "not found";
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return allVms;
        }
        public void Add()
        {
            Id = -1;
            try
            {
                Employees emp = new Employees();
                emp.LastName = LastName;
                emp.DepartmentId = DepartmentId;
                emp.Title = Title;
                emp.FirstName = FirstName;
                emp.Email = Email;
                emp.PhoneNo = PhoneNo;
                Id = _model.ADD(emp);
            }
            catch (Exception ex)
            {
                LastName = "not found";
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
        }
        public int Update()
        {
            UpdateStatus operationStatus = UpdateStatus.Failed;
            try
            {
                Employees emp = new Employees();
                emp.Id = Id;
                emp.LastName = LastName;
                emp.DepartmentId = DepartmentId;
                emp.Title = Title;
                emp.FirstName = FirstName;
                emp.Email = Email;
                emp.PhoneNo = PhoneNo;
                if (StaffPicture64 != null)
                {
                    emp.StaffPicture = Convert.FromBase64String(StaffPicture64);
                }
                emp.Timer = Convert.FromBase64String(Timer);

                operationStatus = _model.Update(emp);
            }
            catch (Exception ex)
            {
                LastName = "not found";
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return (int)operationStatus;
        }
        public int Delete()
        {
            int empdentDeleted = -1;
            try
            {
                empdentDeleted = _model.Delete(Id);
            }
            catch (Exception ex)
            {
                LastName = "not found";
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return empdentDeleted;
        }
    }
}

