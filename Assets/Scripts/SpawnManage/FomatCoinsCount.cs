using UnityEngine;

public static class FomatCoinsCount
{
    private static string[] names = new[]
    {
        "",
        "K",
        "M",
        "B",
        "T",
    };

    public static string FormatCount(double count)
    {
        if (count == 0) return "0";

        count = Mathf.Round((float)count);

        int i = 0;
        while (i + 1 < names.Length && count >= 1000d)
        {
            count /= 1000d;
            i++;
        }

        return count.ToString("#.##") + names[i];
    }
}
