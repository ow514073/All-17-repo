using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAP.Model;
using RAP.Controller;
using RAP.view;

namespace RAP.Model
{
	public enum PubType { Conference, Journal, Other };    // enum of the things a publication can be

    /// <summary>
    /// Publication made by a researcher
    /// </summary>
    public class Publication
    {
        public string DOI { get; set; } // digital object identifier
        public string Title { get; set; }   // title of publication
        public string Authors { get; set; } // authors of publication
        public int Year { get; set; }   // year of publication
		public PubType PubType { get; set; }  // enum type of publication
        public string CiteAs { get; set; }  // string to cite publication as
        public DateTime Available { get; set; }  // date of release of publication

        // age of the publication (todays date - release date)
        public int Age
        {
            get { return (DateTime.Today - Available).Days; }
        }

        public override string ToString()
        {
            //This is a straightforward way of constructing the string using DateTime's
            //ToShortDateString method to remove the time component of the completed date
            return Title + " completed by " + PubType + " on " + Available.ToShortDateString();
            
            //This alternative approach uses the Format method of string, with the
            //short date format requested via the :d in the format string
            //return string.Format("{0} completed by {1} on {2:d}", Title, Mode, Certified);
        }
    }
}

