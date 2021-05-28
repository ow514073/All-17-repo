using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KIT206_Week9
{

    //As an example, this includes an additional 'gender' called Any that could be used in a GUI drop-down list.
    //The filtering could then be modified that if Gender.Any is selected that the full list is returned with no filtering performed.
    public enum gender { Any, M, F, X };
    public enum type { Staff, Student };    // added this, not sure if should be an enum but i think so
    public enum title { Dr, Ms, Mr };       // same as above, might need Mrs too but not in database
    public enum campus {  Hobart, Launceston, Cradle Coast }    // same as above

    /// <summary>
    /// A class for a university researcher
    /// </summary>
    public class Researcher
    {
        public int id { get; set; } // researcher's uniques id number
        public string name { get; set; }   // researcher's first name
        public gender gender { get; set; }  // gender of the researcher
        public type type { get; set; }      // type of the researcher
        public title title { get; set; }    // title of the researcher
        public string unit { get; set; }    // unit that the researcher teaches/studies
        public campus campus { get; set; }  // campus that the researcher attends
        public string email { get; set; }   // email address
        public string photo { get; set; }   // string of hyperlink to photo
        public string degree { get; set; }  // degree that the researcher is pursuing (can be null)
        public int supervisor_id { get; set; }  // id of their supervisor (can be null)
        public string level { get; set; }    // level of researcher that the researcher is (can be null)
        public DateTime utas_start { get; set; }    // date researcher started at utas
        public DateTime current_start { get; set; } // date researcher started their current position
        public List<Publication> Publications { get; set; } // list of the researcher's publications

        // the number of publications the researcher has published
        // currently returns all publications in the database???
        public int publication_count
        {
            get { return Publications == null ? 0 : Publications.Count(); }
        }

        // the time in years since the researcher started their current position
        public double tenure
        {
            get { return (DateTime.Today - current_start).Years; }
        }


        /* DOESNT WORK
         * GET WORKING SOMEHOW
         * 3yrAVG, TENURE, PERFORMANCE
        public double three_year_average
        {
            get { return Publications.GetType(age)}
        }
        

        //The tenure of the researcher, out of 10, expressed as a percentage
        public double SkillPercent
        {
            //This is equivalent to SkillCount / 10.0 * 100
            get { return SkillCount * 10.0; }
        }
        */

        public override string ToString()
        {
            // returns the Researcher
            return Researcher;
        }
    }
}
