﻿// Copyright (C) 2018 - 2024 Tony's Studio. All rights reserved.

using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace Client.Views;

/// <summary>
///     Interaction logic for IndexView.xaml
/// </summary>
public partial class IndexView : UserControl
{
    public IndexView()
    {
        InitializeComponent();
    }

    private void UIElement_OnPreviewMouseWheel(object sender, MouseWheelEventArgs e)
    {
        if (e == null)
        {
            throw new ArgumentNullException(nameof(e));
        }

        var scv = (ScrollViewer)sender;
        scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
        e.Handled = true;
    }

    private void Hyperlink_OnClick(object sender, RoutedEventArgs e)
    {
        var link = sender as Hyperlink;
        if (link == null)
        {
            return;
        }

        var psi = new ProcessStartInfo {
            FileName = link.NavigateUri.AbsoluteUri,
            UseShellExecute = true
        };
        Process.Start(psi);
    }
}