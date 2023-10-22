using BoDi;
using System.Reflection;
using ApiTest.Common.Exceptions;
using Microsoft.Extensions.Configuration;
using ApiTest.Specflow.Configuration.Models;
using ApiTest.Specflow.Drivers;
using ApiTest.Specflow.Drivers.Contracts;
using Xunit.Abstractions;

namespace ApiTest.Specflow.Configuration
{
    public static class DependencyInjection
    {
        private const string ConfigurationSectionName = "AppSettings";

        public static IObjectContainer AddConfiguration(this IObjectContainer objectContainer)
        {
            var configurationName = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyConfigurationAttribute>()!.Configuration;
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").AddJsonFile($"appsettings.{configurationName}.json").Build();

            var environment = new TestEnvironment(configuration.GetSection(ConfigurationSectionName)[$"{nameof(TestEnvironment)}"] ??
                                      throw new ConfigurationItemNotFoundException(nameof(TestEnvironment)));
            var baseUrl = new BaseUrl(configuration.GetSection(ConfigurationSectionName)[$"{nameof(BaseUrl)}"] ??
                                      throw new ConfigurationItemNotFoundException(nameof(BaseUrl)));

            var credentials = new Credentials
            {
                Username = configuration.GetSection(ConfigurationSectionName)[$"{nameof(Credentials.Username)}"] ??
                           throw new ConfigurationItemNotFoundException(nameof(Credentials.Username)),
                Password = configuration.GetSection(ConfigurationSectionName)[$"{nameof(Credentials.Password)}"] ??
                           throw new ConfigurationItemNotFoundException(nameof(Credentials.Password))
            };

            objectContainer.RegisterInstanceAs(environment);
            objectContainer.RegisterInstanceAs(baseUrl);
            objectContainer.RegisterInstanceAs(credentials);

            return objectContainer;
        }

        public static IObjectContainer AddApiClients(this IObjectContainer objectContainer)
        {
            var testOutputHelper = objectContainer.Resolve<ITestOutputHelper>();
            var baseUrl = objectContainer.Resolve<BaseUrl>();
            var credentials = objectContainer.Resolve<Credentials>();

            var bookingDriver = new BookingDriver(testOutputHelper, baseUrl.Value, credentials.Username, credentials.Password);

            objectContainer.RegisterInstanceAs<IBookingDriver>(bookingDriver);

            return objectContainer;
        }
    }
}
