using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace WeatherInfoTool.Tests
{
    public class ProgramTests
    {
        [Fact]
        public async Task Main_ValidCity_ReturnsWeatherInfo()
        {
            // Arrange
            string[] args = { "New York" };
            var consoleOut = new StringWriter();
            Console.SetOut(consoleOut);

            // Act
            await Program.Main(args);

            string output = consoleOut.ToString();
            string[] lines = output.Trim().Split(Environment.NewLine);

            // Assert
            Assert.Equal(3, lines.Length);
            Assert.Contains("Temperature:", lines[0]);
            Assert.Contains("Wind Speed:", lines[1]);
            Assert.Contains("Weather Description:", lines[2]);
        }

        [Fact]
        public async Task Main_InvalidCity_ReturnsErrorMessage()
        {
            // Arrange
            string[] args = { "Nonexistent City" };
            var consoleOut = new StringWriter();
            Console.SetOut(consoleOut);

            // Act
            await Program.Main(args);

            string output = consoleOut.ToString().Trim();

            // Assert
            Assert.Equal("City 'Nonexistent City' not found in data file.", output);
        }

        [Fact]
        public async Task Main_NoArguments_ReturnsErrorMessage()
        {
            // Arrange
            string[] args = { };
            var consoleOut = new StringWriter();
            Console.SetOut(consoleOut);

            // Act
            await Program.Main(args);

            string output = consoleOut.ToString().Trim();

            // Assert
            Assert.Equal("Please provide a city as an argument.", output);
        }

        [Fact]
        public async Task Main_InvalidCapitalization_ReturnsWeatherInfo()
        {
            // Arrange
            string[] args = { "new york" };
            var consoleOut = new StringWriter();
            Console.SetOut(consoleOut);

            // Act
            await Program.Main(args);

            string output = consoleOut.ToString();
            string[] lines = output.Trim().Split(Environment.NewLine);

            // Assert
            Assert.Equal(3, lines.Length);
            Assert.Contains("Temperature:", lines[0]);
            Assert.Contains("Wind Speed:", lines[1]);
            Assert.Contains("Weather Description:", lines[2]);
        }

        [Fact]
        public async Task Main_NullOrEmptyArgument_ReturnsErrorMessage()
        {
            // Arrange
            string[] args = { null };
            var consoleOut = new StringWriter();
            Console.SetOut(consoleOut);

            // Act
            await Program.Main(args);

            string output = consoleOut.ToString().Trim();

            // Assert
            Assert.Equal("Please provide a city as an argument.", output);

            // Arrange
            args = new string[] { };
            consoleOut = new StringWriter();
            Console.SetOut(consoleOut);

            // Act
            await Program.Main(args);

            output = consoleOut.ToString().Trim();

            // Assert
            Assert.Equal("Please provide a city as an argument.", output);
        }

        [Fact]
        public async Task Main_NonStringArgument_ReturnsErrorMessage()
        {
            // Arrange
            int arg = 123;
            var consoleOut = new StringWriter();
            Console.SetOut(consoleOut);

            // Act
            await Program.Main(new[] { arg.ToString() });

            string output = consoleOut.ToString().Trim();

            // Assert
            Assert.Equal("Please provide a city as an argument.", output);
        }

        [Fact]
        public async Task Main_SpecialCharacters_ReturnsWeatherInfo()
        {
            // Arrange
            string[] args = { "Los Angeles*" };
            var consoleOut = new StringWriter();
            Console.SetOut(consoleOut);

            // Act
            await Program.Main(args);

            string output = consoleOut.ToString();
            string[] lines = output.Trim().Split(Environment.NewLine);

            // Assert
            Assert.Equal(3, lines.Length);
            Assert.Contains("Temperature:", lines[0]);
            Assert.Contains("Wind Speed:", lines[1]);
            Assert.Contains("Weather Description:", lines[2]);
        }

        [Fact]
        public async Task Main_ApiUnavailable_ReturnsErrorMessage()
        {
            // Arrange
            string[] args = { "New York" };
            var consoleOut = new StringWriter();
            Console.SetOut(consoleOut);

            // Act
            await Program.Main(args);

            string output = consoleOut.ToString();
            string[] lines = output.Trim().Split(Environment.NewLine);

            // Assert
            Assert.Equal(3, lines.Length);
            Assert.Contains("Temperature:", lines[0]);
            Assert.Contains("Wind Speed:", lines[1]);
            Assert.Contains("Weather Description:", lines[2]);
        }
    }
}