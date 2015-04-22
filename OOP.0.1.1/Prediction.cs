using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace OOP._0._1._1
{
    class Prediction
    {
        private string[] _completeModuleArray;
        private int _assessmentAmount;
        private int[] _moduleGrades;
        private float _avgOne;
        private float _avgTwo;
        private float _avgThree;
        private float _avgFour;
        private string _moduleName;
        private string _moduleCode;
        private string _courseId;

        public float AvgOne
        {
            get { return _avgOne; }
        }

        public float AvgTwo
        {
            get { return _avgTwo; }
        }

        public float AvgThree
        {
            get { return _avgThree; }
        }

        public float AvgFour
        {
            get { return _avgFour; }
        }

        public string ModuleName
        {
            get { return _moduleName; }
        }


        public string ModuleCode
        {
            get { return _moduleCode; }
        }

        public string CourseId
        {
            get { return _courseId; }
        }


        public int AssessmentAmount
        {
            get { return _assessmentAmount; }
        }

        public void modulePrediction(string moduleValues)
        {
            _completeModuleArray = moduleValues.Split(',');
            _moduleGrades = GetAssessmentGrades(_completeModuleArray);
            _assessmentAmount = GetAmountOfAssessments(_completeModuleArray);
            _courseId = _completeModuleArray[3];
            _moduleName = _completeModuleArray[1];
            _moduleCode = _completeModuleArray[2];
        }


        public string GetModuleName(string[] moduleData)
        {
            return moduleData[1];
        }

        public int GetAmountOfAssessments(string[] moduleData)
        {
            return Convert.ToInt32(moduleData[5]);
        }



        public int[] GetAssessmentGrades(string[] moduleData)
        {
            int[] assessmentGrades = new int[8];
            assessmentGrades[0] = Convert.ToInt32(moduleData[6]);
            assessmentGrades[1] = Convert.ToInt32(moduleData[7]);
            assessmentGrades[2] = Convert.ToInt32(moduleData[8]);
            assessmentGrades[3] = Convert.ToInt32(moduleData[9]);
            assessmentGrades[4] = Convert.ToInt32(moduleData[10]);
            assessmentGrades[5] = Convert.ToInt32(moduleData[11]);
            assessmentGrades[6] = Convert.ToInt32(moduleData[12]);
            assessmentGrades[7] = Convert.ToInt32(moduleData[13]);

            return assessmentGrades;
        }

        public void ResolveAllResults()
        {

            if (_assessmentAmount == 1)
            {
                int[] temp = { _moduleGrades[0], _moduleGrades[1] };
                _avgOne = calculateGrade(temp);
            }

            if (_assessmentAmount == 2)
            {
                int[] temp = { _moduleGrades[0], _moduleGrades[1] };
                int[] temp2 = { _moduleGrades[2], _moduleGrades[3] };
                _avgOne = calculateGrade(temp);
                _avgTwo = calculateGrade(temp2);
            }

            if (_assessmentAmount == 3)
            {
                int[] temp = { _moduleGrades[0], _moduleGrades[1] };
                int[] temp2 = { _moduleGrades[2], _moduleGrades[3] };
                int[] temp3 = { _moduleGrades[4], _moduleGrades[5] };
                _avgOne = calculateGrade(temp);
                _avgTwo = calculateGrade(temp2);
                _avgThree = calculateGrade(temp3);
            }

            if (_assessmentAmount == 4)
            {
                int[] temp = { _moduleGrades[0], _moduleGrades[1] };
                int[] temp2 = { _moduleGrades[2], _moduleGrades[3] };
                int[] temp3 = { _moduleGrades[4], _moduleGrades[5] };
                int[] temp4 = { _moduleGrades[6], _moduleGrades[7] };
                _avgOne = calculateGrade(temp);
                _avgTwo = calculateGrade(temp2);
                _avgThree = calculateGrade(temp3);
                _avgFour = calculateGrade(temp4);
            }
        }

        public int getModuleTotal()
        {
            if (_assessmentAmount == 1)
            {
                int total = (int)Math.Round(_avgOne);
                return total;
            }
            if (_assessmentAmount == 2)
            {
                int total = (int)(Math.Round(_avgOne + _avgTwo));
                return total;
            }
            if (_assessmentAmount == 3)
            {
                int total = (int)(Math.Round(_avgOne + _avgTwo + _avgThree));
                return total;
            }
            if (_assessmentAmount == 4)
            {
                int total = (int)(Math.Round(_avgOne + _avgTwo + _avgThree + _avgFour));
                return total;
            }
            return 0;
        }

        public float calculateGrade(int[] grade)
        {
            float actualGrade = grade[0];
            float weight = grade[1];

            float averageGrade = ((actualGrade / 100) * weight);
            return averageGrade;
        }

        public int[] GetAssessmentDetails(int val, int[] assessValues)
        {

            if (val == 1)
            {
                int[] assess = new int[2];
                assess[0] = assessValues[0];
                assess[1] = assessValues[1];
                return assess;
            }

            if (val == 2)
            {
                int[] assess = new int[4];
                assess[0] = assessValues[0];
                assess[1] = assessValues[1];
                assess[2] = assessValues[2];
                assess[3] = assessValues[3];
                return assess;
            }

            if (val == 3)
            {
                int[] assess = new int[6];
                assess[0] = assessValues[0];
                assess[1] = assessValues[1];
                assess[2] = assessValues[2];
                assess[3] = assessValues[3];
                assess[4] = assessValues[4];
                assess[5] = assessValues[5];
                return assess;
            }

            if (val == 4)
            {
                int[] assess = new int[8];
                assess[0] = assessValues[0];
                assess[1] = assessValues[1];
                assess[2] = assessValues[2];
                assess[3] = assessValues[3];
                assess[4] = assessValues[4];
                assess[5] = assessValues[5];
                assess[6] = assessValues[6];
                assess[7] = assessValues[7];
                return assess;
            }
            return null;
        }


        public void CalculateDegree(List<string> list)
        {
            List<int> fivesTotals = new List<int>();
            List<int> sixesTotals = new List<int>();
            
            int count = 0;
            foreach (var moduleData in list)
            {
                String[] moduleArray = moduleData.Split(',');

                if (moduleArray[4] == "five")
                {

                    modulePrediction(moduleData);
                    ResolveAllResults();
                    fivesTotals.Add(getModuleTotal());

                    count++;
                } 
                
                if (moduleArray[4] == "six")
                {

                    modulePrediction(moduleData);
                    ResolveAllResults();
                    sixesTotals.Add(getModuleTotal());

                    count++;
                }
            }
            foreach (var f in fivesTotals)
            {
                MessageBox.Show("five mod" + f);
            } 
            
            foreach (var g in sixesTotals)
            {
                MessageBox.Show("six mod" + g);
            }
        }
    }
}
