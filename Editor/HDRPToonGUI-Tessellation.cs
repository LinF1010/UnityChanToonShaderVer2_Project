﻿//Unitychan Toon Shader ver.8.0
//v.8.0.0
//nobuyuki@unity3d.com
//toshiyuki@unity3d.com (Univerasl RP/HDRP) 
//https://github.com/unity3d-jp/UnityChanToonShaderVer2_Project
//(C)Unity Technologies Japan/UCL
using UnityEngine;
using UnityEditor;

namespace UnityEditor.Rendering.HDRP.Toon
{
    public partial class HDRPToonGUI : ShaderGUI
    {
        enum TessellationMode
        {
            None,
            Phong
        }

        public class TessellationStyles
        {
            public const string header = "Tessellation Options";

            public static string tessellationModeStr = "Tessellation Mode";

            public static readonly string[] tessellationModeNames = System.Enum.GetNames(typeof(
            TessellationMode));
            public static GUIContent tessellationText = new GUIContent("Tessellation Options", "Tessellation options");
            public static GUIContent tessellationFactorText = new GUIContent("Tessellation Factor", "Controls the strength of the tessellation effect. Higher values result in more tessellation. Maximum tessellation factor is 15 on the Xbox One and PS4");
            public static GUIContent tessellationFactorMinDistanceText = new GUIContent("Start Fade Distance", "Sets the distance (in meters) at which tessellation begins to fade out.");
            public static GUIContent tessellationFactorMaxDistanceText = new GUIContent("End Fade Distance", "Sets the maximum distance (in meters) to the Camera where HDRP tessellates triangle.");
            public static GUIContent tessellationFactorTriangleSizeText = new GUIContent("Triangle Size", "Sets the desired screen space size of triangles (in pixels). Smaller values result in smaller triangle.");
            public static GUIContent tessellationShapeFactorText = new GUIContent("Shape Factor", "Controls the strength of Phong tessellation shape (lerp factor).");
            public static GUIContent tessellationBackFaceCullEpsilonText = new GUIContent("Triangle Culling Epsilon", "Controls triangle culling. A value of -1.0 disables back face culling for tessellation, higher values produce more aggressive culling and better performance.");
        }

        // tessellation params
        MaterialProperty tessellationMode = null;
        const string kTessellationMode = "_TessellationMode";
        MaterialProperty tessellationFactor = null;
        const string kTessellationFactor = "_TessellationFactor";
        MaterialProperty tessellationFactorMinDistance = null;
        const string kTessellationFactorMinDistance = "_TessellationFactorMinDistance";
        MaterialProperty tessellationFactorMaxDistance = null;
        const string kTessellationFactorMaxDistance = "_TessellationFactorMaxDistance";
        MaterialProperty tessellationFactorTriangleSize = null;
        const string kTessellationFactorTriangleSize = "_TessellationFactorTriangleSize";
        MaterialProperty tessellationShapeFactor = null;
        const string kTessellationShapeFactor = "_TessellationShapeFactor";
        MaterialProperty tessellationBackFaceCullEpsilon = null;
        const string kTessellationBackFaceCullEpsilon = "_TessellationBackFaceCullEpsilon";
        MaterialProperty doubleSidedEnable = null;
        const string kDoubleSidedEnable = "_DoubleSidedEnable";

        public static GUIContent tessellationModeText = new GUIContent("Tessellation Mode",
    "Transparent  mode that fits you. ");


        void FindTessellationProperties(MaterialProperty[] props)
        {
            tessellationMode = FindProperty(kTessellationMode, props, false);
            tessellationFactor = FindProperty(kTessellationFactor, props, false);
            tessellationFactorMinDistance = FindProperty(kTessellationFactorMinDistance, props, false);
            tessellationFactorMaxDistance = FindProperty(kTessellationFactorMaxDistance, props, false);
            tessellationFactorTriangleSize = FindProperty(kTessellationFactorTriangleSize, props, false);
            tessellationShapeFactor = FindProperty(kTessellationShapeFactor, props, false);
            tessellationBackFaceCullEpsilon = FindProperty(kTessellationBackFaceCullEpsilon, props, false);
            doubleSidedEnable = FindProperty(kDoubleSidedEnable, props, false);

        }
        void TessellationModePopup()
        {
            EditorGUI.showMixedValue = tessellationMode.hasMixedValue;
            var mode = (TessellationMode)tessellationMode.floatValue;

            EditorGUI.BeginChangeCheck();
            mode = (TessellationMode)EditorGUILayout.Popup(TessellationStyles.tessellationModeStr, (int)mode, TessellationStyles.tessellationModeNames);
            if (EditorGUI.EndChangeCheck())
            {
                m_MaterialEditor.RegisterPropertyChangeUndo("Tessellation Mode");
                tessellationMode.floatValue = (float)mode;
            }

            EditorGUI.showMixedValue = false;
        }
        void TessellationSetting(Material material)
        {
            if (tessellationMode == null)
            {
                return;
            }
            TessellationModePopup();
        }
    } // End of UTS2GUI2
}// End of namespace UnityChan