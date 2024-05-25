using UniFramework.Event;

namespace HotUpdate.EventDefine
{
    public class FlappyBirdEventDefine
    {
        public class BackgroundPosReset : IEventMessage
        {
            public string gameobject_name;
            public static void SendEventMessage(string name)
            {
                var msg = new BackgroundPosReset();
                msg.gameobject_name = name;
                UniEvent.SendMessage(msg);
            }
        }


        public class BirdDead : IEventMessage
        {
            public static void SendEventMessage()
            {
                var msg = new BirdDead();
                UniEvent.SendMessage(msg);
            }
        }

        
    }
}