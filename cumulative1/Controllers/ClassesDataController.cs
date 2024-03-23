using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using cumulative1.Models;
using MySql.Data.MySqlClient;

namespace cumulative1.Controllers
{
    public class ClassesDataController : ApiController
    {
        // / The database context class which allows us to access our MySQL Database.
        private DbschoolContext School = new DbschoolContext();
        //This Controller Will access the classes table of our blog database.
        /// <summary>
        /// Returns a list of Classess in the system
        /// </summary>
        /// <example>GET api/ClassesData/ListCLasses</example>
        /// <returns>
        /// A list of CLasses (first names and last names)
        /// </returns>
        [HttpGet]

        public IEnumerable<Classes> ListClasses()
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from Classes";

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Classes
            List<Classes> Classes = new List<Classes> { };

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                int ClassId = (int)ResultSet["classid"];
                string ClassCode = ResultSet["classcode"].ToString();
                string TeacherId = ResultSet["teacherid"].ToString();
                string StartDate = ResultSet["startdate"].ToString();
                string FinishDate = ResultSet["finishdate"].ToString();
                string ClassName = ResultSet["classname"].ToString();

                Classes NewClasses = new Classes();
                NewClasses.ClassId = ClassId;
                NewClasses.ClassCode = ClassCode;
                NewClasses.TeacherId = TeacherId;
                NewClasses.StartDate = StartDate;
                NewClasses.FinishDate = FinishDate;
                NewClasses.ClassName = ClassName;


                //Add the Class Name to the List
                Classes.Add(NewClasses);
            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return the final list of class names
            return Classes;
        }


        /// <summary>
        /// Finds an class from the MySQL Database through an id. Non-Deterministic.
        /// </summary>
        /// <param name="id">The Class ID</param>
        /// <returns>Class object containing information about the teacher with a matching ID. Empty Class Object if the ID does not match any Classes in the system.</returns>
        /// <example>api/ClassData/FindClass/6 -> {Class Object}</example>
        /// <example>api/ClassData/FindClass/10 -> {Class Object}</example>
        [HttpGet]
        public Classes FindClasses(int id)
        {
            Classes NewClasses = new Classes();

            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from Classes where classid = @id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                int ClassId = (int)ResultSet["cllassid"];
                string ClassCode = ResultSet["classcode"].ToString();
                string TeacherId = ResultSet["teacherid"].ToString();
                string StartDate = ResultSet["startdate"].ToString();
                string FinishDate = ResultSet["finishdate"].ToString();
                string ClassName = ResultSet["classname"].ToString();

                NewClasses.ClassId = ClassId;
                NewClasses.ClassCode = ClassCode;
                NewClasses.TeacherId = TeacherId;
                NewClasses.StartDate = StartDate;
                NewClasses.FinishDate = FinishDate;
                NewClasses.ClassName = ClassName;
            }

                Conn.Close();

                return NewClasses;
            
        }

    }
}