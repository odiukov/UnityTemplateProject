using System;

namespace DuckLib.Purchasing.InApp
{
    public interface IInAppController
    {
        string GetPriceString(InAppProductType productType);

        string GetLocalizedPriceString(InAppProductType productType);

        bool IsInitialized();

        bool IsPurchased(InAppProductType productType);

        void MakePurchase(InAppProductType productType, Action<InAppPurchaseCommandResult> callback);

        void RestorePurchases(Action<bool> callback);
    }
}