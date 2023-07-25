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
public partial class IconButton : TemplatedControl
{

    public string Icon
    {
        get => GetValue(IconProperty);
        set => SetValue(IconProperty, "fa-solid " + value);
    }

    public int IconSize
    {
        get => GetValue(IconSizeProperty);
        set => SetValue(IconSizeProperty, value);
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

    // Property

    public static readonly StyledProperty<string> IconProperty = AvaloniaProperty.Register<IconButton, string>(
        nameof(Icon), "fa-house");

    public static readonly StyledProperty<int> IconSizeProperty = AvaloniaProperty.Register<IconButton, int>(
        nameof(Icon), 16);

    public static readonly DirectProperty<IconButton, ICommand> CommandProperty =
        AvaloniaProperty.RegisterDirect<IconButton, ICommand>(
            nameof(Command),
            (IconButton button) => button.Command,
            delegate (IconButton button, ICommand c) {
                button.Command = c;
            }, null, BindingMode.OneWay, enableDataValidation: true);

    public static readonly StyledProperty<object> CommandParameterProperty = AvaloniaProperty.Register<IconButton, object>(nameof(CommandParameter));
}