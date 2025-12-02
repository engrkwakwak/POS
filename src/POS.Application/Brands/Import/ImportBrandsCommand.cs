using POS.Application.Abstractions.Messaging;

namespace POS.Application.Brands.Import;

public sealed record ImportBrandsCommand(
    Stream FileStream,
    string FileName,
    string ContentType) : ICommand;
