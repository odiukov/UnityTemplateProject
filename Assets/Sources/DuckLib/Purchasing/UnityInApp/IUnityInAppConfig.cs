#if UNITY_PURCHASING
using System.Collections.Generic;
using DuckLib.Purchasing.InApp;

namespace DuckLib.Purchasing.UnityInApp
{
    public interface IUnityInAppConfig
    {
        InAppInfo GetProduct(InAppProductType productType);
        Dictionary<InAppProductKind, List<InAppProductType>> Products { get; }
    }
}
#endif