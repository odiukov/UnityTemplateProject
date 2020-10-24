using System;
using UniRx;

namespace DuckLib.Core.Commands
{
    public abstract class Command : ICommand
    {
        protected abstract void ExecuteInternal();

        public IObservable<Unit> Execute(Unit args = default)
        {
            ExecuteInternal();
            return Observable.ReturnUnit();
        }
    }
}