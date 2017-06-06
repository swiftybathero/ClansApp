using ClansApp.Data.Models;
using ClansApp.UI.Extensions;
using ClansApp.UI.Serializers;
using ClansApp.UI.Services;
using ClansApp.UI.Services.Messages;
using ClansApp.UI.ViewModels;
using ClansApp.UI.Wrappers;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClansApp.Tests
{
    [TestFixture]
    public class ViewModelTests
    {
        #region Objects/collections
        private WindowFrameViewModel _windowFrameViewModel;
        private ObservableCollection<MemberWrapper> _memberWrapperCollection; 
        #endregion

        #region Mocks
        private Mock<IClansDataService> _clansDataServiceMock;
        private Mock<WindowFrameViewModel> _windowFrameViewModelMock;
        private Mock<ISettingsSerializer> _settingsSerializerMock;
        #endregion

        [SetUp]
        public void SetUp()
        {
            // fill the collection
            _memberWrapperCollection = new ObservableCollection<MemberWrapper>
            {
                new MemberWrapper(new Member() {Name = "First member name", Role = "Test role"}),
                new MemberWrapper(new Member() {Name = "Second member name", Role = "Test role"}),
                new MemberWrapper(new Member() {Name = "Third member name", Role = "Test role"})
            };

            // data service mock
            _clansDataServiceMock = new Mock<IClansDataService>();
            _clansDataServiceMock.Setup(x => x.GetAllMembersAsync()).ReturnsAsync(_memberWrapperCollection);

            // settings serializer mock
            _settingsSerializerMock = new Mock<ISettingsSerializer>();

            // pass mock object to constructor
            _windowFrameViewModelMock = new Mock<WindowFrameViewModel>(_clansDataServiceMock.Object, _settingsSerializerMock.Object);
            _windowFrameViewModel = _windowFrameViewModelMock.Object;

            // to call ShowMembersData private method
            Messenger.Default.Send(new LoginMessage(null, string.Empty));
        }

        [Test]
        public void WasGetAllMembersAsyncCalledOnce()
        {
            // check if method was called only once
            _clansDataServiceMock.Verify(v => v.GetAllMembersAsync(), Times.Once);
        }

        [Test]
        public void AreSettingsSavedOnLogin()
        {
            _windowFrameViewModel.LoginViewModel.LoginCommand.Execute(null);
            _settingsSerializerMock.Verify(v => v.SaveAPIKey(It.IsAny<string>()), Times.AtLeastOnce);
        }

        [Test]
        public void DoesDataViewModelContainMemberListData()
        {
            // checking if collection contains items
            Assert.That(_windowFrameViewModel.DataViewModel.MemberList, Is.Not.Empty);
        }

        [Test]
        public void DoesDataViewModelMemberListDataCointainsAllItems()
        {
            Assert.That(_windowFrameViewModel.DataViewModel.MemberList.Count, Is.EqualTo(_memberWrapperCollection.Count));
        }

        [Test]
        public void IsDataViewModelMemberListDataEqualToOrigin()
        {
            Assert.That(_windowFrameViewModel.DataViewModel.MemberList, Is.EquivalentTo(_memberWrapperCollection));
        }
    }
}
