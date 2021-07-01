using System.Reflection;
using UnityEditor;

namespace Plugins.MobileScreenResolutions.Scripts.Editor
{
    public static class GameViewUtils
    {
        private static readonly object gameViewSizesInstance;
        private static readonly MethodInfo getGroup;
        private static int screenIndex;
        private static int totalResolutions;

        static GameViewUtils()
        {
            var sizesType = typeof(UnityEditor.Editor).Assembly.GetType("UnityEditor.GameViewSizes");
            var singleType = typeof(ScriptableSingleton<>).MakeGenericType(sizesType);
            var instanceProp = singleType.GetProperty("instance");
            getGroup = sizesType.GetMethod("GetGroup");
            gameViewSizesInstance = instanceProp.GetValue(null, null);

            screenIndex = GetBuiltinCount();
        }

        private enum GameViewSizeType
        {
            AspectRatio,
            FixedResolution
        }

        public static int FindSize(int width, int height)
        {
            var group = GetGroup();
            var groupType = group.GetType();
            var getBuiltinCount = groupType.GetMethod("GetBuiltinCount");
            var getCustomCount = groupType.GetMethod("GetCustomCount");
            int sizesCount = (int) getBuiltinCount.Invoke(group, null) + (int) getCustomCount.Invoke(group, null);
            var getGameViewSize = groupType.GetMethod("GetGameViewSize");
            var gvsType = getGameViewSize.ReturnType;
            var widthProp = gvsType.GetProperty("width");
            var heightProp = gvsType.GetProperty("height");
            var indexValue = new object[1];
            for (int i = 0; i < sizesCount; i++)
            {
                indexValue[0] = i;
                var size = getGameViewSize.Invoke(group, indexValue);
                int sizeWidth = (int) widthProp.GetValue(size, null);
                int sizeHeight = (int) heightProp.GetValue(size, null);
                if (sizeWidth == width && sizeHeight == height)
                    return i;
            }

            return -1;
        }

        public static void AddCustomSize(int width, int height, string text)
        {
            var group = GetGroup();
            var addCustomSize = getGroup.ReturnType.GetMethod("AddCustomSize");
            var gvsType = typeof(UnityEditor.Editor).Assembly.GetType("UnityEditor.GameViewSize");
            var gvstType = typeof(UnityEditor.Editor).Assembly.GetType("UnityEditor.GameViewSizeType");
            var ctor = gvsType.GetConstructor(new[] {gvstType, typeof(int), typeof(int), typeof(string)});
            var newSize = ctor.Invoke(new object[] {(int) GameViewSizeType.FixedResolution, width, height, text});
            addCustomSize.Invoke(group, new[] {newSize});

            var sizesType = typeof(UnityEditor.Editor).Assembly.GetType("UnityEditor.GameViewSizes");
            sizesType.GetMethod("SaveToHDD").Invoke(gameViewSizesInstance, null);
        }

        public static void SetSize(int index)
        {
            var gvWndType = typeof(UnityEditor.Editor).Assembly.GetType("UnityEditor.GameView");
            var gvWnd = EditorWindow.GetWindow(gvWndType);
            var SizeSelectionCallback = gvWndType.GetMethod("SizeSelectionCallback",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            SizeSelectionCallback.Invoke(gvWnd, new object[] {index, null});
        }

        public static void SetPrevious()
        {
            CalculateTotalResolutions();
            if (screenIndex - 1 >= GetBuiltinCount())
            {
                screenIndex -= 1;
            }
            else
            {
                screenIndex = totalResolutions - 1;
            }

            SetSize(screenIndex);
        }

        public static void SetNext()
        {
            CalculateTotalResolutions();
            if (screenIndex + 1 < totalResolutions)
            {
                screenIndex += 1;
            }
            else
            {
                screenIndex = GetBuiltinCount();
            }

            SetSize(screenIndex);
        }

        public static void RemoveResolutions()
        {
            CalculateTotalResolutions();

            var defaultResolutions = GetBuiltinCount();
            var extraResolutions = totalResolutions - defaultResolutions;
            var group = GetGroup();
            var removeCustomSize = getGroup.ReturnType.GetMethod("RemoveCustomSize");

            for (int i = 0; i < extraResolutions; i++)
            {
                removeCustomSize.Invoke(group, new object[] {0 + defaultResolutions});
            }

            SetSize(0);
        }

        private static void CalculateTotalResolutions()
        {
            var group = GetGroup();
            var getDisplayTexts = group.GetType().GetMethod("GetDisplayTexts");
            totalResolutions = (getDisplayTexts.Invoke(group, null) as string[]).Length;
        }

        private static GameViewSizeGroupType Platform
        {
            get
            {
#if UNITY_ANDROID
                return GameViewSizeGroupType.Android;
#elif UNITY_IOS
            return GameViewSizeGroupType.iOS;
#endif
                return GameViewSizeGroupType.Standalone;
            }
        }

        private static int GetBuiltinCount()
        {
            var group = GetGroup();
            var getBuiltinCount = getGroup.ReturnType.GetMethod("GetBuiltinCount");
            return (int) getBuiltinCount.Invoke(group, null);
        }

        private static object GetGroup()
        {
            return getGroup.Invoke(gameViewSizesInstance, new object[] {(int) Platform});
        }
    }
}