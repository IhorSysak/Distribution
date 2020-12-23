using System.Collections.Generic;
using Distributor.BLL.DTO;

namespace Distributor.BLL.Interfaces
{
    public interface ITaskService
    {
        IEnumerable<TaskDTO> GetAll();
        TaskDTO GetById(int id);
        void Create(TaskDTO task);
        void Edit(TaskDTO task);
        void Delete(int id);
    }
}
