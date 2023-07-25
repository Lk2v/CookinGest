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
public partial class IconButtonBig : TemplatedControl
{
    public string Title
    {
        get => GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    

    public string Icon
    {
        get => GetValue(IconProperty);
        set => SetValue(IconProperty, "fa-solid " + value);
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


    public static readonly StyledProperty<string> TitleProperty = AvaloniaProperty.Register<IconButtonBig, string>(
        nameof(Title), "<Tab-Name>");

    public static readonly StyledProperty<string> IconProperty = AvaloniaProperty.Register<IconButtonBig, string>(
        nameof(Icon), "fa-house");

    public static readonly DirectProperty<IconButtonBig, ICommand> CommandProperty =
        AvaloniaProperty.RegisterDirect<IconButtonBig, ICommand>(
            nameof(Command),
            (IconButtonBig button) => button.Command,
            delegate (IconButtonBig button, ICommand c) {
                button.Command = c;
            }, null, BindingMode.OneWay, enableDataValidation: true);

    public static readonly StyledProperty<object> CommandParameterProperty = AvaloniaProperty.Register<IconButtonBig, object>(nameof(CommandParameter));
}