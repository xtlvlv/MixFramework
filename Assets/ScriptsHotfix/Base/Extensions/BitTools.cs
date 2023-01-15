using System;
using System.Collections.Generic;
using System.Reflection;

namespace ScriptsHotfix
{
    public static class BitTools
    {

        public static bool GetValueFromBitMark(this uint value, int index)
        {
            return (value >> index) % 2 == 1;
        }

        public static bool GetValueFromBitMark(this int value, int index)
        {
            return (value >> index) % 2 == 1;
        }
    }

}