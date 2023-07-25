using System;
using System.Data.Common;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Input;
using Avalonia.Interactivity;

namespace CookinGest.Components;
public partial class InfoItem : TemplatedControl
{
    public string Title
    {
        get => GetValue(TitleProperty);
        set => SetValue(TitleProperty, value.ToUpper());
    }

    public string Value
    {
        get => GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    public static readonly StyledProperty<string> TitleProperty = AvaloniaProperty.Register<InfoItem, string>(
        nameof(Title), "Name");

    public static readonly StyledProperty<string> ValueProperty = AvaloniaProperty.Register<InfoItem, string>(
        nameof(Value), "(null)");

   
}