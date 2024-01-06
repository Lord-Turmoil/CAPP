using System.Windows;
using Tonisoft.AspExtensions.Response;

namespace Client.Extensions.Popup;

static class PopupManager
{
    public static void ShowInvalidResponse(ApiResponse response)
    {
        MessageBox.Show($"Invalid Response:\n{response.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
    }

    public static void ShowInvalidResponse<TResult>(ApiResponse<TResult> response) where TResult : class
    {
        MessageBox.Show($"Invalid Response:\n{response.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
    }

    public static void ShowNetworkError(Exception e)
    {
        MessageBox.Show($"Error:\n{e.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
    }

    public static void ShowError(string message)
    {
        MessageBox.Show($"Error:\n{message}", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
    }
}