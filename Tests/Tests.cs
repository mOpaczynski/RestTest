using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using Shouldly;

namespace Tests
{
    public class Tests
    {
        [Test]
        public void TestPoint_Get()
        {
            var baseUrl = "http://localhost:50652";
            var endPoint = "api/testpoint";

            var client = new RestClient(baseUrl);
            var request = new RestRequest(endPoint, Method.GET);

            var response = client.Execute(request);

            var responseDeserialized = JsonConvert.DeserializeObject<string[]>(response.Content);

            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            responseDeserialized.ShouldSatisfyAllConditions(
                () => responseDeserialized[0].ShouldBe("value1"),
                () => responseDeserialized[1].ShouldBe("value2"),
                () => responseDeserialized[2].ShouldBe("value3"),
                () => responseDeserialized[3].ShouldBe("banana"));

            Console.WriteLine(response.Content);
        }

        [Test]
        public void TestPoint_Get_Id()
        {
            var baseUrl = "http://localhost:50652";
            var endPoint = "api/testpoint/{id}";
            var randomNumber = 123123123;

            var client = new RestClient(baseUrl);
            var request = new RestRequest(endPoint, Method.GET);
            request.AddUrlSegment("id", randomNumber);

            var response = client.Execute(request);

            var responseDeserialized = JsonConvert.DeserializeObject<string>(response.Content);

            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            responseDeserialized.ShouldBe("Your get number is: " + randomNumber);

            Console.WriteLine(response.Content);
        }

        [Test]
        public void TestPoint_Post()
        {
            var baseUrl = "http://localhost:50652";
            var endPoint = "api/testpoint";
            var someValue = "KEKEKEKEKEKEKE";
            var bookValue = "BOOOOOOOOKKK";

            var client = new RestClient(baseUrl);
            var request = new RestRequest(endPoint, Method.POST);


            ////request.AddHeader("Authentication", "Basic (*8938778347843578547854784578543");
            ////request.AddHeader("content type", "object/json");

            ////request.AddJsonBody(new
            ////{
            ////    Something = "String",
            ////    BABABA = 121212
            ////});

            request.AddParameter("value", someValue, ParameterType.QueryString);
            request.AddParameter("book", bookValue, ParameterType.QueryString);

            var response = client.Execute(request);

            var responseDeserialized = JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Content);

            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            responseDeserialized.ShouldSatisfyAllConditions(
                () => responseDeserialized["Something"].ShouldBe("AnotherThing"),
                () => responseDeserialized["YourValue"].ShouldBe(someValue),
                () => responseDeserialized["YourOtherValue"].ShouldBe("This is your value, but different: " + bookValue),
                () => responseDeserialized["Number"].ShouldBe("42"));

            Console.WriteLine(response.Content);
        }
    }
}
