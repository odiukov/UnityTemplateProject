using DuckLib.Core.Commands;

namespace DuckLib.Purchasing.InApp.Commands
{
    public abstract class InAppPurchaseCommand : CommandWithArgsAndCallback<InAppPurchaseCommandArgs, InAppPurchaseCommandResult>
    {
        protected readonly IInAppController Controller;
        protected readonly InAppPurchaseCommandResult Result = new InAppPurchaseCommandResult();

        protected InAppPurchaseCommand(IInAppController controller)
        {
            Controller = controller;
        }
    }

    public class InAppPurchaseCommandArgs
    {
        public InAppProductType InAppProductType;
        public string Id;
    }
}