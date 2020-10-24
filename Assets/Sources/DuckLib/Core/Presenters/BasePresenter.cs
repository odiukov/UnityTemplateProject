namespace DuckLib.Core.Presenters
{
    public abstract class BasePresenter<TView> 
    {
        protected readonly TView View;

        protected BasePresenter(TView view)
        {
            View = view;
        }

        public abstract void OnStart();

        public abstract void OnDestroy();
    }
}