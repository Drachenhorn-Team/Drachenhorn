using System;
using System.Collections.Generic;
using System.Text;
using Drachenhorn.Core.IO;
using Drachenhorn.Core.Lang;
using Drachenhorn.Xml.Template;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;

namespace Drachenhorn.Core.ViewModels.Template
{
    public class TemplateViewModel : ViewModelBase
    {
        #region Properties

        private SheetTemplate _template;

        public SheetTemplate Template
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

        #endregion Properties

        #region Commands

        public RelayCommand New => new RelayCommand(ExecuteNew);

        private void ExecuteNew()
        {
            Template = new SheetTemplate();
        }

        public RelayCommand Open => new RelayCommand(ExecuteOpen);

        private async void ExecuteOpen()
        {
            if (Template != null)
            {
                var result = await SimpleIoc.Default.GetInstance<IDialogService>().ShowMessage(
                    LanguageManager.Translate("UI.SouldClose"),
                    LanguageManager.Translate("UI.SouldClose.Caption"),
                    LanguageManager.Translate("UI.Yes"),
                    LanguageManager.Translate("UI.No"), null);
                if (result != true) return;
            }

            Messenger.Default.Send(new NotificationMessage(this, "ShowOpenTemplates"));

            //var file = SimpleIoc.Default.GetInstance<IIoService>()
            //    .OpenDirDialog(TemplateMetadata.BaseDirectory, TemplateMetadata.Extension, "test");

            //if (file != null)
            //    Template = SheetTemplate.Load(file);
        }

        public RelayCommand Save => new RelayCommand(ExecuteSave);

        private void ExecuteSave()
        {
            Template?.Save();
        }

        public RelayCommand Close => new RelayCommand(ExecuteClose);

        private void ExecuteClose()
        {
            Template = null;
        }

        #endregion Commands
    }
}
