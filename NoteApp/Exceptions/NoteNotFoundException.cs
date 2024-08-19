namespace NoteApp.Exceptions
{
    public class NoteNotFoundException : Exception
    {
        public NoteNotFoundException(int id) : base($"Note with id = {id} not found") { } 
    }
}
