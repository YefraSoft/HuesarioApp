using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HuesarioApp.ViewModels.Dashboard;

namespace HuesarioApp.Views.Dashboard;

public partial class DashboardView : ContentPage
{
    public DashboardView(DashboardViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}