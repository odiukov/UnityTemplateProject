using System;
using UniRx;
using Zenject;

namespace DuckLib.Core.Commands
{
    public class CommandExecutor
    {
        private readonly DiContainer _container;

        public CommandExecutor(DiContainer container)
        {
            _container = container;
        }

        public IObservable<TResult> Execute<TResult, TCommand>()
            where TCommand : ICommand<TResult, Unit>
        {
            return _container.Instantiate<TCommand>().Execute();
        }

        public IObservable<TResult> Execute<TResult, TArgs, TCommand>(TArgs args = default)
            where TCommand : ICommand<TResult, TArgs>
        {
            return _container.Instantiate<TCommand>().Execute(args);
        }

        public IObservable<Unit> Execute<TArgs, TCommand>(TArgs args)
            where TCommand : ICommand<Unit, TArgs>
        {
            return _container.Instantiate<TCommand>().Execute(args);
        }

        public IObservable<Unit> Execute<TCommand>()
            where TCommand : ICommand<Unit, Unit>
        {
            return _container.Instantiate<TCommand>().Execute();
        }

        public IObservable<TResult> Execute<TResult>(ICommand<TResult, Unit> command)
        {
            _container.Inject(command);
            return command.Execute();
        }

        public IObservable<TResult> Execute<TResult, TArgs>(ICommand<TResult, TArgs> command, TArgs args)
        {
            _container.Inject(command);
            return command.Execute(args);
        }

        public IObservable<Unit> Execute(ICommand<Unit, Unit> command)
        {
            _container.Inject(command);
            return command.Execute();
        }
    }
}