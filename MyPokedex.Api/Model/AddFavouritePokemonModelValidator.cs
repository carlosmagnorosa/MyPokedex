using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPokedex.Api.Model
{
    public class AddFavouritePokemonModelValidator : AbstractValidator<AddFavouritePokemonModel>
    {
        public AddFavouritePokemonModelValidator()
        {
            RuleFor(x => x.PokemonName).NotEmpty();
            RuleFor(x => x.UserId).GreaterThan(0);
        }
    }
}
