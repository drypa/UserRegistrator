using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows.Input;
using Registrator.Annotations;
using WcfCommon.Domain;

namespace Registrator
{
    public class FormViewModel : INotifyPropertyChanged
    {
        private string _lastName;
        private string _firstName;
        private CityViewModel _city;
        private bool _savingProcess;

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion INotifyPropertyChanged

        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                OnPropertyChanged("LastName");
            }
        }

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                OnPropertyChanged("FirstName");
            }
        }

        public CityViewModel City
        {
            get { return _city; }
            set
            {
                _city = value;
                OnPropertyChanged("City");
            }
        }

        public bool SavingProcess
        {
            get { return _savingProcess; }
            set
            {
                _savingProcess = value;
                OnPropertyChanged("SavingProcess");
            }
        }

        public IEnumerable<CityViewModel> Cities
        {
            get
            {
                using (var client = CreateClient())
                {
                    string term = City == null ? null : City.Name;
                    return client.GetCities(term).Select(x => new CityViewModel { Id = x.Id, Name = x.Name });
                }
            }
        }

        private ICommand _saveCommand;

        public ICommand SaveCommand
        {
            get
            {
                return _saveCommand ?? (_saveCommand = new RelayCommand(
                    param => SaveObjectAsync(),
                    param => CanSave()
                    ));
            }
        }

        private bool CanSave()
        {
            return !string.IsNullOrEmpty(FirstName) && !string.IsNullOrEmpty(LastName) && City != null;
        }

        private void Save()
        {
            SavingProcess = true;
            using (var client = CreateClient())
            {
                client.AddUser(new User
                {
                    CityId = City.Id,
                    FirstName = FirstName,
                    LastName = LastName,
                    Id = Guid.NewGuid()
                });
            }
            SavingProcess = false;
        }

        private RegistrationServiceClient CreateClient()
        {
            return new RegistrationServiceClient();
        }

        private void SaveObjectAsync()
        {
            var thread = new Thread(Save);
            thread.Start();
        }

    }
}