using ClansApp.Data.Models;
using ClansApp.UI.Wrappers;
using NUnit.Framework;
using System;

namespace ClansApp.Tests
{
    [TestFixture]
    public class WrapperTests
    {
        private Member member;
        private MemberWrapper memberWrapper;

        [OneTimeSetUp]
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
        public void IsMemberEqualToWrapper()
        {
            foreach (var property in member.GetType().GetProperties())
            {
                var wrapperProperty = memberWrapper.GetType().GetProperty(property.Name);
                if (wrapperProperty != null)
                {
                    Assert.AreEqual(property.GetValue(member), wrapperProperty.GetValue(memberWrapper));
                }
            }
        }

        [Test]
        public void DoesWrapperChangeAffectsMember()
        {
            memberWrapper.Role = "Leader";
            Assert.AreEqual(member.Role, memberWrapper.Role);
        }
    }
}
