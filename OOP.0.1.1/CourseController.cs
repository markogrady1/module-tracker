using System.Collections.Generic;
using System.Threading;

namespace OOP._0._1._1
{
    class CourseController
    {
        private delegate void RunOnThreadPool(string username, string coursename);
        private Database db;
        private Course course;
        private User user;
        private string courseName, userName;
        public CourseController()
        {
            db = new Database();
        }
      
        /*inject the dependencies of this class to loosen the coupling*/
        public void setDependencies(Course course, User user)
        {
            this.course = course;
            this.user = user;
        }

        /*set the given username and name of course to this instance and database*/
        public bool setData(string username, string courseName)
        {
            this.courseName = courseName;
            this.userName = username;
            course.CourseName = this.courseName;
            user.Name = this.userName;
           var res = addToDatabase(username, courseName);
            if (res)
                return true;
            else
                return false;
        }

        /*reset values of properties with existing data from previous course*/
        public bool setExistingData(string existingData)
        {
            string queryParameters = existingData;
            string[] strArray = queryParameters.Split(',');
            
            user.Name = strArray[0];
            course.CourseName = strArray[1];
            return true;
        }

        /*add a new course to database*/
        public bool addToDatabase(string username, string coursename)
        {
            
            var stat = db.OpenConnection();
            if (stat)
            {
                RunOnThreadPool poolDelegate = InsertData;
                var t = new Thread(() => InsertData(username, coursename));
                poolDelegate.Invoke(username, coursename);


                db.CloseConnection();
                return true;
            }
            else
            {
                return false;
            }
        }

        /*insert new course data into the database and add course id to course instance*/
        public void InsertData(string username, string coursename)
        {
            
            db.InsertNewCourse(username, coursename);
            string id = db.GetId(username, coursename, "course");
            course.CourseDatabaseId = id;
            
        }

        /*set new values to dependencies*/
        public void assignNewUser(string username, string coursename)
        {
            courseName = coursename;
            userName = username;
            course.CourseName = courseName;
            user.Name = userName;
        }

        /*find specific course id and set it to course instance*/
        public void MatchCourseData(string existingData)
        {
            string queryParameters = existingData;
            string[] strArray = queryParameters.Split(',');
            
            db.OpenConnection();
            string id =  db.GetId(strArray[0], strArray[1], "course");
            course.CourseDatabaseId = id;
            
        }

        /*return course id*/
        public string getCourseDbId()
        {
            return course.CourseDatabaseId;
        }

        /*return all the course data in the form of a list */
        public List<string> getCourseData(string courseDat)
        {
            db.OpenConnection();
            List<string>data = db.getCourseDetails(courseDat);
            return data;
        }

        public string UserName
        {
            get { return userName; }
        }

        public string CourseName
        {
            get { return courseName; }
        }
    }
}
