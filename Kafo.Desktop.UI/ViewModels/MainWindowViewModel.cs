using System;
using System.Collections.Generic;
using System.Windows.Documents;
using Autofac;
using CommunityToolkit.Mvvm.ComponentModel;
using Kafo.Desktop.UI.Services;

namespace Kafo.Desktop.UI.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    [ObservableProperty] private ObservableObject _selectedViewModel;

    private List<ObservableObject> viewModels = new List<ObservableObject>();

    public MainWindowViewModel(AppNavigator appNavigator, LoginScreenViewModel loginScreenViewModel,
        KafoMainViewModel kafoMainViewModel)
    {
        viewModels.Add(loginScreenViewModel);
        viewModels.Add(kafoMainViewModel);
        appNavigator.onNavigateTo += OnNavigateToHandler;

        _selectedViewModel = loginScreenViewModel;
    }


    private void OnNavigateToHandler(Type type)
    {
        var viewModel = viewModels.Find(x => x.GetType() == type);
        if (viewModel != null)
        {
            SelectedViewModel = viewModel as ObservableObject;
        }
    }
}