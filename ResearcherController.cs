using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAP.Model;
using RAP.Controller;
using RAP.view;
using RAP.Database;
using System.Security.Policy;
using RAP.DTO;

namespace RAP.Controller
{
    
    class ResearcherController
    {
        //The example shown here follows the pattern discussed in the Module 6 summary slides,
        //maintaining two collections, a master and a 'viewable' one (which is the 'view model'
        //in Microsoft's Model-View-ViewModel approach to Model-View-Controller)
        private List<Researcher> researchers;
        public List<Researcher> subResearchers { get { return researchers; } set { } }

        private ObservableCollection<Researcher> viewableResearchers;
        public ObservableCollection<Researcher> VisibleSubResearchers { get { return viewableResearchers; } set { } }

        public ResearcherController()
        {
            researchers = Agency.LoadAll();
            viewableResearchers = new ObservableCollection<Researcher>(researchers);

            //Part of step 2.3.2 from Week 8 tutorial
            foreach (Researcher r in researchers)
            {
                r.Publications = Agency.LoadPublications(r.id);
            }
        }

        public ObservableCollection<Researcher> GetViewableList()
        {
            return VisibleSubResearchers;
        }

        //This version of Filter modifies the viewable list instead of returning a new list,
        //but the procedure is almost the same
        public void Filter(level level)
        {
            var selected = from Researcher r in researchers
                           where r.level == level
                           select r;
            viewableResearchers.Clear();
            //Converts the result of the LINQ expression to a List and then calls viewableStaff.Add with each element of that list in turn
            selected.ToList().ForEach(viewableResearchers.Add);
        }


    }
}
