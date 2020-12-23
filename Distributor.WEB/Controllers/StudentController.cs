using Microsoft.AspNet.Identity;
using System;
using Distributor.BLL.DTO;
using Distributor.DAL.Entities;
using System.Linq;
using System.Web.Mvc;
using Distributor.BLL.Services;
using Distributor.BLL.Infrastructure;
using AutoMapper;

namespace Distributor.WEB.Controllers
{
    [Authorize]
    public class StudentController : Controller
    {
        IMapper map = new MapperConfiguration(cfg => cfg.CreateMap<Student, StudentDTO>()).CreateMapper();
        StudentService studentService;
        public StudentController()
        {
            studentService = new StudentService();
        }

        public ActionResult Students(string lastName)
        {
            string Name;
            if (User.Identity.IsAuthenticated)
            {
                Name = User.Identity.GetUserId();
                var student = studentService.GetAll();
                if (!String.IsNullOrEmpty(lastName))
                {
                    student = student.Where(r => r.LastName == lastName);
                }
                return View(student);
            }
            return View("~/Views/Shared/_LoginPartial");
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(StudentDTO item)
        {
            if (ModelState.IsValid)
            {
                var listStudents = studentService.GetAll();

                if (listStudents == null)
                {
                    throw new NullableItemError();
                }

                if (listStudents.FirstOrDefault(x => x.LastName == item.LastName && x.FirstMidName == item.FirstMidName) != null)
                {
                    ModelState.AddModelError("LastName", "Alredy exist");
                    return View(item);
                }

                try
                {
                    studentService.Create(item);
                    return RedirectToAction("Students");
                }
                catch (CreationError e)
                {
                    MyLogger.GetInstance().Error($"{e.Errors.First()} Error code: {(int)e.Errorcode}  ", null);
                    return new ContentResult { Content = e.Errors.First() };
                }
                catch (CountIsZeroError e)
                {
                    MyLogger.GetInstance().Error($"{e.Errors.First()} Error code: {(int)e.Errorcode}  ", null);
                    return new ContentResult { Content = e.Errors.First() };
                }
            }

            return View(item);
        }

        public ActionResult Edit(int id)
        {
            try
            {
                var student = studentService.GetById(id);
                return View(student);
            }
            catch (CantGetByIdError e)
            {
                MyLogger.GetInstance().Error($"{e.Errors.First()} Error code: {(int)e.Errorcode}  ", null);
                return new ContentResult { Content = e.Errors.First() };
            }
            catch (IncorrectId e)
            {
                MyLogger.GetInstance().Error($"{e.Errors.First()} Error code: {(int)e.Errorcode}  ", null);
                return new ContentResult { Content = e.Errors.First() };
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StudentDTO item)
        {
            if (ModelState.IsValid)
            {
                var student = studentService.GetAll();
                var currentStudent = studentService.GetById(item.ID);


                if (currentStudent == null)
                {
                    throw new CantGetByIdError($"Cant find Control with id = {currentStudent.ID}");
                }
                if (student == null) 
                {
                    throw new NullableItemError();
                }

                if (currentStudent.LastName == item.LastName && currentStudent.FirstMidName == item.FirstMidName)
                {
                    ModelState.AddModelError("LastName", "Please, do changes");
                    return View(item);
                }
                if (student.FirstOrDefault(x => x.LastName == item.LastName && x.FirstMidName == item.FirstMidName) != null) 
                {
                    ModelState.AddModelError("LastName", "Already exist");
                    return View(item);
                }

                try
                {
                    studentService.Edit(item);
                    return RedirectToAction("Students");
                }
                catch (NullableItemError e)
                {
                    MyLogger.GetInstance().Error($"{e.Errors.First()} Error code: {(int)e.Errorcode}  ", null);
                    return new ContentResult { Content = e.Errors.First() };
                }
                catch (CountIsZeroError e)
                {
                    MyLogger.GetInstance().Error($"{e.Errors.First()} Error code: {(int)e.Errorcode}  ", null);
                    return new ContentResult { Content = e.Errors.First() };
                }
            }

            return View(item);
        }

        public ActionResult Delete(int id)
        {
            try
            {
                studentService.Delete(id);
            }
            catch (CantGetByIdError e)
            {
                MyLogger.GetInstance().Error($"{e.Errors.First()} Error code: {(int)e.Errorcode}  ", null);
                return new ContentResult { Content = e.Errors.First() };
            }
            catch (IncorrectId e)
            {
                MyLogger.GetInstance().Error($"{e.Errors.First()} Error code: {(int)e.Errorcode}  ", null);
                return new ContentResult { Content = e.Errors.First() };
            }
            catch (CountIsZeroError e)
            {
                MyLogger.GetInstance().Error($"{e.Errors.First()} Error code: {(int)e.Errorcode}  ", null);
                return new ContentResult { Content = e.Errors.First() };
            }

            return RedirectToAction("Students");
        }

        public ActionResult Details(int id)
        {
            try
            {
                var student = studentService.GetById(id);

                if (student == null)
                {
                    throw new CantGetByIdError($"NotFound Student {id}");
                }

                return View(student);
            }
            catch (CantGetByIdError e)
            {
                MyLogger.GetInstance().Error($"{e.Errors.First()} Error code: {(int)e.Errorcode}  ", null);
                return new ContentResult { Content = e.Errors.First() };
            }
        }
    }
}
