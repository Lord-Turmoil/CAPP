// Copyright (C) 2018 - 2024 Tony's Studio. All rights reserved.

namespace Shared.Common;

/// <summary>
///     Group Technology Core.
/// </summary>
public static class GtCore
{
    /// <summary>
    ///     The version of the Group Technology Core.
    /// </summary>
    public static string Version { get; } = "1.0.0";

    public static IEnumerable<TPart> Filter<TPart>(
        IEnumerable<TPart> parts,
        Func<TPart, string> opitz,
        string matrix) where TPart : class
    {
        return parts.Where(p => Match(opitz(p), matrix));
    }

    public static bool Match(string opitz, string matrix)
    {
        if (opitz.Length != 9 || matrix.Length != 90)
        {
            return false;
        }

        for (int i = 0; i < 9; i++)
        {
            int value = int.Parse(opitz[i].ToString());
            if (matrix[value * 9 + i] != '1')
            {
                return false;
            }
        }

        return true;
    }

    public static int VerifyOpitz(string opitz)
    {
        if (opitz.Length == 9 && int.TryParse(opitz, out int value))
        {
            return value;
        }

        return 0;
    }
}