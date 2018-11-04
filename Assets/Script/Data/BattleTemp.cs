using UnityEngine;

public class BattleTemp
{
    public const float rateToMeter = 0.015f;//間合い系(cm)からunity座標系への変換倍率

    public int styleNo;

    Samurai samurai;
    float[] parameters;
    float expEstim;//経験知による見積もり係数の最大値
    public float ExpEstim { get { return expEstim; } }

    public BattleTemp(Samurai s, int stNo)
    {
        samurai = s;
        styleNo = stNo;
        parameters = new float[8];
        parameters[(int)PName.hp] = samurai.stamina;
        expEstim = Mathf.Exp(-samurai.experience * 0.00025f);
        CalcParams();
    }

    void CalcParams()//パラメータを再計算
    {
        int hp = (int)GetCorrectParam(PName.hp);
        SetParam(PName.stepLength,      samurai.height * 0.3f*rateToMeter);
        SetParam(PName.explosiveness,   samurai.speed * (0.25f + 0.75f * HpRate()));
        SetParam(PName.obsInterval,     25 - samurai.experience * 0.002f - 3 * HpRate());//4~10
        SetParam(PName.power,           samurai.power * 0.5f * (1 + HpRate()));
        SetParam(PName.defence,         samurai.defence * 0.5f * (1 + HpRate()));
        CalcReach(hp);
    }

    void CalcReach(int hp)//reach base is 10~100
    {
        float reachMin = 40, reachMax = 50;
        switch (styleNo)
        {
            case (int)StyleName.小:
                reachMin += 10 + samurai.height / 10;
                reachMax += 40 + samurai.height / 10;
                break;
            case (int)StyleName.中:
                reachMin += 30 + samurai.height / 10;
                reachMax += 70 + samurai.height / 8;
                break;
            case (int)StyleName.大:
                reachMin += 55 + samurai.height / 8;
                reachMax += 100 + samurai.height / 8;
                break;
        }
        //reachMin += 10 * (samurai.stamina - hp) / samurai.stamina;//間合い狭まるよ
        reachMax -= 10 * (1 - HpRate());
        SetParam(PName.reachMin, reachMin*rateToMeter);
        SetParam(PName.reachMax, reachMax*rateToMeter);
    }

    public float HpRate()
    {
        return (float)GetCorrectParam(PName.hp) / samurai.stamina;
    }

    public float GetCorrectParam(PName p)
        //自パラメータの際は誤差半減
    {
        return parameters[(int)p];
    }

    public float GetEstimParam(PName p,BattleTemp observer=null)
    {//経験誤差を加えて返す
        float estim = observer == null ? expEstim : observer.expEstim;
        estim = Random.Range(-estim, estim);
        return parameters[(int)p] * (1 + 0.5f * estim);
    }

    public void SetParam(PName p,float value)
    {
        if (value < 0) value = 0;
        parameters[(int)p] = value;

        if (p == PName.hp) CalcParams();
    }
}

public enum StyleName
{
    小, 中, 大
}

public enum PName
{
    hp = 0, power, defence, stepLength, explosiveness/*瞬発*/,
    reachMin, reachMax, obsInterval,
}
