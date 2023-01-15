using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace ScriptsHotfix
{
    public static class CorotineTools
    {
        public static readonly WaitForEndOfFrame  EndOfFrame  = new WaitForEndOfFrame();
        public static readonly WaitForSeconds     Second1     = new WaitForSeconds(1);
        public static readonly WaitForSeconds     Second2     = new WaitForSeconds(2);
        public static readonly WaitForSeconds     Second3     = new WaitForSeconds(3);
        public static readonly WaitForFixedUpdate FixedUpdate = new WaitForFixedUpdate();
    }
}