using System.Collections.Generic;
using Distributor.BLL.DTO;

namespace Distributor.BLL.Interfaces
{
    public interface IStudentService
    {
        IEnumerable<StudentDTO> GetAll();
        StudentDTO GetById(int id);
        void Create(StudentDTO student);
        void Edit(StudentDTO student);
        void Delete(int id);
    }
}
