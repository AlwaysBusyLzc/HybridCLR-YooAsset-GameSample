using UniFramework.Event;
using UniFramework.Pooling;

public static class HotUpdateEntry
{
    public static void EntryGame()
    {

        UniEvent.Initalize();
        UniPooling.Initalize();

        GameManager.Instance.LoadScene("Hall");
    }
}
