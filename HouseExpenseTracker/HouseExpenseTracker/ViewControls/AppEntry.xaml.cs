using System.Runtime.CompilerServices;

namespace HouseExpenseTracker.ViewControls;

public partial class AppEntry : ContentView
{
    public AppEntry()
    {
        InitializeComponent();
    }

    #region Title
    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set
        {
            SetValue(TitleProperty, value);
        }
    }

    public static readonly BindableProperty TitleProperty =
        BindableProperty.Create(nameof(Title), typeof(string), typeof(AppEntry), "Title");
    #endregion

    #region Placeholder
    public string Placeholder
    {
        get => (string)GetValue(PlaceHolderProperty);
        set
        {
            SetValue(PlaceHolderProperty, value);
        }
    }

    public static readonly BindableProperty PlaceHolderProperty =
        BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(AppEntry), "Enter a description");
    #endregion

    #region TextTransform
    public TextTransform TextTransform
    {
        get => (TextTransform)GetValue(TextTransformProperty);
        set
        {
            SetValue(TextTransformProperty, value);
        }
    }

    public static readonly BindableProperty TextTransformProperty =
        BindableProperty.Create(nameof(TextTransform), typeof(TextTransform), typeof(AppEntry), TextTransform.None);
    #endregion

    #region FocusColor
    public Color FocusColor
    {
        get => (Color)GetValue(FocusColorProperty);
        set
        {
            SetValue(FocusColorProperty, value);
        }
    }

    public static readonly BindableProperty FocusColorProperty =
        BindableProperty.Create(nameof(FocusColor), typeof(Color), typeof(AppEntry));
    #endregion

    #region BorderColor
    public Color BorderColor
    {
        get => (Color)GetValue(BorderColorProperty);
        set
        {
            SetValue(BorderColorProperty, value);
        }
    }

    public static readonly BindableProperty BorderColorProperty =
        BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(AppEntry));
    #endregion

    #region BackgroundColor
    public new Color BackgroundColor
    {
        get => (Color)GetValue(BackgroundColorProperty);
        set
        {
            SetValue(BackgroundColorProperty, value);
        }
    }

    public static readonly new BindableProperty BackgroundColorProperty =
        BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(AppEntry));
    #endregion

    #region TextColor
    public Color TextColor
    {
        get => (Color)GetValue(TextColorProperty);
        set
        {
            SetValue(TextColorProperty, value);
        }
    }

    public static readonly BindableProperty TextColorProperty =
        BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(AppEntry));
    #endregion

    #region Keyboard
    public static readonly BindableProperty KeyboardProperty = BindableProperty.Create(nameof(Keyboard), typeof(Keyboard), typeof(InputView), Keyboard.Default,
            coerceValue: (o, v) => (Keyboard)v ?? Keyboard.Default);

    [System.ComponentModel.TypeConverter(typeof(Microsoft.Maui.Converters.KeyboardTypeConverter))]
    public Keyboard Keyboard
    {
        get => (Keyboard)GetValue(KeyboardProperty);
        set => SetValue(KeyboardProperty, value);
    }
    #endregion

    #region Text
    public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(AppEntry), defaultBindingMode: BindingMode.TwoWay);

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }
    #endregion
}