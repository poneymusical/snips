using FluentValidation;
using Snips.Domain.BusinessObjects;

namespace Snips.Domain.Validators
{
    public class SnippetValidator : AbstractValidator<Snippet>
    {
        public SnippetValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();

            RuleFor(x => x.Title)
                .NotEmpty();
        }
    }
}