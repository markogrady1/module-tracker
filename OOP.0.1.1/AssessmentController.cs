using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP._0._1._1
{
    class AssessmentController
    {
        private string _assess1,
            _assess1Weight,
            _assess2,
            _assess2Weight,
            _assess3,
            _assess3Weight,
            _assess4,
            _assess4Weight;

        private Course course;
        private Module module;
        private string _moduleDbIdValue;
        private string[] _moduleData;
        private ModuleController modController;

        public AssessmentController(ModuleController modController)
        {
            this.modController = modController;
        }

        public void setAssessmentGrades(string assessment1, string assessment1Weight,
            string assessment2, string assessment2Weight, string assessment3,
            string assessment3Weight, string assessment4, string assessment4Weight)
        {
            _assess1 = assessment1;
            _assess1Weight = assessment1Weight;
            _assess2 = assessment2;
            _assess2Weight = assessment2Weight;
            _assess3 = assessment3;
            _assess3Weight = assessment3Weight;
            _assess4 = assessment4;
            _assess4Weight = assessment4Weight;

        }

        public void setSpecificModule(string[] modData, string moduleDbId)
        {
            this._moduleData = modData;
            this._moduleDbIdValue = moduleDbId;
        }

        public void updateSystem()
        {
            string courseId = modController.CourseId;
            string[] assessmentArray = new string[8];
            assessmentArray[0] = _assess1;
            assessmentArray[1] = _assess1Weight;
            assessmentArray[2] = _assess2;
            assessmentArray[3] = _assess2Weight; 
            assessmentArray[4] = _assess3;
            assessmentArray[5] = _assess3Weight; 
            assessmentArray[6] = _assess4;
            assessmentArray[7] = _assess4Weight; 
            
            Database db = new Database();

            db.OpenConnection();
            db.UpdateModuleAssessments(_moduleData,courseId , assessmentArray);
        }
    }
}
