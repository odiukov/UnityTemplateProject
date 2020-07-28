using System;

namespace DuckLib.Core.Commands
{
    public interface ICommand<in TArgs, out TResult>
    {
        IObservable<TResult> Execute(TArgs args);
    }

    public interface ICommand<out TResult>
    {
        IObservable<TResult> Execute();
    }
}