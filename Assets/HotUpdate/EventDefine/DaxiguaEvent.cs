using UniFramework.Event;

namespace HotUpdate.EventDefine
{
    public class DaxiguaEventDefine
    {
        public class FruitDrop : IEventMessage
        {
            public static void SendEventMessage()
            {
                var msg = new DaxiguaEventDefine.FruitDrop();
                UniEvent.SendMessage(msg);
            }
        }
    }
}