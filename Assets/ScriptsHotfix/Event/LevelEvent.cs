namespace ScriptsHotfix
{
    public struct LevelStartEvent
    {
        
    }
    
    public class LevelDeadEvent
    {
        public bool Win;

        public LevelDeadEvent(bool a_res=false)
        {
            Win = a_res;
        }
    }
    
    public struct LevelWalkEvent
    {
        public float Percent;

        public LevelWalkEvent(float a_percent)
        {
            Percent = a_percent;
        }
    }
}