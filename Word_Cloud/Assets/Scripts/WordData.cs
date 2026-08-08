[System.Serializable]
public class WordData
{
    public string Word;
    public int Importance;

    public WordData(string word, int importance)
    {
        Word = word;
        Importance = importance;
    }
}