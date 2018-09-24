using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmExample.ViewModels
{
public class ContactViewModel : ViewModelBase
    {
        /*public ContactViewModel()
        {
            Contacts = new ObservableCollection<ContactModel>();
            Service = new Service();
            
            Service.GetContacts(_PopulateContacts);

            Delete = new DeleteCommand(
                Service, 
                ()=>CanDelete,
                contact =>
                    {
                        CurrentContact = null;
                        Service.GetContacts(_PopulateContacts);
                    });
        }

        private void _PopulateContacts(IEnumerable>ContactModel> contacts)
        {
            Contacts.Clear();
            foreach(var contact in contacts)
            {
                Contacts.Add(contact);
            }
        }

        public IService Service { get; set; }

        public bool CanDelete
        {
            get { return _currentContact != null; }
        }

        public ObservableCollection<ContactModel> Contacts { get; set; }

        public DeleteCommand Delete { get; set; }

        private ContactModel _currentContact;

        public ContactModel CurrentContact
        {
            get { return _currentContact; }
            set
            {
                _currentContact = value;
                RaisePropertyChanged("CurrentContact");
                RaisePropertyChanged("CanDelete");
                Delete.RaiseCanExecuteChanged();
            }
        }*/
    }
}
