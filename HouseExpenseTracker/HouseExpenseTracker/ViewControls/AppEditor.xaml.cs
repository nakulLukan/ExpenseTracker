using BindableProps;

namespace HouseExpenseTracker.ViewControls;

public partial class AppEditor : ContentView
{
    public AppEditor()
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
        BindableProperty.Create(nameof(Title), typeof(string), typeof(AppEditor), "Title");
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
        BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(AppEditor), "Enter a description");
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
        BindableProperty.Create(nameof(TextTransform), typeof(TextTransform), typeof(AppEditor), TextTransform.None);
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
        BindableProperty.Create(nameof(FocusColor), typeof(Color), typeof(AppEditor));
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
        BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(AppEditor));
    #endregion

    #region BackgroundColor
    public Color BackgroundColor
    {
        get => (Color)GetValue(BackgroundColorProperty);
        set
        {
            SetValue(BackgroundColorProperty, value);
        }
    }

    public static readonly BindableProperty BackgroundColorProperty =
        BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(AppEditor));
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
        BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(AppEditor));
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
    public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(AppEditor), defaultBindingMode: BindingMode.TwoWay);

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }
    #endregion

    #region ValidatorErrors
    public static readonly BindableProperty ValidatorErrorsProperty = BindableProperty.Create(nameof(ValidatorErrors), typeof(IDictionary<string, IEnumerable<string>>), typeof(AppEditor), defaultBindingMode: BindingMode.OneWay,
        propertyChanged: (appEntry, oldVal, newVal) =>
        {
            var val = (IDictionary<string, IEnumerable<string>>)newVal;
            var entry = (AppEditor)appEntry;
            entry.IsValid = !val.ContainsKey(entry.ValidationPropertyName);
            if (val.ContainsKey(entry.ValidationPropertyName))
            {
                entry.Errors = val[entry.ValidationPropertyName];
            }
        });

    public IDictionary<string, IEnumerable<string>> ValidatorErrors
    {
        get => (IDictionary<string, IEnumerable<string>>)GetValue(ValidatorErrorsProperty);
        set => SetValue(ValidatorErrorsProperty, value);
    }
    #endregion

    [BindableProp(DefaultBindingMode = (int)BindingMode.OneWay)]
    private bool _isValid = true;

    [BindableProp(DefaultBindingMode = (int)BindingMode.OneWay)]
    private IEnumerable<string> _errors;

    [BindableProp(DefaultBindingMode = (int)BindingMode.OneTime)]
    private string _validationPropertyName;
}

public class CustomEditor : Editor
{
    #region CursorColor

    public static readonly BindableProperty CursorColorProperty =
        BindableProperty.Create(nameof(CursorColor), typeof(Color), typeof(CustomEditor));

    public Color CursorColor
    {
        get => (Color)GetValue(CursorColorProperty);
        set
        {
            SetValue(CursorColorProperty, value);
        }
    }
    #endregion
}