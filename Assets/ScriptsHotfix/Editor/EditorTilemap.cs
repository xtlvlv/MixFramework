using UnityEngine;
using System.Collections;
using ScriptsHotfix;
using UnityEditor;
using UnityEngine.Tilemaps;

namespace ScriptsHotfix
{
    [CustomEditor(typeof(Tilemap), true)]
    public class EditorTilemap : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            Tilemap tilemap = target as Tilemap;
            if (tilemap == null)
            {
                return;
            }
            EditorGUILayout.TextField("Name:", tilemap.name);
            EditorGUILayout.Vector3Field("Size:", tilemap.size);
            EditorGUILayout.BoundsField("Bounds:", tilemap.localBounds);
        }
    }
}

