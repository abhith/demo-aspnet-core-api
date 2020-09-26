using Microsoft.Extensions.DependencyInjection;
using Azakaw.Complaints.Domain.AggregatesModel.ComplaintAggregate;
using Azakaw.Complaints.Infrastructure.Repositories;

namespace Azakaw.Complaints.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IComplaintRepository, ComplaintRepository>();

            return services;
        }
    }
}