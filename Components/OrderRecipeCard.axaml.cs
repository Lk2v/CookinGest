using System;
using System.Data.Common;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Input;
using Avalonia.Interactivity;
using CookinGest.src.DataTemplate;

namespace CookinGest.Components;
public partial class OrderRecipeCard : TemplatedControl
{

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


    public static readonly StyledProperty<string> TitleProperty =
        AvaloniaProperty.Register<OrderRecipeCard, string>(nameof(Title));

    public static readonly StyledProperty<string> DescriptionProperty =
        AvaloniaProperty.Register<OrderRecipeCard, string>(nameof(Description));

    public static readonly StyledProperty<string> ProductsListProperty =
        AvaloniaProperty.Register<OrderRecipeCard, string>(nameof(ProductsList));

    public static readonly StyledProperty<decimal> PriceProperty =
        AvaloniaProperty.Register<OrderRecipeCard, decimal>(nameof(Price));


    public string Title
    {
        get => GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public string Description
    {
        get => GetValue(DescriptionProperty);
        set => SetValue(DescriptionProperty, value);
    }

    public string ProductsList
    {
        get => GetValue(ProductsListProperty);
        set => SetValue(ProductsListProperty, value);
    }

    public decimal Price
    {
        get => GetValue(PriceProperty);
        set => SetValue(PriceProperty, value);
    }

    public static readonly DirectProperty<OrderRecipeCard, ICommand> CommandProperty =
        AvaloniaProperty.RegisterDirect<OrderRecipeCard, ICommand>(
            nameof(Command),
            (OrderRecipeCard button) => button.Command,
            delegate (OrderRecipeCard button, ICommand c) {
                button.Command = c;
            }, null, BindingMode.OneWay, enableDataValidation: true);

    public static readonly StyledProperty<object> CommandParameterProperty = AvaloniaProperty.Register<OrderRecipeCard, object>(nameof(CommandParameter));

}