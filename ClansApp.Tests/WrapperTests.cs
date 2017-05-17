using ClansApp.Data.Models;
using ClansApp.UI.Wrappers;
using NUnit.Framework;

namespace ClansApp.Tests
{
    [TestFixture]
    public class WrapperTests
    {
        private Member member;
        private MemberWrapper memberWrapper;

        [SetUp]
        public void Setup()
        {
            member = new Member()
            {
                Name = "Test member",
                Donations = 7231
            };
            memberWrapper = new MemberWrapper(member);
        }

        [Test]
        public void AreMemberValuesEqualToWrappers()
        {
            foreach (var property in member.GetType().GetProperties())
            {
                var wrapperProperty = memberWrapper.GetType().GetProperty(property.Name);
                if (wrapperProperty != null)
                {
                    Assert.That(property.GetValue(member), Is.EqualTo(wrapperProperty.GetValue(memberWrapper)));
                }
            }
        }

        [Test]
        public void DoesWrapperChangeAffectsMember()
        {
            memberWrapper.Role = "Leader";
            Assert.That(member.Role, Is.EqualTo(memberWrapper.Role));
        }

        [Test]
        public void IsMemberEqualToWrapperModel()
        {
            Assert.That(member, Is.EqualTo(memberWrapper.Model));
        }
    }
}
