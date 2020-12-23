using Microsoft.AspNet.Identity;
using System;
using Distributor.BLL.DTO;
using System.Linq;
using System.Web.Mvc;
using Distributor.BLL.Services;
using Distributor.BLL.Infrastructure;
using AutoMapper;
using Distributor.DAL.Entities;

namespace Distributor.WEB.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {
        IMapper map = new MapperConfiguration(cfg => cfg.CreateMap<Task, TaskDTO>()).CreateMapper();
        TaskService taskService;
        public TaskController()
        {
            taskService = new TaskService();
        }

        public ActionResult Tasks(string title, DateTime? startTask, DateTime? deadline, string status)
        {
            string Name;
            if (User.Identity.IsAuthenticated)
            {
                Name = User.Identity.GetUserId();
                var task = taskService.GetAll();
                if (!String.IsNullOrEmpty(title))
                {
                    task = task.Where(r => r.Title == title);
                }
                if (startTask != null & deadline != null)
                {
                    task = task.Where(r => r.Deadline.Date >= startTask).Where(r => r.Deadline.Date <= deadline);
                }
                if (startTask == null & deadline != null)
                {
                    task = task.Where(r => r.Deadline.Date == deadline);
                }
                if (startTask != null & deadline == null)
                {
                    task = task.Where(r => r.Deadline.Date == startTask);
                }
                return View(task);
            }
            return View("~/Views/Shared/_LoginPartial");
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(TaskDTO item)
        {
            if (ModelState.IsValid)
            {
                var listTask = taskService.GetAll();

                if (listTask == null) 
                {
                    throw new NullableItemError();
                }

                if (listTask.FirstOrDefault(x => x.Title == item.Title) != null)
                {
                    ModelState.AddModelError("Title", "Already exist");
                    return View(item);
                }

                try
                {
                    taskService.Create(item);
                    return RedirectToAction("Tasks");
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
                var task = taskService.GetById(id);
                return View(task);
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
        public ActionResult Edit(TaskDTO item)
        {
            if (ModelState.IsValid)
            {
                var listTask = taskService.GetAll();
                var currentTask = taskService.GetById(item.TaskID);

                if (listTask == null) 
                {
                    throw new NullableItemError();
                }
                if (currentTask == null) 
                {
                    throw new CantGetByIdError($"Cant find task with id = {item.TaskID}");
                }

                if (currentTask.Title == item.Title && currentTask.Deadline == item.Deadline)
                {
                    ModelState.AddModelError("Title", "Please do change");
                    return View(item);
                }
                if(listTask.FirstOrDefault(x => x.Title == item.Title && currentTask.Title != item.Title) != null)
                {
                    ModelState.AddModelError("Title", "Already exist");
                    return View(item);
                }

                try
                {
                    taskService.Edit(item);
                    return RedirectToAction("Tasks");
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
                taskService.Delete(id);
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
            return RedirectToAction("Tasks");
        }

        public ActionResult Details(int id)
        {
            try
            {
                var task = taskService.GetById(id);

                if (task == null)
                {
                    throw new CantGetByIdError($"NotFound Student {id}");
                }

                return View(task);
            }
            catch (CantGetByIdError e)
            {
                MyLogger.GetInstance().Error($"{e.Errors.First()} Error code: {(int)e.Errorcode}  ", null);
                return new ContentResult { Content = e.Errors.First() };
            }
        }
    }
}
