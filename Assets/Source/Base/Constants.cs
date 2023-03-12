using UnityEngine;

namespace Constants
{
    public static class Strings
    {
        public const string IOC_INCLUDED_ASM_PATH = "Assets/Source/Base/IOC/IOCIncludedAssemblies.asset";
        public const string IOC_PREFAB_PATH = "IOC/Context";
        public const string LEVELS_PATH = "Assets/GameAssets/Levels/Levels.json";
        public const string SAMPLE_DATA_MODEL_PATH = "/Source/Base/Models/DataModels/SampleDataModel.cs";
        public const string NEW_DATA_MODEL_PATH = "/Source/Base/Models/DataModels/";
        public const string RENDERED_TEXTURES_PATH = "/RenderedTexturesOutput/";
        public const string REGISTRY_PATH = "Assets/Scriptables/Registry.asset";
        public const string WALKABLE_REF_PATH = "Assets/GameAssets/UI/Grid/Walkable.png";
        public const string NON_WALKABLE_REF_PATH = "Assets/GameAssets/UI/Grid/NonWalkable.png";
    }

    public static class Colors
    {
        public const string NOT_WALKABLE_AREA_COLOR = "#EC953E";
        public const string WALKABLE_AREA_COLOR = "#7AE885";
    }

    public static class Numerical
    {
        public const int NOT_IN_HEAP = int.MaxValue;
        public const int PIXEL_PER_UNIT = 100;
        public const int CELL_HEIGHT_AS_PIXEL = 32;
        public const float CELL_SCALE_AS_UNIT = (float)CELL_HEIGHT_AS_PIXEL / (float)PIXEL_PER_UNIT;
    }
}