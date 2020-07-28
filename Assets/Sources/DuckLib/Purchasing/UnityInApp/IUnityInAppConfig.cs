#if UNITY_PURCHASING
using System.Collections.Generic;
using DuckLib.Purchasing.InApp;

namespace DuckLib.Purchasing.UnityInApp
{
    public interface IUnityInAppConfig
    {
        IEnumerable<InAppInfo> Products { get; }
    }
}
#endif