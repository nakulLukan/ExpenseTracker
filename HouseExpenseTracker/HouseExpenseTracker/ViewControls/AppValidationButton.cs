using HouseExpenseTracker.Utilities.Validation;
using System.Windows.Input;

namespace HouseExpenseTracker.ViewControls;

public class AppValidationButton<T> : Button
    where T : class
{
    #region SubmitCommand
    public ICommand SubmitCommand
    {
        get => (ICommand)GetValue(SubmitCommandProperty);
        set
        {
            SetValue(SubmitCommandProperty, value);
        }
    }

    public static readonly BindableProperty SubmitCommandProperty =
        BindableProperty.Create(nameof(SubmitCommand), typeof(ICommand), typeof(AppValidationButton<T>), defaultBindingMode: BindingMode.OneTime);
    #endregion

    #region Validation
    public AppAbstractValidator<T> Validation
    {
        get => (AppAbstractValidator<T>)GetValue(ValidationProperty);
        set
        {
            SetValue(ValidationProperty, value);
        }
    }

    public static readonly BindableProperty ValidationProperty =
        BindableProperty.Create(nameof(Validation), typeof(AppAbstractValidator<T>), typeof(AppValidationButton<T>), defaultBindingMode: BindingMode.OneTime);
    #endregion

    #region Data
    public T Data
    {
        get => (T)GetValue(DataProperty);
        set
        {
            SetValue(DataProperty, value);
        }
    }

    public static readonly BindableProperty DataProperty =
        BindableProperty.Create(nameof(Data), typeof(T), typeof(AppValidationButton<T>), defaultBindingMode: BindingMode.OneWay);
    #endregion

    public AppValidationButton()
    {
        Clicked += AppValidationButton_Clicked;
    }

    private void AppValidationButton_Clicked(object? sender, EventArgs e)
    {
        if (Validation != null)
        {
            _ = Validation.Validate(Data);
            if (Validation.IsValid)
            {
                SubmitCommand.Execute(CommandParameter);
            }
        }
        else
        {
            SubmitCommand.Execute(CommandParameter);
        }
    }

    ~AppValidationButton()
    {
        Clicked -= AppValidationButton_Clicked;
    }
}
