using NUnit.Framework;

namespace Game.Core.Tests
{
    /// <summary>
    /// Ca kiểm thử khởi tạo môi trường (Sanity Check).
    /// Xác nhận đường ống NUnit EditMode hoạt động độc lập không cần nạp Scene đồ họa.
    /// </summary>
    [TestFixture]
    public class SanityCheckTests
    {
        [Test]
        public void SanityCheck_EnvironmentIsConfigured_ReturnsTrue()
        {
            // Arrange
            const bool isConfigured = true;

            // Act & Assert
            Assert.IsTrue(isConfigured, "Hạ tầng kiểm thử NUnit EditMode độc lập đã sẵn sàng!");
        }
    }
}
