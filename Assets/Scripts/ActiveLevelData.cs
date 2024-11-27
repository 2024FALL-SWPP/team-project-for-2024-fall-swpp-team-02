public class ActiveLevelData
{
    public int level;
    public int score;

    public ActiveLevelData(int level)
    {
        this.level = level;
        this.score = 0;
    }
    public ActiveLevelData(int level, int score)
    {
        this.level = level;
        this.score = score;
    }
}