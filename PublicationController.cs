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

namespace RAP.Controller
{
    class PublicationController
    {
        //The example shown here follows the pattern discussed in the Module 6 summary slides,
        //maintaining two collections, a master and a 'viewable' one (which is the 'view model'
        //in Microsoft's Model-View-ViewModel approach to Model-View-Controller)
        private List<Publication> publications;
       
        private ObservableCollection<Publication> viewablePublications;
        public ObservableCollection<Publication> VisibleSubPublications { get { return viewablePublications; } set { } }

        public PublicationController()
        {
            //publications = Agency.LoadPublications();
            //viewableResearchers = new ObservableCollection<Publication>(publications);
        }
    }
}
