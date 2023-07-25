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
public partial class BankBalance : TemplatedControl
{
    public int Value
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

    // Property


    public static readonly StyledProperty<int> ValueProperty = AvaloniaProperty.Register<BankBalance, int>(
        nameof(Value), 0);

    public static readonly DirectProperty<BankBalance, ICommand> CommandProperty =
        AvaloniaProperty.RegisterDirect<BankBalance, ICommand>(
            nameof(Command),
            (BankBalance button) => button.Command,
            delegate (BankBalance button, ICommand c) {
                button.Command = c;
            }, null, BindingMode.OneWay, enableDataValidation: true);

    public static readonly StyledProperty<object> CommandParameterProperty = AvaloniaProperty.Register<BankBalance, object>(nameof(CommandParameter));
}