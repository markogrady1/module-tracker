using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
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

        //Constructor
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
            username = username.ToLower();
            coursename = coursename.ToLower();
            string query = "INSERT INTO course (coursename, coursecode,username) VALUES('" + coursename + "','ECSC3454', '" + username + "')";

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

        //Update statement
        public void Update()
        {
        }

        //Delete statement
        public void Delete()
        {
        }

        //Select statement
        public List<string> SelectExistingCourses()
        {
            string query = "SELECT * FROM course";

            //Create a list to store the result
            List<string> list = new List<string>();


            MySqlCommand cmd = new MySqlCommand(query, connection);
            //Create a data reader and Execute the command 
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

        //Backup
        public void Backup()
        {
        }

        //Restore
        public void Restore()
        {
        }

        public List<string> GetAvailableCourses()
        {
            string query = "SELECT * FROM available_course";

            //Create a list to store the result
            List<string> availableCourses = new List<string>();

            MySqlCommand cmd = new MySqlCommand(query, connection);
            //Create a data reader and Execute the command 
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
                        MessageBox.Show(dataReader["courseId"] + "");
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

        public void InsertNewModule(string moduleName, string moduleCode, string moduleLevel, string moduleAssessmentAmount, string courseDBId)
        {
            string modName = moduleName.ToLower().Trim();
            string modCode = moduleCode.ToLower().Trim();
            string pattern = "Level ";
            Regex rgx = new Regex(pattern);
            string newLevelStr = rgx.Replace(moduleLevel, "");
            string modLevel = newLevelStr.ToLower().Trim();
            string modAssessAmount = moduleAssessmentAmount.Trim();
            string courseId = courseDBId;

            string query = "INSERT INTO module(moduleName, moduleCode, courseId,  level, assessmentAmount)" +
                           " values('" + modName + "','" + modCode + "'," + courseId + ",'" + modLevel + "'," + modAssessAmount + ");";
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
                            dataReader["assess4"] 
                           
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
            string query = "SELECT * FROM module WHERE courseId=" + courseID + ";";
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
                            dataReader["assessmentAmount"] + ""
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
            string[] modData = moduledata;
            string pattern = "Level: ";
            Regex rgx = new Regex(pattern);
            string newLevelStr = rgx.Replace(modData[2], "");
            string modLevel = newLevelStr.ToLower().Trim();
            string courseID = courseId;
            MessageBox.Show(courseID);
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
                           "WHERE courseId=" + courseID + " AND level='" + modLevel + "';";
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
    }
}
