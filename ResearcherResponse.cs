using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAP.DTO;

namespace RAP.DTO
{
    class ResearcherResponse
    {
        public string Family_name { get; set; }

        public string Given_name { get; set; }

        public string Title { get; set; }

        public ResearcherResponse(string family_name,string given_name,string title)
        {
            Family_name = family_name;
            Given_name = given_name;
            Title = title;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
