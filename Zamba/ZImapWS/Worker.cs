
using Zamba.MailKit;
using static Org.BouncyCastle.Math.EC.ECCurve;
using static Zamba.MailKit.EmailService;

namespace ZImapWS
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                try { 
                    imapConfig iconfig = new imapConfig();
                    EmailService EC = new EmailService();
                    EC.RetrieveEmails(iconfig);
                }
                catch (Exception ex)
                {
                    _logger.LogInformation(ex.ToString(), DateTimeOffset.Now);
                }

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}