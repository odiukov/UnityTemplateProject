using System;
using UniRx;

namespace DuckLib.Core.Commands
{
    public interface ICommand<out TResult, in TArgs>
    {
        IObservable<TResult> Execute(TArgs args = default);
    }

    public interface ICommand<out TResult> : ICommand<TResult, Unit>
    {
    }

    public interface ICommand : ICommand<Unit>
    {
    }
}