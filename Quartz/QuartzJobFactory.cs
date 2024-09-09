using Quartz.Spi;
using Quartz;

namespace FleetPulse_BackEndDevelopment.Quartz
{
    public class QuartzJobFactory : IJobFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public QuartzJobFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            var jobType = bundle.JobDetail.JobType;
            return (IJob)_serviceProvider.GetService(jobType)!;
        }

        public void ReturnJob(IJob job) { }
    }
}