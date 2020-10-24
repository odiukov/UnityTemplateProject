using System;

namespace DuckLib.Core.Extensions
{
    public static class ObservableExtensions
    {
        public static void OnNextAndComplete<T>(this IObserver<T> observer, T value)
        {
            observer.OnNext(value);
            observer.OnCompleted();
        }
    }
}