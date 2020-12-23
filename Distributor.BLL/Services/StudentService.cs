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
    public class StudentService : IStudentService
    {
        UnitOfWork UnitOfWork;
        public StudentService()
        {
            UnitOfWork = new UnitOfWork();
        }

        public IEnumerable<StudentDTO> GetAll()
        {
            var map = new MapperConfiguration(c => c.CreateMap<Student, StudentDTO>()).CreateMapper();
            return map.Map<IEnumerable<Student>, List<StudentDTO>>(UnitOfWork.studentRepository.GetAll());
        }

        public StudentDTO GetById(int id)
        {
            Student item = UnitOfWork.studentRepository.GetTByid(id);
            if (id < 0)
            {
                throw new IncorrectId();
            }
            if (item == null)
            {
                throw new CantGetByIdError($"Cant find Student with id = {id}");
            }

            StudentDTO student = new StudentDTO
            {
                ID = item.ID,
                LastName = item.LastName,
                FirstMidName = item.FirstMidName
            };
            return student;
        }

        public void Create(StudentDTO item)
        {
            if (item.ID < 0)
            {
                throw new IncorrectId();
            }
            if (item == null)
            {
                throw new NullableItemError();
            }

            Student student = new Student
            {
                ID = item.ID,
                LastName = item.LastName,
                FirstMidName = item.FirstMidName
            };

            UnitOfWork.studentRepository.Create(student);
            UnitOfWork.Save();
        }

        public void Edit(StudentDTO item)
        {
            if (item.ID <= 0)
            {
                throw new IncorrectId();
            }
            if (item == null)
            {
                throw new NullableItemError();
            }

            var student = UnitOfWork.studentRepository.GetTByid(item.ID);

            if (student == null) 
            {
                throw new CantGetByIdError($"Cant find student witn id = {item.ID}");
            }

            student.ID = item.ID;
            student.LastName = item.LastName;
            student.FirstMidName = item.FirstMidName;

            UnitOfWork.studentRepository.Update(student);
            UnitOfWork.Save();
        }

        public void Delete(int id)
        {
            var student = UnitOfWork.studentRepository.GetTByid(id);
            if (student == null)
            {
                throw new CantGetByIdError($"NotFound Student {id}");
            }
            UnitOfWork.studentRepository.Delete(id);
            UnitOfWork.Save();
        }
    }
}
