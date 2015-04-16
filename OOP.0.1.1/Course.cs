using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP._0._1._1
{
    class Course
    {
        private string _courseDatabaseId;
        private string _courseCode;
        private string _courseName;

        public string CourseName
        {
            get { return _courseName; }
            set { _courseName = value; }
        }

        public string CourseDatabaseId
        {
            get { return _courseDatabaseId; }
            set { _courseDatabaseId = value; }
        }

        public string CourseCode
        {
            get { return _courseCode; }
            set { _courseCode = value; }
        }
    }
}
