using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP._0._1._1
{
    class User
    {
        private string _name;
        private string _currentCourseId;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string CurrentCourseId
        {
            get { return _currentCourseId; }
            set { _currentCourseId = value; }
        }
    }
}
