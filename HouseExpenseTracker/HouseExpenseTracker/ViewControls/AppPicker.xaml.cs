using BindableProps;
using HouseExpenseTracker.Models;
using System.Collections;
using System.Windows.Input;

namespace HouseExpenseTracker.ViewControls;

public partial class AppPicker : ContentView
{
    public AppPicker()
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
        BindableProperty.Create(nameof(Title), typeof(string), typeof(AppPicker), "Title");
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
        BindableProperty.Create(nameof(FocusColor), typeof(Color), typeof(AppPicker));
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
        BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(AppPicker));
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
        BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(AppPicker));
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
        BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(AppPicker));
    #endregion

    #region ItemsSource
    public static readonly BindableProperty ItemsSourceProperty =
    BindableProperty.Create(nameof(ItemsSource), typeof(IList), typeof(AppPicker), default(IList),
        propertyChanged: (bindable, oldValue, newValue) =>
        {
            if (newValue is IList<PickerItemDto> source)
            {
                if (!source.Any(x => x.Id == 0))
                {
                    ((AppPicker)bindable).ItemsSource.Insert(0, new PickerItemDto()
                    {
                        Id = 0,
                        Name = "Select"
                    });
                }
            }
        });
    public IList ItemsSource
    {
        get { return (IList)GetValue(ItemsSourceProperty); }
        set { SetValue(ItemsSourceProperty, value); }
    }
    #endregion

    #region SelectedItem
    public static readonly BindableProperty SelectedItemProperty =
            BindableProperty.Create(nameof(SelectedItem), typeof(object), typeof(AppPicker), null, BindingMode.TwoWay,
                propertyChanged: (bindable, prevVal, newVal) =>
                {
                    if (newVal is PickerItemDto currVal && currVal != null && currVal.Id == 0)
                    {
                        ((AppPicker)bindable).SelectedItem = null;
                    }
                });
    public object SelectedItem
    {
        get { return GetValue(SelectedItemProperty); }
        set { SetValue(SelectedItemProperty, value); }
    }
    #endregion

    #region SelectedItemCommand
    public static readonly BindableProperty SelectedItemCommandProperty = BindableProperty.Create(
    nameof(SelectedItemCommand), typeof(ICommand), typeof(AppPicker), null);

    public ICommand SelectedItemCommand
    {
        get { return (ICommand)GetValue(SelectedItemCommandProperty); }
        set { SetValue(SelectedItemCommandProperty, value); }
    }

    #endregion

    #region ValidatorErrors
    public static readonly BindableProperty ValidatorErrorsProperty = BindableProperty.Create(nameof(ValidatorErrors), typeof(IDictionary<string, IEnumerable<string>>), typeof(AppPicker), defaultBindingMode: BindingMode.OneWay,
        propertyChanged: (appPicker, oldVal, newVal) =>
        {
            var val = (IDictionary<string, IEnumerable<string>>)newVal;
            var picker = (AppPicker)appPicker;
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

}

public class CustomPicker : Picker
{
    #region CursorColor

    public static readonly BindableProperty CursorColorProperty =
        BindableProperty.Create(nameof(CursorColor), typeof(Color), typeof(CustomPicker));

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
