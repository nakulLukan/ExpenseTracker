using FluentValidation;
using FluentValidation.Results;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HouseExpenseTracker.Utilities.Validation;

public class AppAbstractValidator<T> : AbstractValidator<T>, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    #region Errors
    private IDictionary<string, IEnumerable<string>> _errors;
    public IDictionary<string, IEnumerable<string>> Errors
    {
        get => _errors;
        set
        {
            _errors = value;
            OnPropertyChanged();
        }
    }
    #endregion Errors

    #region IsValid
    private bool _isValid = true;
    public bool IsValid
    {
        get => _isValid;
        set
        {
            _isValid = value;
            OnPropertyChanged();
        }
    }
    #endregion IsValid

    public override ValidationResult Validate(ValidationContext<T> context)
    {
        var validationResult = base.Validate(context);
        IsValid = validationResult.IsValid;
        Errors = validationResult.Errors
            .GroupBy(x => x.PropertyName)
            .Where(x => x.Any())
            .ToDictionary(x => x.Key, x => x.Select(x => x.ErrorMessage));
        return validationResult;
    }

    public void OnPropertyChanged([CallerMemberName] string name = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
