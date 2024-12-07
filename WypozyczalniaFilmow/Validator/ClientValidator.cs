using FluentValidation;
using WypozyczalniaFilmow.Models;
using WypozyczalniaFilmow.Database; // Twoja klasa DbContext

namespace WypozyczalniaFilmow.Validators
{
    public class ClientValidator : AbstractValidator<Client>
    {
        public ClientValidator(AppDbContext dbContext)
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email jest wymagany.")
                .EmailAddress().WithMessage("Nieprawidłowy format adresu e-mail.")
                .Custom((value, context) =>
                {
                    // Sprawdzanie unikalności emaila
                    var emailExists = dbContext.Persons.OfType<Client>().Any(c => c.Email == value);
                    if (emailExists)
                    {
                        context.AddFailure("Email", "Podany adres e-mail już istnieje w bazie.");
                    }
                });

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Imię jest wymagane.")
                .MaximumLength(50).WithMessage("Imię nie może przekraczać 50 znaków.");

            RuleFor(x => x.Surname)
                .NotEmpty().WithMessage("Nazwisko jest wymagane.")
                .MaximumLength(50).WithMessage("Nazwisko nie może przekraczać 50 znaków.");

            RuleFor(x => x.PhoneNumber)
                .GreaterThan(0).WithMessage("Numer telefonu musi być dodatnią liczbą.")
                .LessThan(999999999).WithMessage("Numer telefonu nie może mieć więcej niż 9 cyfr.")
                .Custom((value, context) =>
                {
                    // Sprawdzanie unikalności numeru telefonu
                    var phoneExists = dbContext.Persons.OfType<Client>().Any(c => c.PhoneNumber == value);
                    if (phoneExists)
                    {
                        context.AddFailure("PhoneNumber", "Podany numer telefonu już istnieje w bazie.");
                    }
                });
        }
    }
}
