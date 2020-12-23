using Distributor.BLL.DTO;
using System.Web.Mvc;

namespace Distributor.WEB.Models
{
    public class ControlCenterModel
    {
        public int ControlCenterID { get; set; }
        public int TaskID { get; set; }
        public string TaskName { get; set; }
        public int StudentID { get; set; }
        public string StudentName { get; set; }
        public TaskDTO TaskDTO { get; set; }
        public StudentDTO StudentDTO { get; set; }
        public SelectList StudentList { get; set; }
        public SelectList TaskList { get; set; }
    }
}