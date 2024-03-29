﻿// Copyright (C) 2018 - 2024 Tony's Studio. All rights reserved.

namespace Client.Models;

class SwatchItem
{
    public int Id { get; set; }
    public bool IsChecked { get; set; }

    public int X => (Id - 1) % 9;
    public int Y => (Id - 1) / 10;
}