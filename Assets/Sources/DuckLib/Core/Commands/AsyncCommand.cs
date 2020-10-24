using System;
using Cysharp.Threading.Tasks;
using UniRx;

namespace DuckLib.Core.Commands
{
    public abstract class AsyncCommand : ICommand
    {
        protected abstract UniTask ExecuteInternal();

        public IObservable<Unit> Execute(Unit args = default)
        {
            return Observable.Create<Unit>(observer =>
            {
                var disposable = ExecuteInternal()
                    .ToObservable()
                    .ContinueWith(Observable.ReturnUnit())
                    .Subscribe(observer);
                return disposable;
            });
        }
    }
}