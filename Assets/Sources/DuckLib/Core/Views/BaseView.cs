using DuckLib.Core.Builders;
using DuckLib.Core.Presenters;
using UnityEngine;
using Zenject;

namespace DuckLib.Core.Views
{
    public abstract class BaseView : MonoBehaviour
    {
        [Inject] protected PresenterBuilder PresenterBuilder;
    }

    public abstract class BaseViewWithPresenter<TPresenter, TView> : BaseView
        where TPresenter : BasePresenter<TView>
        where TView : class
    {
        protected TPresenter Presenter;

        protected virtual void Start()
        {
            Presenter = PresenterBuilder.Build<TPresenter, TView>(this as TView);
            Presenter.OnStart();
        }

        protected virtual void OnDestroy()
        {
            Presenter.OnDestroy();
        }
    }
}