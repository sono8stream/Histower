

public class Samurai
{
    #region 表示ステータス
    public string lName, fName;//苗字、名
    public int age;//年齢
    public int height;//身長、歩幅に相関
    #endregion

    #region 非表示ステータス
    public const int expLim = 10000;//経験最大値
    public const int paramLim = 500;//その他のステータス最大値

    public int stamina;//体力
    public int power;//力、攻速はこれと速度に依存
    public int speed;//速度、歩行速度と攻速に相関
    public int defence;//守り
    public int experience;//経験知、増えるほど相手の実力を見積もりやすい
    #endregion

    public Samurai(string ln, string fn, int h, int stam,
        int pow, int sp, int def,int exp)
    {
        fName = fn;
        lName = ln;
        height = h;
        stamina = stam;
        power = pow;
        speed = sp;
        defence = def;
        experience = exp;

    }
}
