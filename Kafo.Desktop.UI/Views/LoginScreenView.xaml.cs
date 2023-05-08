using System.Windows;
using System.Windows.Controls;

namespace Kafo.Desktop.UI.Views;

public partial class LoginScreenView : Page
{
    public LoginScreenView()
    {
        InitializeComponent();
    }
    
    private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
    {
        if (this.DataContext != null)
        { ((dynamic)this.DataContext).Password = ((PasswordBox)sender).SecurePassword; }
    }
}