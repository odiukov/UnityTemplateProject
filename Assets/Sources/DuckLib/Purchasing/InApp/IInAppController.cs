using System;
using DuckLib.Purchasing.InApp.Commands;

namespace DuckLib.Purchasing.InApp
{
    public interface IInAppController
    {
        string GetPriceString(InAppInfo product);
        string GetLocalizedPriceString(InAppInfo product);
        bool IsInitialized();
        bool IsPurchased(InAppInfo product);
        IObservable<InAppPurchaseCommandResult> MakePurchase(InAppInfo product);
        IObservable<bool> RestorePurchases();
    }
}