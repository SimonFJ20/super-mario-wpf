namespace SuperMarioWPF;

class Scoreboard
{
    public int score, coins, lives;
    private double time;

    public Scoreboard()
    {
        score = 0;
        coins = 0;
        lives = 0;
        time = 300;
    }

    public bool Tick(double deltaTS)
    {
        time -= deltaTS;
        return !(time < 0);
    }
}