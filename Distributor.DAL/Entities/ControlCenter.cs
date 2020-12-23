using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Distributor.DAL.Entities
{
    public enum ProgressTask
    {
        A, B, C, D, F
    }

    public class ControlCenter
    {
        private int id;
        private int taskID;
        private string taskName;
        private int studentID;
        private string studentName;
        private string status;
        private string loadOfWork;
        private ProgressTask progress;


        [Key]
        public int ControlCenterID { get { return id; } set { id = value; } }
        [ForeignKey("Task")]
        [Required(ErrorMessage = "Title is empty")]
        public int TaskID { get { return taskID; } set { taskID = value; } }
        [MaxLength(20, ErrorMessage = "Lenght can`t be more than 20")]
        [MinLength(2, ErrorMessage = "Lenght can`t be less than 2")]
        [Required(ErrorMessage = "Title is empty")]
        public string TaskName { get { return taskName; } set { taskName = value; } }
        [ForeignKey("Student")]
        [Required(ErrorMessage = "Title is empty")]
        public int StudentID { get { return studentID; } set { studentID = value; } }
        [MaxLength(20, ErrorMessage = "Lenght can`t be more than 20")]
        [MinLength(2, ErrorMessage = "Lenght can`t be less than 2")]
        [Required(ErrorMessage = "Title is empty")]
        public string StudentName { get { return studentName; } set { studentName = value; } }
        [MaxLength(20, ErrorMessage = "Lenght can`t be more than 20")]
        [MinLength(2, ErrorMessage = "Lenght can`t be less than 2")]
        [Required(ErrorMessage = "Status is empty")]
        public string Status { get { return status; } set { status = value; } }
        [DisplayFormat(NullDisplayText = "No Progress")]
        public ProgressTask? ProgressTask { get { return progress; } set { progress = (ProgressTask)value; } }
        public string LoadOfWork { get { return loadOfWork; } set { loadOfWork = value; } }
        public Task Task { get; set; }
        public Student Student { get; set; }
    }
}
