using UnityEngine;

namespace DuckLib.Core.Extensions
{
    public static class GameObjectExtensions
    {
        public static bool IsOfLayer(this Collider2D collider, string layer) =>
            collider.gameObject.layer == LayerMask.NameToLayer(layer);

        public static bool Matches(this Collider2D collider, LayerMask layerMask) =>
            ((1 << collider.gameObject.layer) & layerMask) != 0;

        public static void SetLayer(this GameObject parent, string layer, bool includeChildren = false)
        {
            var unmaskedLayer = LayerMask.NameToLayer(layer);
            parent.layer = unmaskedLayer;
            if (!includeChildren) return;
            //for some reasons this is faster than direct transform enumeration
            foreach (var trans in parent.transform.GetComponentsInChildren<Transform>(true))
                trans.gameObject.layer = unmaskedLayer;
        }
    }
}