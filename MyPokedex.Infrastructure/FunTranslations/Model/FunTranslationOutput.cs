﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPokedex.Infrastructure.FunTranslations.Model
{
    public record FunTranslationOutput
    {
        public Contents Contents { get; init; }
    }
}
