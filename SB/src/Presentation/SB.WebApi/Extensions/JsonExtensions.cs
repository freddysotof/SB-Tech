﻿using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Text.Json;

namespace SB.WebApi.Extensions
{
    public static class JsonExtensions
    {
        public static bool TryParseJson<T>(this string @this, out T result)
        {
            bool success = true;
            var settings = new JsonSerializerSettings
            {
                Error = (sender, args) => { success = false; args.ErrorContext.Handled = true; },
                MissingMemberHandling = MissingMemberHandling.Error
            };
            result = JsonConvert.DeserializeObject<T>(@this, settings);
            return success;
        }

        


    }
}
