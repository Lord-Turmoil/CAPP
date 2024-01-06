using System.Linq.Expressions;

namespace Shared.Common;


/// <summary>
/// Group Technology Core.
/// </summary>
public static class GtCore
{
    /// <summary>
    /// The version of the Group Technology Core.
    /// </summary>
    public static string Version { get; } = "1.0.0";

    public static IEnumerable<TPart> Filter<TGroup, TPart>(
        IEnumerable<TPart> parts,
        Func<TPart, string> opitz,
        TGroup group,
        Func<TGroup, string> matrix)
        where TGroup : class
        where TPart : class
    {
        string matrixString = matrix(group);
        return parts.Where(p => Match(opitz(p), matrixString));
    }

    private static bool Match(string opitz, string matrix)
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
}