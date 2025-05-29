using FluentValidation;
using MonkeyShelter.Core.DTOs;
using MonkeyShelter.Core.Interfaces;

namespace MonkeyShelter.API.FluentValidator
{
    public class CreateSpeciesDtoValidator : AbstractValidator<CreateSpeciesDto>
    {
        public CreateSpeciesDtoValidator(ISpeciesRepository speciesRepository) 
        {
            RuleFor(s => s.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name is max 100 character long.")
                .MustAsync(async (name, cancellationToken) =>
                    !await speciesRepository.ExistsSpecies(name))
                .WithMessage("Species with the same name already exists.");
        }
    }
}
