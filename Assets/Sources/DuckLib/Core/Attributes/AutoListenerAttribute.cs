using System;

namespace DuckLib.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class)]
    public class AutoListenerAttribute : Attribute
    {
        public readonly Type Type;

        public AutoListenerAttribute(Type type)
        {
            Type = type;
        }
    }
}