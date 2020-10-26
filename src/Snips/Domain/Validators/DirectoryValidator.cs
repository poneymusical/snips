using FluentValidation;
using Snips.Domain.BusinessObjects;

namespace Snips.Domain.Validators
{
    public class DirectoryValidator : AbstractValidator<Directory>
    {
        public DirectoryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();

            RuleFor(x => x.Name)
                .NotEmpty();
        }
    }
}