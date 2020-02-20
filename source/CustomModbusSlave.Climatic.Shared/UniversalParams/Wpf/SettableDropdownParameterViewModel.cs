using System.Collections.Generic;
using AlienJust.Support.Collections;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.Conversion.Contracts;
using CustomModbus.Slave.FastReply.Contracts;

namespace CustomModbusSlave.Es2gClimatic.Shared.UniversalParams.Wpf
{
    public class SettableDropdownParameterViewModel<TRaw> : SendableParameterBase<TRaw> where TRaw : struct
    {
        public List<IRawAndConvertedValues<TRaw, string>> DropItems { get; }

        public SettableDropdownParameterViewModel(
            int paramIndex,
            string name,
            TRaw? defaultValue,
            IBuilderOneToOne<TRaw, string> stringBuilder,
            List<IRawAndConvertedValues<TRaw, string>> dropItems,
            string toolTipText,
            IParameterSetter parameterSetter,
            IThreadNotifier uiNotifier,
            IBuilderOneToOne<TRaw, BytesPair> sendConverter)
            : base(paramIndex, name, defaultValue, stringBuilder, toolTipText, parameterSetter, uiNotifier, sendConverter)
        {
            DropItems = dropItems;
        }
    }
}