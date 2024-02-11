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
    BindableProperty.Create(nameof(ItemsSource), typeof(IList), typeof(AppPicker), default(IList));
    public IList ItemsSource
    {
        get { return (IList)GetValue(ItemsSourceProperty); }
        set { SetValue(ItemsSourceProperty, value); }
    }
    #endregion

    #region SelectedItem
    public static readonly BindableProperty SelectedItemProperty =
            BindableProperty.Create(nameof(SelectedItem), typeof(object), typeof(AppPicker), null, BindingMode.TwoWay);
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

    private void Picker_SelectedIndexChanged(object sender, EventArgs e)
    {
        SelectedItemCommand?.Execute(SelectedItem);
    }

    #endregion
}