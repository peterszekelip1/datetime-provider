namespace DateTimeProvider.Tests;

public class DateTimeProviderTests
{
    [Fact(DisplayName = "Given DateTimeProviderContext is set to use a fake UtcNow value " +
                        "When DateTimeProvider.Instance.UtcNow is retrieved " +
                        "Then the previously set fakeUtc value is returned")]
    public void DateTimeProviderTests_001()
    {
        var fakeUtcNow = new DateTime(1900, 8, 13, 15, 26, 12, DateTimeKind.Utc);
        using var _ = new DateTimeProviderContext(fakeUtcNow);
        
        var utcNow = DateTimeProvider.Instance.UtcNow;

        utcNow.Should().Be(fakeUtcNow);
    }
    
    [Fact(DisplayName = "Given multiple DateTimeProviderContexts are nested within each other and set to use a different fake UtcNow values " +
                        "When DateTimeProvider.Instance.UtcNow is retrieved multiple times " +
                        "Then the previously set fakeUtc values are returned according to the DateTimeProviderContext they're defined in")]
    public void DateTimeProviderTests_002()
    {
        DateTime utcNow1;
        DateTime utcNow2;
        
        var fakeUtcNow1 = new DateTime(1900, 8, 13, 15, 26, 12, DateTimeKind.Utc);
        var fakeUtcNow2 = new DateTime(1800, 8, 13, 15, 26, 12, DateTimeKind.Utc);
        using (var unused1 = new DateTimeProviderContext(fakeUtcNow1))
        {
            using (var unused2 = new DateTimeProviderContext(fakeUtcNow2))
            {
                utcNow2 = DateTimeProvider.Instance.UtcNow;                
            }
            utcNow1 = DateTimeProvider.Instance.UtcNow;
        }
        
        utcNow1.Should().Be(fakeUtcNow1);
        utcNow2.Should().Be(fakeUtcNow2);
    }
}
