using System;
using Distributor.DAL.EF;
using Distributor.DAL.Entities;

namespace Distributor.DAL.Repositories
{
    public class UnitOfWork : IDisposable
    {
        private ApplicationDbContext context = new ApplicationDbContext();
        private GenericRepository<Student> _studentRepository;
        private GenericRepository<Task> _taskRepository;
        private GenericRepository<ControlCenter> _controlCenterRepository;

        public GenericRepository<Student> studentRepository
        {
            get
            {
                if (_studentRepository == null)
                {

                    _studentRepository = new GenericRepository<Student>(context);
                }
                return _studentRepository;
            }
        }

        public GenericRepository<Task> taskRepository
        {
            get
            {
                if (_taskRepository == null)
                {

                    _taskRepository = new GenericRepository<Task>(context);
                }
                return _taskRepository;
            }
        }

        public GenericRepository<ControlCenter> controlCenterRepository
        {
            get
            {
                if (_controlCenterRepository == null)
                {

                    _controlCenterRepository = new GenericRepository<ControlCenter>(context);
                }
                return _controlCenterRepository;
            }
        }

        public void Save()
        {
            this.context.SaveChanges();
        }

        private bool disposing = false;
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposing)
            {
                if (disposing)
                {
                    context.Dispose();
                }
                this.disposing = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}