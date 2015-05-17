using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace OOP._0._1._1
{
    public class Database
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;
 
        public Database()
        {
            Initialize();
        }

        //Initialize values
        private void Initialize()
        {
            server = "127.0.0.1";
            database = "oop";
            uid = "root";
            password = "";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
                               database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            connection = new MySqlConnection();
            connection.ConnectionString = connectionString;
        }

        //open connection to database
        public bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        //close connection to database
        public bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        //Insert statement
        public void InsertNewCourse(string username, string coursename)
        {
            DateTime time = DateTime.Now;
                string mysqlDateFormat = time.ToString("yyyy-MM-dd HH:mm");
            

            username = username.ToLower();
            coursename = coursename.ToLower();
            string query = "INSERT INTO course (coursename, created_at,username) VALUES('" + coursename + "','" + mysqlDateFormat + "', '" + username + "')";

            //create command and assign the query and connection from the constructor
            MySqlCommand cmd = new MySqlCommand(query, connection);
            try
            {
                //Execute command
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.Message);
            }


        }

        

        //Delete statement
        public void DeleteModule( string id)
        {
            string moduleID = id.Trim();
            string query = "DELETE FROM module WHERE moduleId =" + moduleID + ";";

            //create command and assign the query and connection from the constructor
            MySqlCommand cmd = new MySqlCommand(query, connection);
            try
            {
                //Execute command
                cmd.ExecuteNonQuery();
                CloseConnection();
            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.Message);
                CloseConnection();
            }

        }

        //Select method
        public List<string> SelectExistingCourses()
        {
            string query = "SELECT * FROM course";

            List<string> list = new List<string>();


            MySqlCommand cmd = new MySqlCommand(query, connection);
            //Create a data reader and Execute the c ommand 
            try
            {
                MySqlDataReader dataReader = cmd.ExecuteReader();
                try
                {
                    //Read the data and store them in the list
                    while (dataReader.Read())
                    {
                        String resultString = dataReader["username"] + ", " + dataReader["coursename"];
                        list.Add(resultString);
                    }

                    dataReader.Close();

                    //close Connection
                    this.CloseConnection();

                    //return list to be displayed
                    return list;
                }
                catch (MySqlException ex)
                {
                    return null;
                }
            }
            catch (MySqlException e)
            {
                return null;
            }


        }

        //Count statement
        public int Count()
        {
            return 0;
        }

        public List<string> GetAvailableCourses()
        {
            string query = "SELECT * FROM available_courses";

            List<string> availableCourses = new List<string>();

            MySqlCommand cmd = new MySqlCommand(query, connection);
            try
            {
                MySqlDataReader dataReader = cmd.ExecuteReader();
                try
                {
                    //Read the data and store them in the list
                    while (dataReader.Read())
                    {
                        availableCourses.Add(dataReader["coursename"] + "");
                    }

                    //close Data Reader
                    dataReader.Close();

                    //close Connection
                    this.CloseConnection();

                    //return list to be displayed
                    return availableCourses;
                }
                catch (MySqlException ex)
                {
                    return null;
                }
            }
            catch (MySqlException e)
            {
                return null;
            }
        }

        public string GetId(string username, string coursename, string table)
        {
            username = username.ToLower().Trim();
            coursename = coursename.ToLower().Trim();
            
            string query = "SELECT courseId FROM " + table + " WHERE coursename='" + coursename + "' AND username='" + username + "';";
            String resultString = null;
            MySqlCommand cmd = new MySqlCommand(query, connection);
            try
            {
                MySqlDataReader dataReader = cmd.ExecuteReader();
                try
                {
                    while (dataReader.Read())
                    {
                        resultString = dataReader["courseId"] + "";
                    }
                    dataReader.Close();
                    CloseConnection();
                    return resultString;
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message);
                    return null;
                }
            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.Message);
                return null;
            }
        }

        public void InsertNewModule(string moduleName, string moduleCode, string moduleLevel, string moduleAssessmentAmount,string moduleCredit, string courseDBId)
        {

            string modName = moduleName.ToLower().Trim();
            string modCode = moduleCode.ToLower().Trim();
            string pattern = "Level ";
            Regex rgx = new Regex(pattern);
            string newLevelStr = rgx.Replace(moduleLevel, "");
            string modLevel = newLevelStr.ToLower().Trim();
            string modAssessAmount = moduleAssessmentAmount.Trim();
            string modCredit = moduleCredit.Trim();
            string courseId = courseDBId;

            string query = "INSERT INTO module(moduleName, moduleCode, courseId,  level, assessmentAmount, credit)" +
                           " values('" + modName + "','" + modCode + "'," + courseId + ",'" + modLevel + "'," + modAssessAmount + ","+modCredit+");";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            try
            {
                //Execute command
                cmd.ExecuteNonQuery();
                CloseConnection();
            }
            catch (MySqlException e)
            {
                CloseConnection();
            }
        }

        public List<String> getModulesByLevel(string courseId, string level)
        {
            
            string courseID = courseId;
            string modLevel = level.ToLower().Trim();
            string query = "SELECT * FROM module WHERE courseId=" + courseID + " AND level='" + modLevel + "';";
            List<string> modulesList = new List<string>();

            MySqlCommand cmd = new MySqlCommand(query, connection);

            try
            {
                MySqlDataReader dataReader = cmd.ExecuteReader();
                try
                {
                    while (dataReader.Read())
                    {

                        modulesList.Add(
                            dataReader["moduleId"] + "," +
                            dataReader["moduleName"] + "," +
                            dataReader["moduleCode"] + "," +
                            dataReader["courseId"] + "," +
                            dataReader["level"] + "," +
                            dataReader["assessmentAmount"] + "," +
                            dataReader["assess1"] + "," +
                            dataReader["assess2"] + "," +
                            dataReader["assess3"] + "," +
                            dataReader["assess4"] + "," +
                            dataReader["credit"]
                            );
                    }
                    dataReader.Close();
                    CloseConnection();
                    return modulesList;
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message);
                    CloseConnection();
                    return null;
                }
            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.Message);
                CloseConnection();
                return null;
            }
        }

        public List<String> getAllModules(string courseId)
        {
            
            String courseID = courseId;
            string query = "SELECT * FROM module WHERE courseId=" + courseID + " ";
            List<string> modulesList = new List<string>();

            MySqlCommand cmd = new MySqlCommand(query, connection);

            try
            {
                MySqlDataReader dataReader = cmd.ExecuteReader();
                try
                {
                   
                    while (dataReader.Read())
                    {

                        modulesList.Add(
                            dataReader["moduleId"] + "," +
                            dataReader["moduleName"] + "," +
                            dataReader["moduleCode"] + "," +
                            dataReader["courseId"] + "," +
                            dataReader["level"] + "," +
                            dataReader["assessmentAmount"] + "," +
                            //grades and weights for module
                            dataReader["assess1"] + "," +
                            dataReader["assess1Weight"] + "," +
                            dataReader["assess2"] + "," +
                            dataReader["assess2Weight"] + "," +
                            dataReader["assess3"] + "," +
                            dataReader["assess3Weight"] + "," +
                            dataReader["assess4"] + "," +
                            dataReader["assess4Weight"]+ "," +
                            dataReader["credit"]
                            
                            );
                    }
                    dataReader.Close();
                    CloseConnection();
                    return modulesList;
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message);
                    CloseConnection();
                    return null;
                }
            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.Message);
                CloseConnection();
                return null;
            }
        }

        public void UpdateModuleAssessments(string[] moduledata, string courseId, string[] assessArray)
        {
            string modulename = moduledata[0].ToLower().Trim();
            string[] modData = moduledata;
            string pattern = "Level: ";
            Regex rgx = new Regex(pattern);
            string newLevelStr = rgx.Replace(modData[2], "");
            string modLevel = newLevelStr.ToLower().Trim();
            string courseID = courseId;
            string[] assessmentArr = assessArray;

            string query = "UPDATE module " +
                           "SET assess1=" + assessmentArr[0] + ", " +
                           "assess1Weight=" + assessmentArr[1] + ", " +
                           "assess2=" + assessmentArr[2] + ", " +
                           "assess2Weight=" + assessmentArr[3] + ", " +
                           "assess3=" + assessmentArr[4] + ", " +
                           "assess3Weight=" + assessmentArr[5] + ", " +
                           "assess4=" + assessmentArr[6] + ", " +
                           "assess4Weight=" + assessmentArr[7] + " " +
                           "WHERE moduleName='" + modulename+"' AND courseId=" + courseID + " AND level='" + modLevel + "';";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            try
            {
                cmd.ExecuteNonQuery();
                CloseConnection();
            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.Message);
                CloseConnection();
            }





        }

        public List<string> getCourseDetails(string id)
        {
            string query = "SELECT * FROM course WHERE  courseId="+id+";";

            List<string> list = new List<string>();


            MySqlCommand cmd = new MySqlCommand(query, connection);
            try
            {
                MySqlDataReader dataReader = cmd.ExecuteReader();
                try
                {
                    while (dataReader.Read())
                    {
                        String resultString = dataReader["username"] + ", " + dataReader["coursename"];
                        list.Add(resultString);
                    }

                    dataReader.Close();

                    this.CloseConnection();

                    return list;
                }
                catch (MySqlException ex)
                {
                    return null;
                }
            }
            catch (MySqlException e)
            {
                return null;
            }


        }

        public List<string> getAllDegreeModules(string courseId)
        {
            String courseID = courseId;
            string query = "SELECT * FROM module WHERE courseId=" + courseID + " AND  level='five' OR   level='six' ;";
            List<string> modulesList = new List<string>();

            MySqlCommand cmd = new MySqlCommand(query, connection);

            try
            {
                MySqlDataReader dataReader = cmd.ExecuteReader();
                try
                {

                    while (dataReader.Read())
                    {

                        modulesList.Add(
                            dataReader["moduleId"] + "," +
                            dataReader["moduleName"] + "," +
                            dataReader["moduleCode"] + "," +
                            dataReader["courseId"] + "," +
                            dataReader["level"] + "," +
                            dataReader["assessmentAmount"] + "," +
                            //grades and weights for module
                            dataReader["assess1"] + "," +
                            dataReader["assess1Weight"] + "," +
                            dataReader["assess2"] + "," +
                            dataReader["assess2Weight"] + "," +
                            dataReader["assess3"] + "," +
                            dataReader["assess3Weight"] + "," +
                            dataReader["assess4"] + "," +
                            dataReader["assess4Weight"] + "," +
                            dataReader["credit"]

                            );
                    }
                    dataReader.Close();
                    CloseConnection();
                    return modulesList;
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message);
                    CloseConnection();
                    return null;
                }
            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.Message);
                CloseConnection();
                return null;
            }
        }

        public List<String> getAllFinalModules(string courseId, string  level)
        {

            String courseID = courseId;
            string query = "SELECT * FROM module WHERE courseId=" + courseID + " AND level = '"+level+"' ";
            List<string> modulesList = new List<string>();

            MySqlCommand cmd = new MySqlCommand(query, connection);

            try
            {
                MySqlDataReader dataReader = cmd.ExecuteReader();
                try
                {

                    while (dataReader.Read())
                    {

                        modulesList.Add(
                            dataReader["moduleId"] + "," +
                            dataReader["moduleName"] + "," +
                            dataReader["moduleCode"] + "," +
                            dataReader["courseId"] + "," +
                            dataReader["level"] + "," +
                            dataReader["assessmentAmount"] + "," +
                            //grades and weights for module
                            dataReader["assess1"] + "," +
                            dataReader["assess1Weight"] + "," +
                            dataReader["assess2"] + "," +
                            dataReader["assess2Weight"] + "," +
                            dataReader["assess3"] + "," +
                            dataReader["assess3Weight"] + "," +
                            dataReader["assess4"] + "," +
                            dataReader["assess4Weight"] + "," +
                            dataReader["credit"]

                            );
                    }
                    dataReader.Close();
                    CloseConnection();
                    return modulesList;
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message);
                    CloseConnection();
                    return null;
                }
            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.Message);
                CloseConnection();
                return null;
            }
        }
    }
}
