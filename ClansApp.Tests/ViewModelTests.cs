using ClansApp.Data.Models;
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
        private WindowFrameViewModel _windowFrameViewModel;
        private ObservableCollection<MemberWrapper> _memberWrapperCollection;

        [SetUp]
        public void SetUp()
        {
            _memberWrapperCollection = new ObservableCollection<MemberWrapper>
            {
                new MemberWrapper(new Member() {Name = "First member name", Role = "Test role"}),
                new MemberWrapper(new Member() {Name = "Second member name", Role = "Test role"}),
                new MemberWrapper(new Member() {Name = "Third member name", Role = "Test role"})
            };

            var clansDataServiceMock = new Mock<IClansDataService>();
            clansDataServiceMock.Setup(x => x.GetAllMembersAsync()).ReturnsAsync(_memberWrapperCollection);

            _windowFrameViewModel = new WindowFrameViewModel(clansDataServiceMock.Object, null);

            // to call ShowMembersData private method
            Messenger.Default.Send(new LoginMessage(null, string.Empty));
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
