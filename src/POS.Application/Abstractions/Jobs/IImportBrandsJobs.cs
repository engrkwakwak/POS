namespace POS.Application.Abstractions.Jobs;

public interface IImportBrandsJobs
{
    Task ExecuteAsync(string fileIdentifier, CancellationToken cancellationToken);
}
