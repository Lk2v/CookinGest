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
public partial class HighlightData : TemplatedControl
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

    private ICommand _command;

    public ICommand Command
    {
        get
        {
            return _command;
        }
        set
        {

            SetAndRaise(CommandProperty, ref _command, value);
        }
    }

    public object CommandParameter
    {
        get
        {
            return GetValue(CommandParameterProperty);
        }
        set
        {
            SetValue(CommandParameterProperty, value);
        }
    }

    public bool Disabled
    {
        get => _command == null;
    }
    // Property


    public static readonly StyledProperty<string> TitleProperty = AvaloniaProperty.Register<HighlightData, string>(
        nameof(Title), "<Tab-Name>");

    public static readonly StyledProperty<string> ValueProperty = AvaloniaProperty.Register<HighlightData, string>(
        nameof(Value), "(null)");

    public static readonly StyledProperty<bool> DisabledProperty = AvaloniaProperty.Register<HighlightData, bool>(
        nameof(Disabled), false);

    public static readonly DirectProperty<HighlightData, ICommand> CommandProperty =
        AvaloniaProperty.RegisterDirect<HighlightData, ICommand>(
            nameof(Command),
            (HighlightData button) => button.Command,
            delegate (HighlightData button, ICommand c) {
                button.Command = c;
            }, null, BindingMode.OneWay, enableDataValidation: true);

    public static readonly StyledProperty<object> CommandParameterProperty = AvaloniaProperty.Register<HighlightData, object>(nameof(CommandParameter));

    

    
    // Constructor

    public HighlightData()
    {
        
    }

}