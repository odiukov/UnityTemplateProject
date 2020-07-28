using System;

namespace DuckLib.Core.Commands
{
    public interface ICommand<out TResult, in TArgs>
    {
        IObservable<TResult> Execute(TArgs args);
    }

    public interface ICommand<out TResult>
    {
        IObservable<TResult> Execute();
    }
}