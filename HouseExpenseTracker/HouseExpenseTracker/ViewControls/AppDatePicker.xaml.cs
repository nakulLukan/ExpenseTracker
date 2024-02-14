using BindableProps;

namespace HouseExpenseTracker.ViewControls;

public partial class AppDatePicker : ContentView
{
    public AppDatePicker()
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
        BindableProperty.Create(nameof(Title), typeof(string), typeof(AppDatePicker), "Title");
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
        BindableProperty.Create(nameof(FocusColor), typeof(Color), typeof(AppDatePicker));
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
        BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(AppDatePicker));
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
        BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(AppDatePicker));
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
        BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(AppDatePicker));
    #endregion

    #region Format
    public static readonly BindableProperty FormatProperty = BindableProperty.Create(nameof(Format), typeof(string), typeof(AppDatePicker), "ddd, dd MMM yyyy");

    public string Format
    {
        get { return (string)GetValue(FormatProperty); }
        set { SetValue(FormatProperty, value); }
    }
    #endregion

    #region Date
    public static readonly BindableProperty DateProperty = BindableProperty.Create(nameof(Date), typeof(DateTime), typeof(AppDatePicker), default(DateTime), BindingMode.TwoWay,
            propertyChanged: null,
            defaultValueCreator: (bindable) => DateTime.Today);
    public DateTime Date
    {
        get { return (DateTime)GetValue(DateProperty); }
        set { SetValue(DateProperty, value); }
    }

    #endregion

    #region WidthRequest
    public static readonly new BindableProperty WidthRequestProperty = BindableProperty.Create(nameof(WidthRequest), typeof(double), typeof(AppDatePicker), -1d);

    public new double WidthRequest
    {
        get { return (double)GetValue(WidthRequestProperty); }
        set { SetValue(WidthRequestProperty, value); }
    }

    #endregion

    #region ValidatorErrors
    public static readonly BindableProperty ValidatorErrorsProperty = BindableProperty.Create(nameof(ValidatorErrors), typeof(IDictionary<string, IEnumerable<string>>), typeof(AppDatePicker), defaultBindingMode: BindingMode.OneWay,
        propertyChanged: (appDatePicker, oldVal, newVal) =>
        {
            var val = (IDictionary<string, IEnumerable<string>>)newVal;
            var picker = (AppDatePicker)appDatePicker;
            picker.IsValid = !val.ContainsKey(picker.ValidationPropertyName);
            if (val.ContainsKey(picker.ValidationPropertyName))
            {
                picker.Errors = val[picker.ValidationPropertyName];
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

    private void DatePicker_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(Date))
        {
            Focus();
        }
    }
}

public class CustomDatePicker : DatePicker
{
}