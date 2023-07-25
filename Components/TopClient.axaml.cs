using System;
using System.Data.Common;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;

namespace CookinGest.Components;

public partial class TopClient : TemplatedControl
{
    public string FirstName
    {
        get => GetValue(FirstNameProperty);
        set => SetValue(FirstNameProperty, value);
    }

    public string LastName
    {
        get => GetValue(LastNameProperty);
        set => SetValue(LastNameProperty, value);
    }

    public string SubTitle
    {
        get => GetValue(LastNameProperty);
        set => SetValue(LastNameProperty, value);
    }

    public string Initiale
    {
        get => GetValue(InitialeProperty);
        set => SetValue(InitialeProperty, value);
    }

    public int Index
    {
        get => GetValue(IndexProperty);
        set => SetValue(IndexProperty, value);
    }

    // Property
    public static readonly StyledProperty<int> IndexProperty = AvaloniaProperty.Register<TopClient, int>(
        nameof(Index), 0);

    public static readonly StyledProperty<string> FirstNameProperty = AvaloniaProperty.Register<TopClient, string>(
        nameof(FirstName), "<prenom>");

    public static readonly StyledProperty<string> LastNameProperty = AvaloniaProperty.Register<TopClient, string>(
        nameof(LastName), "<nom>");

    public static readonly StyledProperty<string> InitialeProperty = AvaloniaProperty.Register<TopClient, string>(
        nameof(LastName), "--");

    public static readonly StyledProperty<string> SubTitleProperty = AvaloniaProperty.Register<TopClient, string>(
        nameof(SubTitle), "infos");

}