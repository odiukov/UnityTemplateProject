#if UNITY_PURCHASING
using System;
using System.Collections.Generic;
using DuckLib.Core;
using DuckLib.Purchasing.InApp;
using DuckLib.Purchasing.InApp.Commands;
using DuckLib.Purchasing.UnityInApp.Commands;
using UnityEngine.Purchasing;

namespace DuckLib.Purchasing.UnityInApp
{
    public class UnityInAppController : IStoreListener, IInAppController
    {
        public event Action<PurchaseEventArgs> PurchaseSuccess;
        public event Action<Product, PurchaseFailureReason> PurchaseFailed;

        public IStoreController StoreController;
        public IExtensionProvider StoreExtensionProvider;

        private readonly IUnityInAppConfig _unityInAppConfig;

        private readonly Dictionary<InAppProductKind, ProductType> _productTypeMap =
            new Dictionary<InAppProductKind, ProductType>();

        public UnityInAppController(IUnityInAppConfig config)
        {
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

            foreach (var productsByKind in _unityInAppConfig.Products)
            {
                productsByKind.Value.ForEach(productType =>
                    builder.AddProduct(_unityInAppConfig.GetProduct(productType).Id, _productTypeMap[productsByKind.Key]));
            }

            UnityPurchasing.Initialize(this, builder);
        }

        public void MakePurchase(InAppProductType productType, Action<InAppPurchaseCommandResult> callback)
        {
            new PurchaseCommand(this)
                .AddCallback(new Responder<InAppPurchaseCommandResult>(callback, OnPurchaseError))
                .SetArgs(new InAppPurchaseCommandArgs()
                {
                    InAppProductType = productType,
                    Id = _unityInAppConfig.GetProduct(productType).Id
                }).Execute();
        }

        private void OnPurchaseError(InAppPurchaseCommandResult result)
        {
            UnityEngine.Debug.LogError(result.ProductType + " " + result.FailReason);
        }

        public void RestorePurchases(Action<bool> callback)
        {
            new RestorePurchaseCommand(this)
                .AddCallback(new Responder<bool>(callback))
                .Execute();
        }

        public bool IsInitialized()
        {
            return StoreController != null && StoreExtensionProvider != null;
        }

        public bool IsPurchased(InAppProductType productType)
        {
            return StoreController != null &&
                   StoreController.products.WithID(_unityInAppConfig.GetProduct(productType).Id).hasReceipt;
        }

        public string GetPriceString(InAppProductType productType)
        {
            return IsInitialized()
                ? StoreController.products.WithID(_unityInAppConfig.GetProduct(productType).Id).metadata
                    .localizedPriceString
                : _unityInAppConfig.GetProduct(productType).DefaultPrice;
        }

        public string GetLocalizedPriceString(InAppProductType productType)
        {
            return IsInitialized()
                ? StoreController.products.WithID(_unityInAppConfig.GetProduct(productType).Id).metadata
                    .localizedPriceString
                : _unityInAppConfig.GetProduct(productType).DefaultPrice;
        }

        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            StoreController = controller;
            StoreExtensionProvider = extensions;
        }

        public void OnInitializeFailed(InitializationFailureReason error)
        {
            UnityEngine.Debug.LogError(error);
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