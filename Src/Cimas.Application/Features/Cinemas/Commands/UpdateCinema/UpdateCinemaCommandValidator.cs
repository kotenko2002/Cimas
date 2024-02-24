﻿using FluentValidation;

namespace Cimas.Application.Features.Cinemas.Commands.UpdateCinema
{
    public class UpdateCinemaCommandValidator : AbstractValidator<UpdateCinemaCommand>
    {
        public UpdateCinemaCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty();

            RuleFor(x => x.CinemaId)
                .NotEmpty();

            RuleFor(x => x.Name)
                .NotEmpty()
                .MinimumLength(6);

            RuleFor(x => x.Adress)
                .NotEmpty()
                .MinimumLength(6);
        }
    }
}
