using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            string query = "INSERT INTO course (coursename, coursecode,username) VALUES('"+coursename+"','ECSC3454', '"+username+"')";

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


            //close connection
            //this.CloseConnection();

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


            //Open connection

            //Create Command

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
                    //MessageBox.Show(list[1]);
                    //close Data Reader
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


            //Open connection

            //Create Command

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
                    //MessageBox.Show(list[1]);
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
            string query = "SELECT id FROM "+ table + " WHERE coursename='" + coursename + "' AND username='" + username + "';";
            String resultString = null;
            MySqlCommand cmd = new MySqlCommand(query, connection);
            try
            {
                MySqlDataReader dataReader = cmd.ExecuteReader();
                try
                {
                    while (dataReader.Read())
                    {
                        resultString = dataReader["id"]+"";
                        MessageBox.Show(resultString);

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
    }
}
