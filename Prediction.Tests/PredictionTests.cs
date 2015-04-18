using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Prediction.Tests
{
    [TestFixture]
    public class PredictionTests
    {
        [Test]
        public void shouldReturnArray()
        {
            var predObj = new Prediction();
            Assert.IsInstanceOf<Prediction>(predObj);
        }
    }
}
