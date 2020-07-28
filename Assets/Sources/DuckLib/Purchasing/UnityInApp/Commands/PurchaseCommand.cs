#if UNITY_PURCHASING
using System;
using DuckLib.Core.Commands;
using DuckLib.Purchasing.InApp;
using DuckLib.Purchasing.InApp.Commands;
using UniRx;
using UnityEngine.Purchasing;

namespace DuckLib.Purchasing.UnityInApp.Commands
{
    public sealed class PurchaseCommand : ICommand<InAppPurchaseCommandArgs, InAppPurchaseCommandResult>
    {
        private readonly IInAppController _controller;

        public PurchaseCommand(IInAppController controller)
        {
            _controller = controller;
        }

        public IObservable<InAppPurchaseCommandResult> Execute(InAppPurchaseCommandArgs args)
        {
            return Observable.Create<InAppPurchaseCommandResult>(observer =>
            {
                var result = new InAppPurchaseCommandResult {ProductId = args.Id};
                if (_controller is UnityInAppController unityInAppController && unityInAppController.IsInitialized())
                {
                    var product = unityInAppController.StoreController.products.WithID(args.Id);

                    if (product != null && product.hasReceipt &&
                        (product.definition.type == ProductType.NonConsumable ||
                         product.definition.type == ProductType.Subscription))
                    {
                        observer.OnNext(result);
                        observer.OnCompleted();
                    }

                    if (product != null && product.availableToPurchase)
                    {
                        unityInAppController.PurchaseSuccess += productEvent =>
                        {
                            if (args.Id == productEvent.purchasedProduct.definition.id)
                            {
                                observer.OnNext(result);
                                observer.OnCompleted();
                            }
                            else
                            {
                                observer.OnError(new OperationCanceledException("Wrong result id"));
                                observer.OnCompleted();
                            }
                        };
                        unityInAppController.PurchaseFailed += (product1, reason) =>
                        {
                            observer.OnError(new OperationCanceledException(reason.ToString()));
                            observer.OnCompleted();
                        };
                        unityInAppController.StoreController.InitiatePurchase(product);
                    }
                    else
                    {
                        observer.OnError(
                            new OperationCanceledException("Purchase failed, Product is not available to purchase"));
                        observer.OnCompleted();
                    }
                }
                else
                {
                    observer.OnError(
                        new OperationCanceledException("Purchase failed, Product is not available to purchase"));
                    observer.OnCompleted();
                }

                return Disposable.Empty;
            });
        }
    }
}
#endif