namespace NoteApp.Abstractions
{
    public interface ITimeProvider
    {
        DateTime UtcNow => DateTime.UtcNow;
    }
}
