using System;
using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using Kafo.Desktop.UI.Services;

namespace Kafo.Desktop.UI.ViewModels;

public partial class KafoMainViewModel : ObservableObject
{ 
    [ObservableProperty] 
    private ObservableObject _selectedViewModel;

    private List<ObservableObject> viewModels = new List<ObservableObject>();

    public KafoMainViewModel(AppNavigator appNavigator)
    {
        appNavigator.onNavigateTo += OnNavigateToHandler;
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