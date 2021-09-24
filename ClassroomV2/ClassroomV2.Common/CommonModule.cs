using Autofac;
using ClassroomV2.Common.Utilities;
using Microsoft.Extensions.Configuration;


namespace ClassroomV2.Common
{
    public class CommonModule : Module
    {
        private IConfiguration _configuration;
        public CommonModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EmailService>().As<IEmailService>()
                .WithParameter("host", _configuration.GetSection("Email:host").Value)
                .WithParameter("port", _configuration.GetSection("Email:port").Value)
                .WithParameter("username", _configuration.GetSection("Email:username").Value)
                .WithParameter("password", _configuration.GetSection("Email:password").Value)
                .WithParameter("useSSL", _configuration.GetSection("Email:useSSL").Value)
                .WithParameter("from", _configuration.GetSection("Email:username").Value)
                .InstancePerLifetimeScope();
            
            base.Load(builder);
        }
    }
}
