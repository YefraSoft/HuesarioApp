using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HuesarioApp.ViewModels.Auth;

namespace HuesarioApp.Views.Auth;

public partial class RegisterView : ContentPage
{
    public RegisterView(RegisterViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}