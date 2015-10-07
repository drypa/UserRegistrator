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
        private ICommand _saveCommand;
        private ICommand _cancelCommand;

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion INotifyPropertyChanged

        #region viewModel props
        public virtual string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                OnPropertyChanged("LastName");
            }
        }

        public virtual string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                OnPropertyChanged("FirstName");
            }
        }

        public virtual CityViewModel City
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

        public ICommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                {
                    _saveCommand = new RelayCommand(
                        param => Clear(),
                        param => IsValid()
                    );
                }
                return _saveCommand;
            }
        }

        public ICommand CancelCommand
        {
            get
            {
                if (_cancelCommand == null)
                {
                    _cancelCommand = new RelayCommand(
                        param => Clear(),
                        param => true
                        );
                }
                return _cancelCommand;
            }
        }

        #endregion viewModel props

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

        private void Clear()
        {
            LastName = string.Empty;
            FirstName = string.Empty;
            City = null;
        }

        #region save
        private bool IsValid()
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

        private void SaveObjectAsync()
        {
            Thread thread = new Thread(Save);
            thread.Start();
        }
        #endregion save

        #region helpers
        private RegistrationServiceClient CreateClient()
        {
            return new RegistrationServiceClient();
        }
        #endregion helpers
        

    }
}