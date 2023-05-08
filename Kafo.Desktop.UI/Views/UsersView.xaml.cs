using System.Windows;
using System.Windows.Controls;

namespace Kafo.Desktop.UI.Views;

public partial class UsersView : UserControl
{
    public UsersView()
    {
        InitializeComponent();
    }
    
    private void NewPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
    {
        if (this.DataContext != null)
        { ((dynamic)this.DataContext).NewPassword = ((PasswordBox)sender).SecurePassword; }
    }

    private void NewPasswordRepeatBox_PasswordChanged(object sender, RoutedEventArgs e)
    {
        if (this.DataContext != null)
        { ((dynamic)this.DataContext).NewPasswordRepeat = ((PasswordBox)sender).SecurePassword; }
    }

    private void NewUserPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
    {
        if (this.DataContext != null)
        { ((dynamic)this.DataContext).NewUserModel.Password = ((PasswordBox)sender).SecurePassword; }
    }

    private void NewUserPasswordRepeatBox_PasswordChanged(object sender, RoutedEventArgs e)
    {
        if (this.DataContext != null)
        { ((dynamic)this.DataContext).NewUserModel.PasswordRepeat = ((PasswordBox)sender).SecurePassword; }
    }

}