using FlueToDo.App.DTO;
using FlueToDo.App.DTO.ApiResponse;
using FluToDo.App.Components.Navigation;
using FluToDo.App.Components.Toast;
using FluToDo.App.ViewModels;
using FluToDo.Service.Http.Interfaces;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace FluToDo.App.Test
{
    public class ToDoItemsViewModelTests
    {
        private Mock<IToDoItemsService> _service;

        [Fact]
        public async Task ToDoItemsProperty_IsEmpty_WhenServiceHasNotItems()
        {
            // arrange
            List<ToDoItem> toDoItems = new List<ToDoItem>();
            SetupToDoItemsServiceWithItems(toDoItems);
            var viewModel = SetupToDoItemsViewModel();

            // act
            await Task.Run(() => viewModel.OnAppearing());

            // assert
            Assert.True(!viewModel.ToDoItems.Any());
            Assert.Equal(viewModel.ToDoItems.Count, toDoItems.Count);
        }

        [Fact]
        public async Task ToDoItemsProperty_IsNotEmpty_WhenServiceHasItems()
        {
            // arrange
            List<ToDoItem> toDoItems = new List<ToDoItem> { _mockToDoItem1, _mockToDoItem2 };
            SetupToDoItemsServiceWithItems(toDoItems);
            var viewModel = SetupToDoItemsViewModel();

            // act
            await Task.Run(() => viewModel.OnAppearing());

            // assert
            Assert.True(viewModel.ToDoItems.Any());
            Assert.Equal(viewModel.ToDoItems.Count, toDoItems.Count);
        }

        [Fact]
        public async Task DeleteToDoItem_DeletesItem_WhenItemKeyExists()
        {
            /// arrange
            List<ToDoItem> toDoItems = new List<ToDoItem> { _mockToDoItem1, _mockToDoItem2 };
            SetupToDoItemsServiceWithItems(toDoItems);
            var viewModel = SetupToDoItemsViewModel();
            await Task.Run(() => viewModel.OnAppearing());

            // act
            await Task.Run(() => viewModel.DeleteToDoItemCommand.Execute(_mockToDoItem1));

            // assert
            var expectedToDoItemsInList = 2;
            Assert.Equal(expectedToDoItemsInList, viewModel.ToDoItems.Count);
            Assert.Equal(viewModel.ToDoItems.First().Key, _mockToDoItem1.Key);
        }

        [Fact]
        public async Task DeleteToDoItem_NotDeleteItem_WhenItemKeyNotExist()
        {
            // arrange
            List<ToDoItem> toDoItems = new List<ToDoItem> { _mockToDoItem1, _mockToDoItem2 };
            SetupToDoItemsServiceWithItems(toDoItems);
            var viewModel = SetupToDoItemsViewModel();
            await Task.Run(() => viewModel.OnAppearing());
            var toDoItemOutOfList = new ToDoItem { Key = "randomKey" };

            // act
            await Task.Run(() => viewModel.DeleteToDoItemCommand.Execute(toDoItemOutOfList));

            // assert
            var expectedToDoItemsInList = 2;
            Assert.Equal(expectedToDoItemsInList, viewModel.ToDoItems.Count);
            Assert.Equal(viewModel.ToDoItems.First().Key, _mockToDoItem1.Key);
        }

        [Fact]
        public async Task UpdateToDoItem_UpdateIsComplete_WhenItemIsTapped()
        {
            // arrange
            List<ToDoItem> toDoItems = new List<ToDoItem> { _mockToDoItem1, _mockToDoItem2 };
            var currentIsCompleteValue = toDoItems.First().IsComplete;
            SetupToDoItemsServiceWithItems(toDoItems);
            var viewModel = SetupToDoItemsViewModel();
            await Task.Run(() => viewModel.OnAppearing());

            // act
            await Task.Run(() => viewModel.SelectedItem = viewModel.ToDoItems.First());

            // assert
            var expectedIsCompleteValue = !currentIsCompleteValue;
            Assert.Equal(expectedIsCompleteValue, viewModel.ToDoItems.First().IsComplete);
        }

        private ToDoItemsViewModel SetupToDoItemsViewModel()
        {
            Xamarin.Forms.Mocks.MockForms.Init();
            return new ToDoItemsViewModel(
                Mock.Of<IToast>(),
                Mock.Of<INavigator>(),
                _service.Object);
        }

        private void SetupToDoItemsServiceWithItems(List<ToDoItem> toDoItems)
        {
            _service = new Mock<IToDoItemsService>();

            _service.Setup(x => x.GetToDoItems())
            .Returns(() =>
            {
                return Task.Run(() => new GetToDoItemsResponse
                {
                    Items = toDoItems
                });
            });

            _service.Setup(x => x.DeleteToDoItem(_mockToDoItem1.Key))
            .Returns(() =>
            {
                return Task.Run(() => 
                {
                    toDoItems.Remove(_mockToDoItem1);
                    return new BaseApiResponse();
                });
            });

            _service.Setup(x => x.DeleteToDoItem(_mockToDoItem2.Key))
            .Returns(() =>
            {
                return Task.Run(() => {
                    toDoItems.Remove(_mockToDoItem2);
                    return new BaseApiResponse();
                });
            });
        }

        private ToDoItem _mockToDoItem1 = new ToDoItem
        {
            Key = "testKey1",
            Name = "testName1",
            IsComplete = true
        };

        private ToDoItem _mockToDoItem2 = new ToDoItem
        {
            Key = "testKey2",
            Name = "testName2",
            IsComplete = true
        };
    }
}
