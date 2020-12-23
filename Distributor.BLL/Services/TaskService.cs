using AutoMapper;
using System;
using System.Collections.Generic;
using Distributor.BLL.DTO;
using Distributor.DAL.Entities;
using Distributor.DAL.Repositories;
using Distributor.BLL.Interfaces;
using Distributor.BLL.Infrastructure;

namespace Distributor.BLL.Services
{
    public class TaskService : ITaskService
    {
        UnitOfWork UnitOfWork;
        public TaskService()
        {
            UnitOfWork = new UnitOfWork();
        }

        public IEnumerable<TaskDTO> GetAll()
        {
            var map = new MapperConfiguration(c => c.CreateMap<Task, TaskDTO>()).CreateMapper();
            return map.Map<IEnumerable<Task>, List<TaskDTO>>(UnitOfWork.taskRepository.GetAll());
        }

        public TaskDTO GetById(int id)
        {
            Task item = UnitOfWork.taskRepository.GetTByid(id);
            if (id < 0)
            {
                throw new IncorrectId();
            }
            if (item == null)
            {
                throw new CantGetByIdError($"Cant find Task with id = {id}");
            }

            TaskDTO task = new TaskDTO
            {
                TaskID = item.TaskID,
                Title = item.Title,
                Deadline = item.Deadline
            };
            return task;
        }

        public void Create(TaskDTO item)
        {
            if (item.TaskID < 0)
            {
                throw new IncorrectId();
            }
            if (item == null) 
            {
                throw new NullableItemError();
            }

            Task task = new Task
            {
                TaskID = item.TaskID,
                Title = item.Title,
                Deadline = DateTime.Now
            };

            UnitOfWork.taskRepository.Create(task);
            UnitOfWork.Save();
        }

        public void Edit(TaskDTO item)
        {
            if (item.TaskID < 0)
            {
                throw new IncorrectId();
            }
            if (item == null)
            {
                throw new NullableItemError();
            }

            var task = UnitOfWork.taskRepository.GetTByid(item.TaskID);

            if (task == null)
            {
                throw new CantGetByIdError($"Cant find task with id = {item.TaskID}");
            }

            task.TaskID = item.TaskID;
            task.Title = item.Title;
            task.Deadline = item.Deadline;

            UnitOfWork.taskRepository.Update(task);
            UnitOfWork.Save();
        }

        public void Delete(int id)
        {
            var task = UnitOfWork.taskRepository.GetTByid(id);

            if (task == null)
            {
                throw new CantGetByIdError($"Cant find Control with id = {id}");
            }

            UnitOfWork.taskRepository.Delete(id);
            UnitOfWork.Save();
        }
    }
}
