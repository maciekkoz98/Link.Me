using LinkMe.Core.Entities;
using LinkMe.Core.Enums;
using Xunit;

namespace LinkMe.Tests
{
    public class LinkShortenerTest
    {
        [Fact]
        public void ShortLinkTest()
        {
            var link = new Link
            {
                OriginalLink = "https://www.isod.ee.pw.edu.pl",
            };
            link.GenerateShortLink(UserType.Unregistered);

            Assert.True(link.ShortLink.Length < 9);
        }
    }
}
