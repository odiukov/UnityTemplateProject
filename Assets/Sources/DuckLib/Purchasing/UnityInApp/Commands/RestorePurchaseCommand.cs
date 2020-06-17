#if UNITY_PURCHASING
using DuckLib.Core.Commands;
using DuckLib.Purchasing.InApp;

namespace DuckLib.Purchasing.UnityInApp.Commands
{
    public class RestorePurchaseCommand : CommandWithCallback<bool>
    {
        private readonly IInAppController _controller;

        public RestorePurchaseCommand(IInAppController controller)
        {
            _controller = controller;
        }

        protected override void OnStart()
        {
            var unityInAppController = (UnityInAppController) _controller;
            if (unityInAppController.IsInitialized())
            {
#if UNITY_ANDROID
                FinishCommand(true);
#elif UNITY_IOS
                unityInAppController.StoreExtensionProvider.GetExtension<IAppleExtensions>()
                    .RestoreTransactions(result => FinishCommand(result));
#endif
            }
            else
            {
                FinishCommand(false);
            }
        }
    }
}
#endif