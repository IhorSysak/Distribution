using System.Collections.Generic;
using Distributor.BLL.DTO;
using Distributor.DAL.Entities;

namespace Distributor.BLL.Interfaces
{
    public interface IControlCenterServices
    {
        IEnumerable<ControlCenterDTO> GetAll();
        List<Task> GetListTasksNotStudent(int id);
        ControlCenterDTO GetById(int id);
        void Create(ControlCenterDTO controlCenter);
        void Edit(ControlCenterDTO controlCenter);
        void Delete(int id);
    }
}
