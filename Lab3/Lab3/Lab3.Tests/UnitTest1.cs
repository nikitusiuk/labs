using System;
using System.Collections.Generic;
using Xunit;

namespace Lab3.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {            
            Calculator calculator = new Calculator();
            Assert.True(calculator.Calculate("2"));
            Assert.True(calculator.Calculate("+"));
            Assert.True(calculator.Calculate("2"));
            Assert.True(calculator.Calculate("*"));
            Assert.True(calculator.Calculate("3"));
            Assert.Equal(new List<int> { 2, 4, 12 }, calculator.GetMem());
            Assert.Equal("12", calculator.GetLastMem());
            Assert.True(calculator.Calculate("sj"));
            Assert.True(calculator.Calculate("sx"));
            Assert.True(calculator.Calculate("ss"));
            Assert.True(calculator.Calculate("#2"));
            Assert.Equal("4", calculator.GetLastMem());
            Assert.True(calculator.Calculate("/"));
            Assert.True(calculator.Calculate("2"));
            Assert.True(calculator.Calculate("-"));
            Assert.True(calculator.Calculate("1"));
            Assert.Equal("1", calculator.GetLastMem());
            Assert.True(calculator.Calculate("+"));
            Assert.True(calculator.Calculate("lj"));
            Assert.Equal(new List<int> { 2, 4, 12 }, calculator.GetMem());
            Assert.True(calculator.Calculate("+"));
            Assert.True(calculator.Calculate("2"));
            Assert.Equal("14", calculator.GetLastMem());
            Assert.True(calculator.Calculate("lx"));
            Assert.Equal(new List<int> { 2, 4, 12 }, calculator.GetMem());
            Assert.True(calculator.Calculate("+"));
            Assert.True(calculator.Calculate("3"));
            Assert.Equal("15", calculator.GetLastMem());
            Assert.True(calculator.Calculate("ls"));
            Assert.Equal(new List<int> { 2, 4, 12 }, calculator.GetMem());
            Assert.True(calculator.Calculate("-"));
            Assert.True(calculator.Calculate("1"));
            Assert.Equal("11", calculator.GetLastMem());


        }
    }
}
