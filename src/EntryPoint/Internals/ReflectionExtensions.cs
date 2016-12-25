using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint.Exceptions;
using System.Reflection;

namespace EntryPoint.Internals {
    internal static class ReflectionExtensions {
        internal static BaseOptionAttribute GetOptionDefinition(this PropertyInfo prop) {
            var attributes = prop.GetCustomAttributes<BaseOptionAttribute>().ToList();
            if (attributes.Count > 1) {
                throw new InvalidModelException(
                    $"More than one Option attribute was applied to {prop.Name}");
            }
            if (!attributes.Any()) {
                return null;
            }
            return attributes.First();
        }

        internal static bool OptionIsRequired(this PropertyInfo prop) {
            return prop.GetCustomAttribute<OptionRequiredAttribute>() != null;
        }

        internal static DocumentationAttribute GetDocumentation(this PropertyInfo prop) {
            return prop.GetCustomAttribute<DocumentationAttribute>() 
                ?? new DocumentationAttribute();
        }
    }
}
