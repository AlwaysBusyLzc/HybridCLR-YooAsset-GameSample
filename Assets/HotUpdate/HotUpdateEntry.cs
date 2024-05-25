using UniFramework.Event;
using UniFramework.Log;
using UniFramework.Pooling;

public static class HotUpdateEntry
{
    public static void EntryGame()
    {

        UniEvent.Initalize();
        UniLog.Initalize();
        UniPooling.Initalize();

        GameManager.Instance.LoadScene("Hall");
    }
}
