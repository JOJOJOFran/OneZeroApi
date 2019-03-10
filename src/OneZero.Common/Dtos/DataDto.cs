using Newtonsoft.Json;
using OneZero.Common.Convert;
using System;
using System.Collections.Generic;
using System.Text;

namespace OneZero.Common.Dtos
{
    [JsonConverter(typeof(GuidJsonConvert))]
    public class DataDto
    {

    }
}
