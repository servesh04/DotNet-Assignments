using NUnit.Framework;
using Moq;
using LibraryMembershipApp.Interfaces;
using LibraryMembershipApp.Models;
using LibraryMembershipApp.Services;

namespace LibraryManagementApp.Tests
{
    [TestFixture]
    public class LibraryServiceTests
    {
        private Mock<IMemberRepository> _mockMemberRepo;
        private Mock<IBookRepository> _mockBookRepo;
        private Mock<INotificationService> _mockNotificationService;
        private LibraryService _libraryService;

        [SetUp]
        public void SetUp()
        {
            _mockMemberRepo = new Mock<IMemberRepository>();
            _mockBookRepo = new Mock<IBookRepository>();
            _mockNotificationService = new Mock<INotificationService>();

            _libraryService = new LibraryService(_mockMemberRepo.Object, _mockBookRepo.Object, _mockNotificationService.Object);

        }

        [Test]
        public void BorrowBook_WhenAllConditionsAreValid_ShouldReturnSuccessMessage()
        {
            int memberId = 1;
            int bookId = 100;
            var activeMember = new Member { MemberId = memberId, Email = "test@gmail.com", IsActive = true, BorrowedBookCount = 2, IsPremiumMember = false };
            var availableBook = new Book { BookId = bookId, BookTitle = "Percy Jackson", IsAvailable = true };

            _mockBookRepo.Setup(r => r.GetBookById(bookId)).Returns(availableBook);
            _mockMemberRepo.Setup(r => r.GetMemberById(memberId)).Returns(activeMember);

            string result = _libraryService.BorrowBook(memberId, bookId);

            Assert.That(result, Is.EqualTo("Book borrowed successfully"));

            _mockBookRepo.Verify(r => r.MarkBookAsBorrowed(bookId), Times.Once);
            _mockMemberRepo.Verify(r => r.UpdateBorrowedBookCount(It.IsAny<int>()), Times.Once);
            _mockNotificationService.Verify(n => n.SendBorrowedNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Once);


        }

        [Test]
        public void BorrowBook_WhenBookIsNotAvailable_ShouldReturnBookIsNotAvailable()
        {
            int memberId = 1;
            int bookId = 100;
            var member = new Member { MemberId = memberId, IsActive = true, BorrowedBookCount = 0 };
            var unavailableBook = new Book { BookId = bookId, IsAvailable = false };

            _mockMemberRepo.Setup(r => r.GetMemberById(memberId)).Returns(member);
            _mockBookRepo.Setup(r => r.GetBookById(bookId)).Returns(unavailableBook);

            string result = _libraryService.BorrowBook(memberId, bookId);

            Assert.That(result, Is.EqualTo("Book is not available"));

            _mockBookRepo.Verify(r => r.MarkBookAsBorrowed(It.IsAny<int>()), Times.Never);
            _mockMemberRepo.Verify(r => r.UpdateBorrowedBookCount(It.IsAny<int>()), Times.Never);
            _mockNotificationService.Verify(n => n.SendBorrowedNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Test] 
        public void BorrowBook_WhenNormalMemberHasThreeBooks_ShouldReturnBorrowingLimitReached()
        {

            int memberId = 1;
            int bookId = 100;
            var normalMember = new Member { MemberId = memberId, IsActive = true, BorrowedBookCount = 3, IsPremiumMember = false };

            _mockMemberRepo.Setup(r => r.GetMemberById(memberId)).Returns(normalMember);

            string result = _libraryService.BorrowBook(memberId, bookId);

            Assert.That(result, Is.EqualTo("Borrowing limit reached"));

            _mockBookRepo.Verify(r => r.MarkBookAsBorrowed(It.IsAny<int>()), Times.Never);
            _mockMemberRepo.Verify(r => r.UpdateBorrowedBookCount(It.IsAny<int>()), Times.Never);
            _mockNotificationService.Verify(n => n.SendBorrowedNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Test] 
        public void BorrowBook_WhenMemberIdIsInvalid_ShouldReturnInvalidMemberId()
        {
            int invalidMemberId = 0;
            int bookId = 100;

            string result = _libraryService.BorrowBook(invalidMemberId, bookId);

            Assert.That(result, Is.EqualTo("Invalid member id"));

            _mockMemberRepo.Verify(r => r.GetMemberById(It.IsAny<int>()), Times.Never);
            _mockBookRepo.Verify(r => r.GetBookById(It.IsAny<int>()), Times.Never);
            _mockBookRepo.Verify(r => r.MarkBookAsBorrowed(It.IsAny<int>()), Times.Never);
            _mockMemberRepo.Verify(r => r.UpdateBorrowedBookCount(It.IsAny<int>()), Times.Never);
            _mockNotificationService.Verify(n => n.SendBorrowedNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Test] 
        public void BorrowBook_WhenBookIdIsInvalid_ShouldReturnInvalidBookId()
        {
            int memberId = 1;
            int invalidBookId = -5;

            string result = _libraryService.BorrowBook(memberId, invalidBookId);

            Assert.That(result, Is.EqualTo("Invalid book id"));

            _mockMemberRepo.Verify(r => r.GetMemberById(It.IsAny<int>()), Times.Never);
            _mockBookRepo.Verify(r => r.GetBookById(It.IsAny<int>()), Times.Never);
            _mockBookRepo.Verify(r => r.MarkBookAsBorrowed(It.IsAny<int>()), Times.Never);
            _mockMemberRepo.Verify(r => r.UpdateBorrowedBookCount(It.IsAny<int>()), Times.Never);
            _mockNotificationService.Verify(n => n.SendBorrowedNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Test] 
        public void BorrowBook_WhenPremiumMemberHasThreeBooks_ShouldAllowBorrowing()
        {
            int memberId = 1;
            int bookId = 100;
            var premiumMember = new Member { MemberId = memberId, Email = "prem@lib.com", IsActive = true, BorrowedBookCount = 3, IsPremiumMember = true };
            var book = new Book { BookId = bookId, BookTitle = "C# Advanced", IsAvailable = true };

            _mockMemberRepo.Setup(r => r.GetMemberById(memberId)).Returns(premiumMember);
            _mockBookRepo.Setup(r => r.GetBookById(bookId)).Returns(book);

            string result = _libraryService.BorrowBook(memberId, bookId);

            Assert.That(result, Is.EqualTo("Book borrowed successfully"));
        }

        [Test] 
        public void BorrowBook_WhenPremiumMemberHasFiveBooks_ShouldReturnBorrowingLimitReached()
        {

            int memberId = 1;
            int bookId = 100;
            var premiumMember = new Member { MemberId = memberId, IsActive = true, BorrowedBookCount = 5, IsPremiumMember = true };

            _mockMemberRepo.Setup(r => r.GetMemberById(memberId)).Returns(premiumMember);

            string result = _libraryService.BorrowBook(memberId, bookId);

            Assert.That(result, Is.EqualTo("Borrowing limit reached"));
        }
    }
}
