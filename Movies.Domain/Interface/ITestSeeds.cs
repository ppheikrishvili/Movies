using Movies.Domain.Entity;

namespace Movies.Domain.Interface;

public interface ITestSeedsService
{
    List<ImdbUser> GetImdbUsers(int rowCount);
}