using Drachenhorn.Core.Lang;
using Drachenhorn.Core.UI;
using Drachenhorn.Xml.Template;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;

namespace Drachenhorn.Core.ViewModels.Template
{
    public class TemplateViewModel : ViewModelBase
    {
        #region Properties

        private SheetTemplate _template;

        public SheetTemplate Template
        {
            get => _template;
            set
            {
                if (_template == value)
                    return;
                _template = value;
                RaisePropertyChanged();
            }
        }

        #endregion

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
                var result = await MessageFactory.NewMessage()
                    .MessageTranslated("Dialog.ShouldClose")
                    .TitleTranslated("Dialog.ShouldClose_Caption")
                    .ButtonTranslated("Dialog.Yes", 0)
                    .ButtonTranslated("Dialog.No")
                    .ShowMessage();
                if (result != 0) return;
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