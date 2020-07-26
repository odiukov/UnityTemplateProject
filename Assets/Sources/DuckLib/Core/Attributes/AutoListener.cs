using System;

namespace DuckLib.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class)]
    public class AutoListener : Attribute
    {
        public readonly Type Type;

        public AutoListener(Type type)
        {
            Type = type;
        }
    }
}