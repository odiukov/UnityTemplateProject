#if UNITY_PURCHASING
using System;
using System.Collections.Generic;
using DuckLib.Purchasing.InApp;
using DuckLib.Purchasing.InApp.Commands;
using DuckLib.Purchasing.UnityInApp.Commands;
using UnityEngine.Purchasing;
using DuckLib.Logger;

namespace DuckLib.Purchasing.UnityInApp
{
    public sealed class UnityInAppController : IStoreListener, IInAppController
    {
        public event Action<PurchaseEventArgs> PurchaseSuccess;
        public event Action<Product, PurchaseFailureReason> PurchaseFailed;

        public IStoreController StoreController;
        public IExtensionProvider StoreExtensionProvider;

        private readonly IUnityInAppConfig _unityInAppConfig;
        private readonly ILogger _logger;

        private readonly Dictionary<InAppProductKind, ProductType> _productTypeMap =
            new Dictionary<InAppProductKind, ProductType>();

        public UnityInAppController(IUnityInAppConfig config, ILogger logger)
        {
            _logger = logger;
            _unityInAppConfig = config;
            _productTypeMap.Add(InAppProductKind.Consumable, ProductType.Consumable);
            _productTypeMap.Add(InAppProductKind.NonConsumable, ProductType.NonConsumable);
            _productTypeMap.Add(InAppProductKind.Subscription, ProductType.Subscription);
            InitializeController();
        }

        private void InitializeController()
        {
            if (IsInitialized())
                return;

            var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

            foreach (var product in _unityInAppConfig.Products)
            {
                builder.AddProduct(product.Id, _productTypeMap[product.ProductKind]);
            }

            UnityPurchasing.Initialize(this, builder);
        }

        public IObservable<InAppPurchaseCommandResult> MakePurchase(InAppInfo product)
        {
            return new PurchaseCommand(this)
                .Execute(new InAppPurchaseCommandArgs
                {
                    Id = product.Id
                });
        }

        public IObservable<bool> RestorePurchases()
        {
            return new RestorePurchaseCommand(this).Execute();
        }

        public bool IsInitialized()
        {
            return StoreController != null && StoreExtensionProvider != null;
        }

        public bool IsPurchased(InAppInfo product)
        {
            return StoreController != null &&
                   StoreController.products.WithID(product.Id).hasReceipt;
        }

        public string GetPriceString(InAppInfo product)
        {
            return IsInitialized()
                ? StoreController.products.WithID(product.Id).metadata
                    .localizedPriceString
                : product.DefaultPrice;
        }

        public string GetLocalizedPriceString(InAppInfo product)
        {
            return IsInitialized()
                ? StoreController.products.WithID(product.Id).metadata
                    .localizedPriceString
                : product.DefaultPrice;
        }

        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            StoreController = controller;
            StoreExtensionProvider = extensions;
        }

        public void OnInitializeFailed(InitializationFailureReason error)
        {
            _logger.LogError(error);
        }

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e)
        {
            PurchaseSuccess?.Invoke(e);
            return PurchaseProcessingResult.Complete;
        }

        public void OnPurchaseFailed(Product i, PurchaseFailureReason p)
        {
            PurchaseFailed?.Invoke(i, p);
        }
    }
}
#endif