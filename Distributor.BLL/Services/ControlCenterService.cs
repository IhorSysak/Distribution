using AutoMapper;
using System.Collections.Generic;
using Distributor.BLL.DTO;
using Distributor.DAL.Entities;
using Distributor.DAL.Repositories;
using Distributor.BLL.Interfaces;
using Distributor.BLL.Infrastructure;
using System.Linq;
using System.Data.Entity;

namespace Distributor.BLL.Services
{
    public class ControlCenterService : IControlCenterServices
    {
        UnitOfWork UnitOfWork;
        IMapper map = new MapperConfiguration(cfg => cfg.CreateMap<ControlCenter, ControlCenterDTO>()).CreateMapper();
        public ControlCenterService()
        {
            UnitOfWork = new UnitOfWork();
        }

        public List<Task> GetListTasksNotStudent(int id)
        {
            if (id <= 0) 
            {
                throw new IncorrectId();
            }

            List<Task> listTask1 = new List<Task>();
            List<Task> listTask2 = UnitOfWork.taskRepository.GetAll().ToList();

            IEnumerable<ControlCenter> controlCenters = UnitOfWork.controlCenterRepository.GetAll().Include(r => r.Task).Where(i => i.StudentID == id);
            foreach (var item in controlCenters)
            {
                listTask1.Add(item.Task);
            }
            
            var result = listTask1.Except(listTask2).Union(listTask2.Except(listTask1)).ToList();

            return result;
        }

        public IEnumerable<ControlCenterDTO> GetAll()
        {
            var map = new MapperConfiguration(c => c.CreateMap<ControlCenter, ControlCenterDTO>()).CreateMapper();
            return map.Map<IEnumerable<ControlCenter>, List<ControlCenterDTO>>(UnitOfWork.controlCenterRepository.GetAll());
        }

        public ControlCenterDTO GetById(int id)
        {
            ControlCenter item = UnitOfWork.controlCenterRepository.GetTByid(id);
            if (id < 0)
            {
                throw new IncorrectId();
            }
            if (item == null)
            {
                throw new CantGetByIdError($"Cannot find item with  id={id}");
            }

            ControlCenterDTO controlCenter = new ControlCenterDTO
            {
                ControlCenterID = item.ControlCenterID,
                TaskName = item.TaskName,
                StudentName = item.StudentName,
                Status = item.Status,
                ProgressTask = (DTO.ProgressTask?)item.ProgressTask
            };
            return controlCenter;
        }

        public void Create(ControlCenterDTO item)
        {
            if (item.ControlCenterID < 0) 
            {
                throw new IncorrectId();
            }

            var studentName = UnitOfWork.studentRepository.GetTByid(item.StudentID).LastName;
            var taskName = UnitOfWork.taskRepository.GetTByid(item.TaskID).Title;

            if (studentName.Length <= 0)
            {
                throw new CountIsZeroError($"StudentName is empty");
            }
            if (taskName.Length <= 0)
            {
                throw new CountIsZeroError($"TaskName is empty");
            }

            ControlCenter controlCenter = new ControlCenter
            {
                TaskID = item.TaskID,
                StudentID = item.StudentID,
                StudentName = studentName,
                TaskName = taskName,
                Status = item.Status,
                ProgressTask = (DAL.Entities.ProgressTask?)item.ProgressTask
            };

            UnitOfWork.controlCenterRepository.Create(controlCenter);
            UnitOfWork.Save();
        }

        public void Edit(ControlCenterDTO item)
        {
            if (item == null) 
            {
                throw new NullableItemError();
            }
            if (item.ControlCenterID < 0) 
            {
                throw new IncorrectId();
            }

            var control = UnitOfWork.controlCenterRepository.GetTByid(item.ControlCenterID);
            var studentName = UnitOfWork.studentRepository.GetTByid(item.StudentID).LastName;
            var taskName = UnitOfWork.taskRepository.GetTByid(item.TaskID).Title;

            if (control == null) 
            {
                throw new CantGetByIdError($"Cant find Control with id = {control.ControlCenterID}");
            }
            if (studentName.Length <= 0)
            {
                throw new CountIsZeroError($"StudentName is empty");
            }
            if (taskName.Length <= 0) 
            {
                throw new CountIsZeroError($"TaskName is empty");
            }

            control.TaskID = item.TaskID;
            control.StudentID = item.StudentID;
            control.StudentName = studentName;
            control.TaskName = taskName;
            control.Status = item.Status;
            control.ProgressTask = (DAL.Entities.ProgressTask?)item.ProgressTask;


            UnitOfWork.controlCenterRepository.Update(control);
            UnitOfWork.Save();
        }

        public void Delete(int id)
        {
            if (id <= 0) 
            {
                throw new IncorrectId();    
            }
            var controlCenter = UnitOfWork.controlCenterRepository.GetTByid(id);
            if (controlCenter == null)
            {
                throw new CantGetByIdError($"NotFound Control Center {id}");
            }
            UnitOfWork.controlCenterRepository.Delete(id);
            UnitOfWork.Save();
        }
    }
}
