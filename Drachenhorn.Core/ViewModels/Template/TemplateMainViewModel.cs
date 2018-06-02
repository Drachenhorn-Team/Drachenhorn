using System;
using System.Collections.Generic;
using System.Text;
using Drachenhorn.Core.IO;
using Drachenhorn.Xml.Exceptions;
using Drachenhorn.Xml.Template;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;

namespace Drachenhorn.Core.ViewModels.Template
{
    public class TemplateMainViewModel : ViewModelBase
    {
        private DSATemplate _template;

        public DSATemplate Template
        {
            get { return _template; }
            set
            {
                if (_template == value)
                    return;
                _template = value;
                RaisePropertyChanged();
            }
        }

        #region c'tor

        public TemplateMainViewModel()
        {
            InitializeCommands();
        }

        #endregion c'tor

        #region Commands

        private void InitializeCommands()
        {
            Save = new RelayCommand(ExecuteSave);
        }

        public RelayCommand Save { get; private set; }

        private void ExecuteSave()
        {
            Template.Save();
        }

        #endregion Commands
    }
}
