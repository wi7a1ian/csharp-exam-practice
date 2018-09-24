using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmExample.Models;
using MvvmExample.Commands;

namespace MvvmExample.ViewModels
{
    /// <summary>
    /// 
    /// http://channel9.msdn.com/Shows/Visual-Studio-Toolbox/Getting-Started-with-MVVM
    /// http://www.codeproject.com/Articles/100175/Model-View-ViewModel-MVVM-Explained
    /// http://www.codeproject.com/Articles/819294/WPF-MVVM-step-by-step-Basics-to-Advance-Level
    /// 
    /// </summary>
    public class StudentViewModel : ViewModelBase
    {
        public ObservableCollection<StudentModel> StudentList { get; set; }
        public string SelectedStudent { get; set; }

        private ICommand _updateStudentNameCommand;
        public ICommand UpdateStudentNameCommand
        {
            get { return _updateStudentNameCommand; }
            set { _updateStudentNameCommand = value; }
        }
        private string _selectedName;
        public string SelectedName
        {
            get { return _selectedName; }
            set
            {
                if (_selectedName != value)
                {
                    _selectedName = value;
                    RaisePropertyChanged("SelectedName");
                }
            }
        }

        public StudentViewModel()
        {
            UpdateStudentNameCommand = new DelegateCommand(new Action<object>(SelectedStudentDetails));
            StudentList = new ObservableCollection<StudentModel> 
            { 
                    new StudentModel { FirstName = "Bruce" },
                    new StudentModel { FirstName = "Harry" },
                    new StudentModel { FirstName = "Stuart" },
                    new StudentModel { FirstName = "Robert" }
            };
        }


        public void SelectedStudentDetails(object parameter)
        {
            if (parameter != null)
                SelectedName = (parameter as MvvmExample.Models.StudentModel).FirstName;
        }
    }
}
