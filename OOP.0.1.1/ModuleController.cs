using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP._0._1._1
{
    class ModuleController
    {
        private StartUp startUp;
        private Database db;
        private string _courseId;

        public ModuleController(StartUp startup)
        {
            this.startUp = startup;
            db = new Database();
        }

        public string CourseId
        {
            get { return _courseId; }
            set { _courseId = value; }
        }

        public void CreateNewModule(CourseController cc, string username, string coursename, string moduleName, string moduleCode, string moduleLevel, string moduleAssessmentAmount, string moduleCredit)
        {
            //string courseID = db.GetId(username, coursename, "course");
            this._courseId = cc.getCourseDbId();
            Module module = new Module();
            module.CourseDatabaseId = this._courseId;
            module.ModuleCode = moduleCode;
            string pattern = "Level ";
            Regex rgx = new Regex(pattern);
            string newLevelStr = rgx.Replace(moduleLevel, "");
            module.ModuleLevel = newLevelStr;
            module.ModuleAssessmentAmount = moduleAssessmentAmount;
            db.OpenConnection();
            db.InsertNewModule(moduleName, moduleCode, moduleLevel, moduleAssessmentAmount, moduleCredit, _courseId);
            getAllModulesByLevel();
        }

        public void setCourseId(CourseController cc)
        {
            this._courseId = cc.getCourseDbId();
        }

        public void resetAllModules()
        {
            getAllModulesByLevel();
        }
        public void getAllModulesByLevel()
        {
            db.OpenConnection();
            List<string> levelFourModules = db.getModulesByLevel(this._courseId, "four");
            db.OpenConnection();
            List<string> levelFiveModules = db.getModulesByLevel(this._courseId, "five");
            db.OpenConnection();
            List<string> levelSixModules = db.getModulesByLevel(this._courseId, "six");
            //foreach (string line in levelFourModules)
            //{
            //MessageBox.Show(line);
            if (levelFourModules.Count != 0)
            {
                startUp.setLevel("four", levelFourModules);
            }
            else
            {
                startUp.LevelFourCleanUp();
            }

            if (levelFiveModules.Count != 0)
            {
                startUp.setLevel("five", levelFiveModules);
            }
            else
            {
                startUp.LevelFiveCleanUp();
            }

            if (levelSixModules.Count != 0)
            {
                startUp.setLevel("six", levelSixModules);
            }
            else
            {
                startUp.LevelSixCleanUp();
            }

            //}

            //foreach (string line in levelFiveModules)
            //{
            //    MessageBox.Show(line);
            //    //startUp.setLevel("four", levelFourModules);
            //}

            //foreach (string line in levelSixModules)
            //{
            //    MessageBox.Show(line);
            //    //startUp.setLevel("four", levelFourModules);
            //}

        }

        public List<string> resolveAllModules()
        {

            db.OpenConnection();
            List<string> modList = db.getAllModules(_courseId);

            return modList;
        }

        public List<string> resolveAllDegreeModules(string level)
        {
            db.OpenConnection();
            List<string> modList = db.getAllFinalModules(_courseId, level);
            //prediction.CalculateDegree(courseList);
            return modList;

        }

        public int Level6Outcome(Prediction prediction, List<int> list)
        {
            Prediction predict = prediction;
            int A = predict.CalculateLevelSix(list);

            return A;
        }
        public int Level5Outcome(Prediction prediction, List<int> list)
        {
            Prediction predict = prediction;
            int B = predict.CalculateLevelFive(list);

            return B;
        }

        public string GetFinalDegree(int a, int b)
        {
            Prediction pred = new Prediction();
            string result = pred.FinalDegreeResult(a, b);

            return result;
        }

        //public int getAverage(List<string> list)
        //{
        //    Prediction pred = new Prediction();
        //    int result = pred.FinalAvgResult(list);

        //    return result;
        //}
        public int getAvg(List<int> fours)
        {
            int tot = 0;
            foreach (var list in fours)
            {
                tot += list;
            }
            if (fours.Count != 0)
            {
                tot = tot / fours.Count;
                return tot;
            }
            

            return 0;
        }

        public void removeModule(string moduleId)
        {
            string moduleID = moduleId;
            db.OpenConnection();
            db.DeleteModule( moduleID);

        }
    }
}
