#if UNITY_PURCHASING
using System;
using DuckLib.Core.Commands;
using DuckLib.Purchasing.InApp;
using UniRx;

#if UNITY_IOS
using UnityEngine.Purchasing;
#endif

namespace DuckLib.Purchasing.UnityInApp.Commands
{
    public sealed class RestorePurchaseCommand : ICommand<bool>
    {
        private readonly IInAppController _controller;

        public RestorePurchaseCommand(IInAppController controller)
        {
            _controller = controller;
        }

        public IObservable<bool> Execute()
        {
            return Observable.Create<bool>(observer =>
            {
                var unityInAppController = (UnityInAppController) _controller;
                if (unityInAppController.IsInitialized())
                {
#if UNITY_IOS
                    unityInAppController.StoreExtensionProvider.GetExtension<IAppleExtensions>()
                        .RestoreTransactions(result =>
                        {
                            observer.OnNext(result);
                            observer.OnCompleted();
                        });
#else
                    observer.OnNext(true);
                    observer.OnCompleted();
#endif
                }
                else
                {
                    observer.OnError(new OperationCanceledException("UnityInAppController is not initialized"));
                }

                return this;
            });
        }

        public void Dispose()
        {
        }
    }
}
#endif