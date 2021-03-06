﻿using System.Collections.Generic;

namespace CustomModbusSlave.Es2gClimatic.Shared.ParameterPresentation
{
    internal sealed class ParameterDescriptionSimple : IParameterDescription
    {
        public string Key { get; set; }

        public string Identifier { get; set; }

        public string CustomName { get; set; }

        public IParameterView View { get; set; }

        public IList<IParameterEvent> Events { get; set; }

        public IParameterInjectionConfiguration Injection { get; set; }
    }
}