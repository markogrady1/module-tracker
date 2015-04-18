using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace OOP._0._1._1.tests
{
    class PredictionTests
    {
        [Test]
        public void shouldReturnArray()
        {
            var predObj = new Prediction();
            Assert.IsInstanceOf<Prediction>(predObj);
        }

        [Test]
        public void shouldReturnNameOfModule()
        {
            var predObj = new Prediction();

            string modName = predObj.GetModuleName(GetStringArray());
            Assert.That(modName, Is.EqualTo("Web tech"));
        }

        [Test]
        public void shouldReturnModuleAssessmentAmount()
        {
            var predObj = new Prediction();

            int assessmentAmount = predObj.GetAmountOfAssessments(GetStringArray());

            Assert.That(assessmentAmount, Is.EqualTo(2));
        }

        [Test]
        public void shouldReturnArrayOfIntegers()
        {
            var predObj = new Prediction();


            int[] assessValues = predObj.GetAssessmentGrades(GetStringArray());

            Assert.That(assessValues.Length, Is.EqualTo(8));
            Assert.That(assessValues[0], Is.EqualTo(56));
        }

        [Test]
        public void shouldReturnGrade_and_weight_of_FirstAssessment()
        {
            var predObj = new Prediction();
            predObj.modulePrediction(GetModuleString());
            predObj.ResolveAllResults();
            float actual = predObj.AvgOne;
            Assert.That(actual, Is.EqualTo(24.5));
        }

       
        [Test]
        public void shouldReturnGrade_and_weight_of_SecondAssessment()
        {
            var predObj = new Prediction();
            predObj.modulePrediction(GetModuleString());
            predObj.ResolveAllResults();
            float actual = predObj.AvgTwo;
            Assert.That(actual, Is.EqualTo(20.0));
        }

        [Test]
        public void shouldReturnGrade_and_weight_of_ThirdAssessment()
        {
            var predObj = new Prediction();
            predObj.modulePrediction(GetModuleString());
            predObj.ResolveAllResults();
            float actual = predObj.AvgThree;
            Assert.That(actual, Is.EqualTo(19.25));
           
        }

        [Test]
        public void shouldReturnGrade_and_weight_of_FourthAssessment()
        {
            var predObj = new Prediction();
            predObj.modulePrediction(GetModuleString());
            predObj.ResolveAllResults();
            float actual = predObj.AvgFour;
            Assert.That(actual, Is.EqualTo(24.75));
        }

        [Test]
        public void shouldReturnNull_for_FifthAssessment()
        {
            var predObj = new Prediction();
            int[] assessValues = predObj.GetAssessmentGrades(GetStringArray());
            int[] assess = predObj.GetAssessmentDetails(5, assessValues);
            Assert.That(assess, Is.EqualTo(null));
        }

        [Test]
        public void shouldReturn_totalOfAllGrades_as_average()
        {
            var predObj = new Prediction();
            predObj.modulePrediction(GetModuleString());
            predObj.ResolveAllResults();
            float actual = predObj.getModuleTotal();
            Assert.That(actual, Is.EqualTo(88));
        }
        [Test]
        public void shouldReturn_totalOfAllGrades_average_Of_TwoGrades()
        {
            var predObj = new Prediction();
            predObj.modulePrediction("402, web tech, ecsc34, 452, four, 2, 87, 50, 77, 50, 77, 25, 99, 25");
            predObj.ResolveAllResults();
            float actual = predObj.getModuleTotal();
            Assert.That(actual, Is.EqualTo(82));
        }

        [Test]
        public void shouldReturn_totalOfAllGrades_average_Of_ThreeGrades()
        {
            var predObj = new Prediction();
            predObj.modulePrediction("402, web tech, ecsc34, 452, four, 3, 72, 30, 100, 35, 93, 35, 99, 25");
            predObj.ResolveAllResults();
            float actual = predObj.getModuleTotal();
            Assert.That(actual, Is.EqualTo(89));
        }

        public string[] GetStringArray()
        {
            string[] moduleData =
            {
                "402", "Web tech", "ecsc023", "452", "four", 
                "2", "56", "25", "66", "25", "77", "25",
                "99", "25"
            };
            return moduleData;
        }

        private string GetModuleString()
        {
            return "402, web tech, ecsc34, 452, four, 4, 98, 25, 80, 25, 77, 25, 99, 25";
        }
        [Test]
        public void getAvg_from_grade_and_weight()
        {
            var predObj = new Prediction();
            
            string modValues = "402, web tech, ecsc34, 452, four, 1, 56, 25, 66, 25, 77, 25, 99, 25";
            predObj.modulePrediction(modValues);
            int[] val = {56, 25};
            float newVal = predObj.calculateGrade(val);
            Assert.That(Math.Round(newVal), Is.EqualTo(14.0));


           
        }
    }

   
}


















