﻿// Copyright (C) 2018 - 2024 Tony's Studio. All rights reserved.

using System.ComponentModel;
using System.Diagnostics;
using System.Windows;

namespace Client.Views;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainView : Window
{
    public MainView()
    {
        InitializeComponent();
    }

    private void MainView_OnClosing(object? sender, CancelEventArgs e)
    {
        Process.Start("Launch.bat", "stop");
    }
}