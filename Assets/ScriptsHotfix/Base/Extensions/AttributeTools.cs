using System;

namespace ScriptsHotfix
{
    public static class AttributeTools
    {
        public static TAtr GetAttribute<TAtr>(Type type) where TAtr : Attribute
        {
            TAtr[] attrs = (TAtr[])type.GetCustomAttributes(typeof(TAtr), false);
            if (attrs.Length > 0)
            {
                TAtr attr = attrs[0];
                return attr;
            }

            return null;
        }
    }
}