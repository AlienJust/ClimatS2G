﻿namespace CustomModbusSlave.Es2gClimatic.Shared.ParameterPresentation
{
    internal sealed class ParameterDescriptionSimple : IParameterDescription
    {
        public string Key { get; set; }

        public string Identifier { get; set; }

        public string CustomName { get; set; }

        public IParameterView View { get; set; }
    }
}