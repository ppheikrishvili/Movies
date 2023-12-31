﻿using Movies.Domain.Interface;
using Movies.Domain.Entity;

namespace Movies.Persistence.Seeds;

public class TestSeedsService : ITestSeedsService
{
    public List<ImdbUser> GetImdbUsers(int rowCount) => new DefaultUser().Generate(rowCount);

    public List<Actor> GetImdbActors(int rowCount) => new DefaultActor().Generate(rowCount);
}