namespace ScriptsHotfix
{
    public class ViewId
    {
        public const int LoginUI = 1;
        public const int MainUI = 2;
        public const int SpecialEffectsUI = 3;
        public const int BigMapUI = 4;
    }

    public enum ViewLayer
    {
        // TODO: 分层管理
    }

    public class ViewConfig
    {
        public int    ViewId;
        public int       SortingOrder = 0;
        public string    PrefabPath;
    }
}