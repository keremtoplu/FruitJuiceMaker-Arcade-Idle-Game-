using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public static class Extensions
{
    private static string[] units = { "", "K", "M", "B", "T", "q", "Q", "s", "S" };

    public static string FormatCurrency(this double num, int precision = 1)
    {
        double formattedNumber = 0;
        int unitIndex = 0;
        bool isSciNotation = false;

        if (num < 1e3)
        {
            formattedNumber = num;
            unitIndex = 0;
        }
        else if (num < 1e6)  // 1K - 1M
        {
            formattedNumber = num / 1e3;
            unitIndex = 1;
        }
        else if (num < 1e9)   // 1M - 1B
        {
            formattedNumber = num / 1e6;
            unitIndex = 2;
        }
        else if (num < 1e12)  // 1B - 1T
        {
            formattedNumber = num / 1e9;
            unitIndex = 3;
        }
        else if (num < 1e15) // 1T - 1q
        {
            formattedNumber = num / 1e12;
            unitIndex = 4;
        }
        else if (num < 1e18) // 1 quadrillion - 1 Quintillion
        {
            formattedNumber = num / 1e15;
            unitIndex = 5;
        }
        else if (num < 1e21) // 1 Quintillion - 1 sextillion
        {
            formattedNumber = num / 1e18;
            unitIndex = 6;
        }
        else if (num < 1e24) // 1 sextillion - 1 Septillion
        {
            formattedNumber = num / 1e21;
            unitIndex = 7;
        }
        else if (num < 1e27) // 1 Septillion - 1 Octillion
        {
            formattedNumber = num / 1e24;
            unitIndex = 8;
        }
        else
        {
            isSciNotation = true;
        }

        var roundedNumber = Math.Round(formattedNumber, 1, MidpointRounding.ToEven);

        // NOTE: If roundedNumber is 1000 then format should be changed.
        // Example: 999.98 will be rounded to 1000, for example; its unit is K
        // and after rounding, it should be M and the number should be 1 not 1000. This is an edge case.
        if (roundedNumber == 1000.0)
        {
            if (unitIndex + 1 < units.Length)
            {
                unitIndex++;
                roundedNumber = 1.0;
            }
            // Number is rounded something beyond our format, show with sci-notion
            else
            {
                isSciNotation = true;
            }
        }

        if (isSciNotation)
        {
            return num.ToString("0.##E+0", CultureInfo.InvariantCulture);
        }

        return roundedNumber.FormatNumber(precision) + units[unitIndex];
    }

    public static string FormatCurrency(this int num, int precision = 1)
    {
        return FormatCurrency((double)num, precision);
    }
    public static string FormatCurrency(this float num, int precision = 1)
    {
        return FormatCurrency((double)num, precision);
    }

    public static string FormatNumber(this double value, int precision = 1)
        {
            string format = "0." + new string('#', precision);
            return value.ToString(format, CultureInfo.InvariantCulture);
        }
}
