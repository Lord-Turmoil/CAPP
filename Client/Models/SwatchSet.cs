// Copyright (C) 2018 - 2024 Tony's Studio. All rights reserved.

namespace Client.Models;

class SwatchSet
{
    public string Name { get; set; } = null!;
    public IEnumerable<SwatchItem> Items { get; set; } = null!;
}