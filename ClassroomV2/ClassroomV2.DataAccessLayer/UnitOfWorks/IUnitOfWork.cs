using System;


namespace ClassroomV2.DataAccessLayer.UnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        void Save();
    }
}
