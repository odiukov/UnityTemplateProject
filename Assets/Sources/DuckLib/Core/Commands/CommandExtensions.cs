using System;
using DuckLib.Core.Extensions;
using UniRx;
using Zenject;

namespace DuckLib.Core.Commands
{
    public static class CommandExtensions
    {
        public static DiContainer Container { get; set; }

        public static IObservable<TResult> ContinueWith<TResult, TCommand>(this IObservable<Unit> source)
            where TCommand : ICommand<TResult, Unit>
        {
            return source.ContinueWith(Container.Instantiate<TCommand>().Execute);
        }

        public static IObservable<TResult> ContinueWith<TResult, TArgs, TCommand>(this IObservable<TArgs> source)
            where TCommand : ICommand<TResult, TArgs>
        {
            return source.ContinueWith(Container.Instantiate<TCommand>().Execute);
        }

        public static IObservable<Unit> ContinueWith<TCommand>(this IObservable<Unit> source)
            where TCommand : ICommand<Unit, Unit>
        {
            return source.ContinueWith(Container.Instantiate<TCommand>().Execute);
        }


        public static IObservable<Unit> ToCommand<TSignal, TCommand>(this BindSignalIdToBinder<TSignal> builder)
            where TCommand : ICommand
        {
            return Observable.Create<Unit>(observer =>
            {
                builder.ToMethod(() =>
                {
                    Container.Instantiate<TCommand>()
                        .Execute()
                        .Subscribe(result =>
                        {
                            observer.OnNext(result);
                            observer.OnCompleted();
                        });
                });
                return Disposable.Empty;
            });
        }

        public static IObservable<Unit> ToCommandWithSignalArgs<TSignal>(
            this BindSignalIdToBinder<TSignal> builder, ICommand<Unit, TSignal> command)
        {
            return Observable.Create<Unit>(observer =>
            {
                builder.ToMethod(signal =>
                {
                    command
                        .Execute(signal)
                        .Subscribe(result =>
                        {
                            observer.OnNext(result);
                            observer.OnCompleted();
                        });
                });
                return Disposable.Empty;
            });
        }


        public static IObservable<TResult> ContinueWith<TResult>(this IObservable<Unit> source,
            ICommand<TResult, Unit> command)
        {
            Container.Inject(command);
            return source.ContinueWith(command.Execute);
        }

        public static IObservable<TResult> ContinueWith<TResult, TArgs>(this IObservable<TArgs> source,
            ICommand<TResult, TArgs> command)
        {
            Container.Inject(command);
            return source.ContinueWith(command.Execute);
        }

        public static IObservable<Unit> ContinueWith(this IObservable<Unit> source, ICommand<Unit, Unit> command)
        {
            Container.Inject(command);
            return source.ContinueWith(command.Execute);
        }
    }
}