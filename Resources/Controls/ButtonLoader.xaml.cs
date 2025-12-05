using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HuesarioApp.Resources.Controls;

public partial class ButtonLoader : ContentView
{
    public ButtonLoader()
    {
        InitializeComponent();
    }

    public static readonly BindableProperty ButtonTextProperty =
        BindableProperty.Create(
            nameof(ButtonText),
            typeof(string),
            typeof(ButtonLoader),
            defaultValue: "Button");

    public string ButtonText
    {
        get => (string)GetValue(ButtonTextProperty);
        set => SetValue(ButtonTextProperty, value);
    }

    public static readonly BindableProperty LoaderTextProperty =
        BindableProperty.Create(
            nameof(LoaderText),
            typeof(string),
            typeof(ButtonLoader),
            defaultValue: "Loading...");

    public string LoaderText
    {
        get => (string)GetValue(LoaderTextProperty);
        set => SetValue(LoaderTextProperty, value);
    }

    public static readonly BindableProperty IsBusyProperty =
        BindableProperty.Create(
            nameof(IsBusy),
            typeof(bool),
            typeof(ButtonLoader),
            defaultValue: false);

    public bool IsBusy
    {
        get => (bool)GetValue(IsBusyProperty);
        set => SetValue(IsBusyProperty, value);
    }

    public static readonly BindableProperty CommandProperty =
        BindableProperty.Create(
            nameof(Command),
            typeof(ICommand),
            typeof(ButtonLoader));

    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }
}