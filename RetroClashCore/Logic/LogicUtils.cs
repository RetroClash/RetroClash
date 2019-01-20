namespace RetroClashCore.Logic
{
    public class LogicUtils
    {
        public static int GetLeagueByScore(int score)
        {
            if (score <= 399)
                return 0;
            if (score >= 400 && score < 500)
                return 1;
            if (score >= 500 && score < 600)
                return 2;
            if (score >= 600 && score < 800)
                return 3;
            if (score >= 800 && score < 1000)
                return 4;
            if (score >= 1000 && score < 1200)
                return 5;
            if (score >= 1200 && score < 1400)
                return 6;
            if (score >= 1400 && score < 1600)
                return 7;
            if (score >= 1600 && score < 1800)
                return 8;
            if (score >= 1800 && score < 2000)
                return 9;
            if (score >= 2000 && score < 2200)
                return 10;
            if (score >= 2200 && score < 2400)
                return 11;
            if (score >= 2400 && score < 2600)
                return 12;
            if (score >= 2600 && score < 2800)
                return 13;
            if (score >= 2800 && score < 3000)
                return 14;
            if (score >= 3000 && score < 3200)
                return 15;
            if (score >= 3200 && score < 10000000)
                return 16;

            return 0;
        }
    }
}