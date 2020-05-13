using AlienJust.Support.ModelViewViewModel;

namespace CustomModbusSlave.Es2gClimatic.Shared.AppWindow
{
    internal sealed class ParameterViewModelSimple : ViewModelBase, IParameterViewModel
    {
        public string Name { get; }

        public IParameterGetterViewModel Getter { get; }

        public IParameterSetterViewModel Setter { get; }

        public ParameterViewModelSimple(string customName, string parameterNameFromConfiguration, IParameterGetterViewModel getter, IParameterSetterViewModel setter)
        {
            Name = customName ?? parameterNameFromConfiguration;
            Getter = getter;
            Setter = setter;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    Getter.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~ParameterViewModelSimple()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}