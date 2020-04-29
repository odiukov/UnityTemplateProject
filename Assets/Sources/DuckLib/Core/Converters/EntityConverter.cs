using UnityEngine;

namespace DuckLib.Core.Converters
{
    public static class EntityConverter
    {
        public static void Convert<TEntity>(this GameObject viewGo, TEntity entity)
        {
            var wrappers = viewGo.GetComponents<IConvertToEntity<TEntity>>();
            foreach (var eventListener in wrappers)
            {
                eventListener.Convert(entity);
            }
        }
    }
}