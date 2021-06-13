using FlueToDo.App.DTO;
using FlueToDo.App.DTO.ApiResponse;
using FluToDo.App.Components.Navigation;
using FluToDo.App.Components.Toast;
using FluToDo.App.ViewModels;
using FluToDo.Service.Http.Interfaces;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
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
            SetupToDoItemsServiceWithItems(new List<ToDoItem>());
            var viewModel = SetupCreateToDoItemViewModel();
            viewModel.NewToDoName = "validName";

            // act
            await Task.Run(() => viewModel.SaveNewToDoItemCommand.Execute(null));

            // assert
            _service.Verify(x => x.CreateToDoItem(It.IsAny<ToDoItem>()), Times.Once);
        }

        private CreateToDoItemViewModel SetupCreateToDoItemViewModel()
        {
            
            var viewModel = new CreateToDoItemViewModel(
                Mock.Of<INavigator>(),
                Mock.Of<IToast>(),
                _service.Object);

            return viewModel;
        }

        private void SetupToDoItemsServiceWithItems(List<ToDoItem> toDoItems)
        {
            _service = new Mock<IToDoItemsService>();

            _service.Setup(x => x.CreateToDoItem(It.IsAny<ToDoItem>()))
            .Returns(() =>
            {
                return Task.Run(() => {
                    return new BaseApiResponse();
                });
            });
        }
    }
}
