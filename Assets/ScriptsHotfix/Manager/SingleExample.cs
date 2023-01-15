using BaseFramework.Core;

namespace ScriptsHotfix
{
    public class SingleExample: Singleton<SingleExample>, ISingletonAwake, ISingletonUpdate, ISingletonLateUpdate
    {
        
        public void Awake()
        {
            // throw new System.NotImplementedException();
        }

        public void Update()
        {
            // throw new System.NotImplementedException();
        }

        public void LateUpdate()
        {
            // throw new System.NotImplementedException();
        }
        
        public override void Dispose()
        {
            base.Dispose();
        }
    }
}