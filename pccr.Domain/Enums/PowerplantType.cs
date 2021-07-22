using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace pccr.Domain.Enums
{
    public enum PowerplantType
    {
        [EnumMember(Value = "gasfired")]
        Gasfired,
        [EnumMember(Value = "turbojet")]
        Turbojet,
        [EnumMember(Value = "windturbine")]
        Windturbine
    }
}
