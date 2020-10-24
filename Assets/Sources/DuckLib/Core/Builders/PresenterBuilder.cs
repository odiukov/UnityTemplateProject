using DuckLib.Core.Presenters;
using Zenject;

namespace DuckLib.Core.Builders
{
    public class PresenterBuilder
    {
        private readonly DiContainer _container;

        public PresenterBuilder(DiContainer container)
        {
            _container = container;
        }

        public TPresenter Build<TPresenter, TView>(TView view) where TPresenter : BasePresenter<TView>
            where TView : class
        {
            var presenter = _container.Instantiate<TPresenter>(new []{view});
            return presenter;
        }
    }
}