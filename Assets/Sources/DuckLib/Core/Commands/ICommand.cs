using System;

namespace DuckLib.Core.Commands
{
    public interface ICommand<in TArgs, out TResult> : IDisposable
    {
        IObservable<TResult> Execute(TArgs args);
    }

    public interface ICommand<out TResult> : IDisposable
    {
        IObservable<TResult> Execute();
    }

    public interface ICommandWithArgs<in TArgs> : IDisposable
    {
        void Execute(TArgs args);
    }
}