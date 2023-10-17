using Movies.Domain.Entity;

namespace Movies.Domain.Interface;

public interface ITestSeedsService
{
    List<ImdbUser> GetImdbUsers(int rowCount);
    List<Actor> GetImdbActors(int rowCount);
}