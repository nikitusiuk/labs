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
            Assert.True(calculator.CalculateExpression("2"));
            Assert.True(calculator.CalculateExpression("+2"));
            Assert.True(calculator.CalculateExpression("*3"));
            Assert.Equal(new List<int> { 2, 4, 12 }, calculator.GetMem());
            Assert.Equal("12", calculator.GetLastMem());
            Assert.True(calculator.CalculateExpression("sj"));
            Assert.True(calculator.CalculateExpression("sx"));
            Assert.True(calculator.CalculateExpression("ss"));
            Assert.True(calculator.CalculateExpression("#2"));
            Assert.Equal("4", calculator.GetLastMem());
            Assert.True(calculator.CalculateExpression("/2"));
            Assert.True(calculator.CalculateExpression("-1"));
            Assert.Equal("1", calculator.GetLastMem());
            Assert.True(calculator.CalculateExpression("+0"));
            Assert.True(calculator.CalculateExpression("lj"));
            Assert.Equal(new List<int> { 2, 4, 12 }, calculator.GetMem());
            Assert.True(calculator.CalculateExpression("+2"));
            Assert.Equal("14", calculator.GetLastMem());
            Assert.True(calculator.CalculateExpression("lx"));
            Assert.Equal(new List<int> { 2, 4, 12 }, calculator.GetMem());
            Assert.True(calculator.CalculateExpression("+3"));
            Assert.Equal("15", calculator.GetLastMem());
            Assert.True(calculator.CalculateExpression("ls"));
            Assert.Equal(new List<int> { 2, 4, 12 }, calculator.GetMem());
            Assert.True(calculator.CalculateExpression("-1"));
            Assert.Equal("11", calculator.GetLastMem());

        }
    }
}
