#if UNITY_PURCHASING
using DuckLib.Purchasing.InApp;
using DuckLib.Purchasing.InApp.Commands;
using UnityEngine.Purchasing;

namespace DuckLib.Purchasing.UnityInApp.Commands
{
    public class PurchaseCommand : InAppPurchaseCommand
    {
        protected override void OnStart()
        {
            Result.ProductType = Args.InAppProductType;
            if (Controller is UnityInAppController unityInAppController && unityInAppController.IsInitialized())
            {
                var product = unityInAppController.StoreController.products.WithID(Args.Id);

                if (product != null && product.hasReceipt && (product.definition.type == ProductType.NonConsumable || product.definition.type == ProductType.Subscription))
                {
                    FinishCommandWithResult(true);
                }
                else if (product != null && product.availableToPurchase)
                {
                    unityInAppController.PurchaseSuccess += OnPurchaseSuccess;
                    unityInAppController.PurchaseFailed += OnPurchaseFailed;
                    unityInAppController.StoreController.InitiatePurchase(product);
                }
                else
                {
                    Result.FailReason = "Purchase failed, Product is not available to purchase";
                    FinishCommandWithResult(false);
                }
            }
            else
            {
                Result.FailReason = "Purchase failed, InAppController not initialized";
                FinishCommandWithResult(false);
            }
        }

        private void FinishCommandWithResult(bool result)
        {
            FinishCommand(result, Result);
        }

        protected override void OnReleaseResources()
        {
            base.OnReleaseResources();
            ((UnityInAppController) Controller).PurchaseSuccess -= OnPurchaseSuccess;
            ((UnityInAppController) Controller).PurchaseFailed -= OnPurchaseFailed;
        }

        private void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
        {
            Result.FailReason = failureReason.ToString();
            FinishCommandWithResult(false);
        }

        private void OnPurchaseSuccess(PurchaseEventArgs obj)
        {
            FinishCommandWithResult(Args.Id == obj.purchasedProduct.definition.id);
        }


        public PurchaseCommand(IInAppController controller) : base(controller)
        {
        }
    }
}
#endif