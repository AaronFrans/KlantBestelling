using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests
{
    [TestClass]
    public class RestTests
    {
        [TestMethod]
        public void MapperTest_ToRestObject()
        {
            Action act = () => Mapper.ToProductType("Bla");
            act.Should().Throw<RestException>().WithMessage("Het gegeven product bestaat niet");

            act = () => Mapper.ToProductType("Leffe");
            act.Should().NotThrow<RestException>();
        }


    }
}
