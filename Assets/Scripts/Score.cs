[System.Serializable]
public class Score
{
    public string username;
    public int score;
    public int unixTimestamp;

    public Score(string username, int score, int unixTimestamp)
    {
        this.username = username;
        this.score = score;
        this.unixTimestamp = unixTimestamp;
    }
}