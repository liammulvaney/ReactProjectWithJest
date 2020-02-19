using RepositoryPattern.Interface;
using System;
using System.Runtime.InteropServices;

namespace RepositoryPattern.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        public IRepository _Repository { get; set; }
        private bool isDisposed;
        private IntPtr nativeResource = Marshal.AllocHGlobal(100);


        public UnitOfWork(IRepository Repository)
        {
            _Repository = Repository;
        }

       
        // Dispose() calls Dispose(true)
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // The bulk of the clean-up code is implemented in Dispose(bool)
        protected virtual void Dispose(bool disposing)
        {
            if (isDisposed) return;

            if (disposing)
            {
                // free managed resources
                _Repository = null;
            }

            // free native resources if there are any.
            if (nativeResource != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(nativeResource);
                nativeResource = IntPtr.Zero;
            }

            isDisposed = true;
        }

        // NOTE: Leave out the finalizer altogether if this class doesn't
        // own unmanaged resources, but leave the other methods
        // exactly as they are.
        ~UnitOfWork()
        {
            // Finalizer calls Dispose(false)
            Dispose(false);
        }
    }
}
