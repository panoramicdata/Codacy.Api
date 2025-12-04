using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Design", "CA1034:Nested types should not be visible", Justification = "Nested types used for logical grouping of related API operations")]
[assembly: SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Instance members required for interface implementation")]
[assembly: SuppressMessage("Design", "S2360:Optional parameters should not be used", Scope = "namespaceanddescendants", Target = "~N:Codacy.Api.Interfaces", Justification = "Optional parameters are required for Refit API interfaces to support optional query parameters")]
