// Copyright (C) 2018 - 2024 Tony's Studio. All rights reserved.

using System.Collections.ObjectModel;
using System.Text;
using Client.Models;

namespace Client.Extensions;

static class SwatchHelper
{
    public static IEnumerable<SwatchItem> InitSwatchHeaders()
    {
        var swatchHeaders = new List<SwatchItem>();
        for (int i = 0; i < 9; i++)
        {
            swatchHeaders.Add(new SwatchItem { Id = i + 1 });
        }

        return swatchHeaders;
    }

    public static ObservableCollection<SwatchSet> InitSwatches()
    {
        var swatches = new ObservableCollection<SwatchSet>();
        for (int i = 0; i < 10; i++)
        {
            var swatchSet = new SwatchSet { Name = $"{i}" };
            var swatchItems = new List<SwatchItem>();
            for (int j = 0; j < 9; j++)
            {
                swatchItems.Add(new SwatchItem { Id = i * 9 + j + 1 });
            }

            swatchSet.Items = swatchItems;
            swatches.Add(swatchSet);
        }

        return swatches;
    }

    public static ObservableCollection<SwatchSet> InitSwatches(string matrix)
    {
        if (matrix.Length < 90)
        {
            matrix = matrix.PadRight(90, '0');
        }

        var swatches = new ObservableCollection<SwatchSet>();
        for (int i = 0; i < 10; i++)
        {
            var swatchSet = new SwatchSet { Name = $"{i}" };
            var swatchItems = new List<SwatchItem>();
            for (int j = 0; j < 9; j++)
            {
                swatchItems.Add(new SwatchItem { Id = i * 9 + j + 1, IsChecked = matrix[i * 9 + j] == '1' });
            }

            swatchSet.Items = swatchItems;
            swatches.Add(swatchSet);
        }

        return swatches;
    }

    public static string GetSwatchString(IEnumerable<SwatchItem> swatchItems)
    {
        var sb = new StringBuilder();
        foreach (SwatchItem swatchItem in swatchItems)
        {
            sb.Append(swatchItem.IsChecked ? "1" : "0");
        }

        return sb.ToString();
    }
}