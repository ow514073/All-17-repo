using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows; //for generating a MessageBox upon encountering an error

using MySql.Data.MySqlClient;
using MySql.Data.Types;

namespace KIT206_Week9
{
    abstract class Agency
    {
        //If including error reporting within this class (as this sample does) then you'll need a way
        //to control whether the errors are actually shown or silently ignored, since once you have
        //connected the GUI to the Boss object then the GUI designer will execute its code, which
        //will try to connect to the database to load live data into the GUI at design time.
        private static bool reportingErrors = false;

        //These would not be hard coded in the source file normally, but read from the application's settings (and, ideally, with some amount of basic encryption applied)
        private const string db = "kit206";
        private const string user = "kit206";
        private const string pass = "kit206";
        private const string server = "alacritas.cis.utas.edu.au";

        private static MySqlConnection conn = null;

        //Part of step 2.3.3 in Week 8 tutorial. This method is a gift to you because .NET's approach to converting strings to enums is so horribly broken
        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value);
        }

        /// <summary>
        /// Creates and returns (but does not open) the connection to the database.
        /// </summary>
        private static MySqlConnection GetConnection()
        {
            if (conn == null)
            {
                //Note: This approach is not thread-safe
                string connectionString = String.Format("Database={0};Data Source={1};User Id={2};Password={3}", db, server, user, pass);
                conn = new MySqlConnection(connectionString);
            }
            return conn;
        }

        //For step 2.2 in Week 8 tutorial
        public static List<Researcher> LoadAll()
        {
            List<Employee> researchers = new List<Employee>();

            MySqlConnection conn = GetConnection();
            MySqlDataReader rdr = null;

            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("select id, type, given_name, family_name, title, unit, campus, email, photo, degree, supervisor_id,level, utas_start, current_start from researcher", conn);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    //Note that in your assignment you will need to inspect the *type* of the
                    //employee/researcher before deciding which kind of concrete class to create.
                    researchers.Add(new Researcher
                    {
                        id = rdr.GetInt32(0),
                        type = ParseEnum<type>(rdr.GetString(1)),
                        name = rdr.GetString(2) + " " + rdr.GetString(3),
                        title = ParseEnum<title>(rdr.GetString(4)),
                        unit = rdr.GetString(5),
                        campus = ParseEnum<campus>(rdr.GetString(6)),
                        email = rdr.GetString(7),
                        photo = rdr.GetString(8),
                        degree = rdr.GetString(9),
                        supervisor_id = rdr.GetInt32(10),
                        level = rdr.GetString(11),
                        utas_start = rdr.GetDateTime(12),
                        current_start = rdr.GetDateTime(13)
                    });
                }
            }
            catch (MySqlException e)
            {
                ReportError("loading researchers", e);
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return researchers;
        }

        //For step 2.3 in Week 8 tutorial
        public static List<Publication> LoadPublications(int id)
        {
            List<Publication> publications = new List<Publication>();

            MySqlConnection conn = GetConnection();
            MySqlDataReader rdr = null;

            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("select title, year, type, available " +
                                                    "from publication as pub, researcher_publication as respub " +
                                                    "where pub.doi=respub.doi and researcher_id=?id", conn);

                cmd.Parameters.AddWithValue("id", id);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    publications.Add(new Publication
                    {
                        DOI = rdr.GetString(0),
                        Title = rdr.GetString(1),
                        Authors = rdr.GetString(2),
                        Year = rdr.GetInt32(3),
                        Type = ParseEnum<Type>(rdr.GetString(4)),
                        CiteAs = rdr.GetString(5),
                        Available = rdr.GetDateTime(6)
                    });
                }
            }
            catch (MySqlException e)
            {
                ReportError("loading publications", e);
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return publications;
        }

        /// <summary>
        /// In a more complete application this error would be logged to a file
        /// and the error reported back to the original caller, who is closer
        /// to the GUI and hence better able to produce the error message box
        /// (which would not show the actual error details like this does).
        /// </summary>
        private static void ReportError(string msg, Exception e)
        {
            if (reportingErrors)
            {
                MessageBox.Show("An error occurred while " + msg + ". Try again later.\n\nError Details:\n" + e,
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
