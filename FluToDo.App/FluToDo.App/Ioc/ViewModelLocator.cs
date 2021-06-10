using CommonServiceLocator;
using FluToDo.App.ViewModels;

namespace FluToDo.App.Ioc
{
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            Bootstrapper.Initialize();
        }

        public ToDoItemsViewModel ToDoItemsViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ToDoItemsViewModel>();
            }
        }

        public CreateToDoItemViewModel CreateToDoItemViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CreateToDoItemViewModel>();
            }
        }
    }
}
