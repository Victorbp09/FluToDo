using FlueToDo.App.DTO;
using FluToDo.App.Components.Interfaces;
using FluToDo.App.ViewModels;
using FluToDo.Service.Http.Interfaces;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xunit;

namespace FluToDo.App.Test
{
    public class CreateToDoItemViewModelTests
    {
        private Mock<IToDoItemsService> _service;

        [Fact]
        public async Task CreateToDoItem_NotFire_WhenNewToDoItemNameIsNotValid()
        {
            // arrange
            SetupToDoItemsServiceWithItems(new List<ToDoItem>());
            var viewModel = SetupCreateToDoItemViewModel();
            viewModel.NewToDoName = string.Empty;
            var newToDoItem = new ToDoItem { Name = viewModel.NewToDoName };

            // act
            await Task.Run(() => viewModel.SaveNewToDoItemCommand.Execute(null));

            // assert
            _service.Verify(x => x.CreateToDoItem(newToDoItem), Times.Never);
        }

        [Fact]
        public async Task CreateToDoItem_FiresOnce_WhenNewToDoItemNameIsValid()
        {
            // arrange
            Infraestructure.MockForms.Init();
            SetupToDoItemsServiceWithItems(new List<ToDoItem>());
            var viewModel = SetupCreateToDoItemViewModel();
            viewModel.NewToDoName = "validName";
            var newToDoItem = new ToDoItem { Name = viewModel.NewToDoName };

            // act
            await Task.Run(() => viewModel.SaveNewToDoItemCommand.Execute(null));

            // assert
            _service.Verify(x => x.CreateToDoItem(newToDoItem), Times.Once);
        }

        private CreateToDoItemViewModel SetupCreateToDoItemViewModel()
        {
            var viewModel = new CreateToDoItemViewModel(
                _service.Object,
                Mock.Of<IToast>());

            return viewModel;
        }

        private void SetupToDoItemsServiceWithItems(List<ToDoItem> toDoItems)
        {
            _service = new Mock<IToDoItemsService>();
        }
    }
}
