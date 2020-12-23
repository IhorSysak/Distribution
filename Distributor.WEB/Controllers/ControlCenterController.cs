using Microsoft.AspNet.Identity;
using System;
using Distributor.BLL.DTO;
using System.Web.Mvc;
using Distributor.BLL.Services;
using Distributor.DAL.EF;
using Distributor.DAL.Repositories;
using System.Linq;
using Distributor.BLL.Infrastructure;
using AutoMapper;
using Distributor.DAL.Entities;

namespace Distributor.WEB.Controllers
{
    [Authorize]
    public class ControlCenterController : Controller
    {
        IMapper map = new MapperConfiguration(cfg => cfg.CreateMap<ControlCenter, ControlCenterDTO>()).CreateMapper();

        ApplicationDbContext db = new ApplicationDbContext();
        ControlCenterService controlCenterService;
        StudentService studentService;
        TaskService taskService;
        UnitOfWork unitOfWork;
        public ControlCenterController()
        {
            studentService = new StudentService();
            controlCenterService = new ControlCenterService();
            taskService = new TaskService();
            unitOfWork = new UnitOfWork();
        }

        public ActionResult ControlCenters(string studentName, string taskName, string status, string progressTask)
        {
            string Name;
            if (User.Identity.IsAuthenticated)
            {
                Name = User.Identity.GetUserId();
                var controlCenter = controlCenterService.GetAll();
                if (!String.IsNullOrEmpty(studentName))
                {
                    controlCenter = controlCenter.Where(r => r.StudentName == studentName);
                }
                if (!String.IsNullOrEmpty(taskName))
                {
                    controlCenter = controlCenter.Where(r => r.TaskName == taskName);
                }
                if (!String.IsNullOrEmpty(status))
                {
                    controlCenter = controlCenter.Where(r => r.Status == status);
                }
                if (!String.IsNullOrEmpty(progressTask))
                {
                    controlCenter = controlCenter.Where(r => r.ProgressTask.ToString() == progressTask);
                }
                return View(controlCenter);
            }
            return View("~/Views/Shared/_LoginPartial");
        }
        public ActionResult Create()
        {
            ViewBag.StudentID = new SelectList(unitOfWork.studentRepository.GetAll(), "ID", "LastName", "FirstMidName");
            ViewBag.TaskID = new SelectList(unitOfWork.taskRepository.GetAll(), "TaskID", "Title");
            return View();
        }

        [HttpPost]
        public ActionResult Create(ControlCenterDTO item)
        {
            ViewBag.StudentID = new SelectList(unitOfWork.studentRepository.GetAll(), "ID", "LastName", "FirstMidName");
            ViewBag.TaskID = new SelectList(unitOfWork.taskRepository.GetAll(), "TaskID", "Title");

            var listControlCenter = controlCenterService.GetAll();

            if (listControlCenter == null)
            {
                throw new NullableItemError();
            }

            if (listControlCenter.FirstOrDefault(x => x.StudentID == item.StudentID && x.TaskID == item.TaskID) != null)
            {
                ModelState.AddModelError("StudentID", "Already busy with this assignment");
                return View(item);
            }

            try
            {
                controlCenterService.Create(item);
                return RedirectToAction("ControlCenters");
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

        public ActionResult Edit(int id)
        {
            try
            {
                ViewBag.StudentID = new SelectList(unitOfWork.studentRepository.GetAll(), "ID", "LastName", "FirstMidName");
                ViewBag.TaskID = new SelectList(unitOfWork.taskRepository.GetAll(), "TaskID", "Title");
                var controlCenter = controlCenterService.GetById(id);
                return View(controlCenter);
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
        public ActionResult Edit(ControlCenterDTO item)
        {
            ViewBag.StudentID = new SelectList(unitOfWork.studentRepository.GetAll(), "ID", "LastName", "FirstMidName");
            ViewBag.TaskID = new SelectList(unitOfWork.taskRepository.GetAll(), "TaskID", "Title");

            var listControlCenter = controlCenterService.GetAll();
            var controlCenter = controlCenterService.GetById(item.ControlCenterID);
            
            if (listControlCenter == null)
            {
                throw new NullableItemError();
            }

            if (listControlCenter.FirstOrDefault(x => x.StudentID == item.StudentID && x.TaskID == item.TaskID) != null)
            {
                ModelState.AddModelError("StudentID", "Already busy with this assignment");
                return View(item);
            }

            try
            {
                controlCenterService.Edit(item);
                return RedirectToAction("ControlCenters");
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

        public ActionResult Delete(int id)
        {
            try
            {
                controlCenterService.Delete(id);
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

            return RedirectToAction("ControlCenters");
        }

        public ActionResult Details(int id)
        {
            try
            {
                const double maxTasks = 5;
                var controlCenter = controlCenterService.GetById(id);
                var allControllers = controlCenterService.GetAll();
                var currentStudentName = controlCenterService.GetById(id).StudentName;
                var found = 0;

                if (controlCenter == null)
                {
                    throw new CantGetByIdError($"Not found ControlCenter {id}");
                }
                if (allControllers == null)
                {
                    throw new NullableItemError();
                }
                if (currentStudentName.Length <= 0)
                {
                    throw new CountIsZeroError($"StudentName is empty");
                }

                foreach (var item in allControllers)
                {
                    found = allControllers.Where(x => x.StudentName == currentStudentName).Count();
                }

                controlCenter.LoadOfWork = ((found / maxTasks) * 100).ToString() + " %";

                return View(controlCenter);
            }
            catch (CantGetByIdError e)
            {
                MyLogger.GetInstance().Error($"{e.Errors.First()} Error code: {(int)e.Errorcode}  ", null);
                return new ContentResult { Content = e.Errors.First() };
            }
        }
    }
}
