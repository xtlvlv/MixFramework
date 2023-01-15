using System.Collections.Generic;

namespace ScriptsHotfix
{
    public static class ListExtension
    {
        public static void AdaptListCount<T>(this List<T> list, int count)
        {
            if (list.Count > count)
            {
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    if (i >= count)
                    {
                        list.RemoveAt(i);
                    }
                }
            }
            else
            {
                for (int i = list.Count; i < count; i++)
                {
                    list.Add(default(T));
                }
            }
        }
    }
}