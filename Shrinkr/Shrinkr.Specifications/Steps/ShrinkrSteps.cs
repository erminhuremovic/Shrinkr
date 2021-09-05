using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Newtonsoft.Json;
using Shouldly;
using Shrinkr.Dal;
using Shrinkr.Dto;
using TechTalk.SpecFlow;
using Xunit;

namespace Shrinkr.Specifications.Steps
{
    [Binding]
    public sealed class ShrinkrSteps : IClassFixture<WebApplicationFactory<Startup>>
    {
        /// <summary>
        /// The base address of the test server is http://localhost
        /// </summary>
        private const string baseAddress = "http://localhost";
        private readonly WebApplicationFactory<Startup> webApplicationFactory;
        private readonly Mock<IDatabase> mockDatabase = new Mock<IDatabase>();
        private HttpClient client;
        private HttpResponseMessage httpResponse;

        public ShrinkrSteps(WebApplicationFactory<Startup> webApplicationFactory)
        {
            this.webApplicationFactory = webApplicationFactory
                .WithWebHostBuilder(
                builder => builder.ConfigureTestServices(
                    services =>
                    {
                        services.AddSingleton(this.mockDatabase.Object);
                    }));
        }

        [Given(@"the Shrinkr service is running")]
        public void GivenTheShrinkrServiceIsRunning()
        {
            // Create a load data access layer mocks here

            this.client = this.webApplicationFactory.CreateClient(new WebApplicationFactoryClientOptions
            {
                BaseAddress = new Uri(baseAddress)
            });
            this.client.DefaultRequestHeaders.Add("Origin", baseAddress);
        }

        [When(@"I provide the Url; ""(.*)""")]
        public async Task WhenIProvideTheUrl(string longUrl)
        {
            var relativeUri = new Uri("generate", UriKind.Relative);
            var serializedItem = JsonConvert.SerializeObject(longUrl);
            var content = new StringContent(serializedItem, Encoding.UTF8, "application/json");
            this.httpResponse = await this.client.PostAsync(relativeUri, content);
        }

        [Then(@"I will receive a shortened Url")]
        public async Task ThenIWillReceiveAShortenedUrl()
        {
            var body = await this.httpResponse.Content.ReadAsStringAsync();

            // The length is made up of http://localhost/12345678
            // 9 additional characters come from /12345678
            body.Length.ShouldBe(baseAddress.Length + 9, $"Body is actually {body}.");
        }

        [Then(@"the Url; ""(.*)"" is added to the database")]
        public void ThenTheUrlIsAddedToTheDatabase(string longUrl)
        {
            this.mockDatabase.Verify(x => x.Add(It.Is<string>(x => x.Length == 8), longUrl), Times.Once);
        }

        [Given(@"the following records are present in the database")]
        public void GivenTheFollowingRecordsArePresentInTheDatabase(Table table)
        {
            var urlMappings = new List<UrlMapping>();
            foreach (var row in table.Rows)
            {
                urlMappings.Add(
                    new UrlMapping
                    {
                        Token = row["Token"],
                        LongUrl = row["Long Url"]
                    });
            }

            this.mockDatabase.Setup(x => x.UrlMappings).Returns(urlMappings);
        }


        [When(@"I navigate to ""(.*)""")]
        public async Task WhenINavigateTo(string shortUrl)
        {
            this.httpResponse = await this.client.GetAsync(shortUrl);
        }

        [Then(@"I will be redirected to ""(.*)""")]
        public void ThenIWillBeRedirectedTo(string longUrl)
        {
            this.httpResponse.RequestMessage.RequestUri.ToString().ShouldBe(longUrl);
        }
    }
}
