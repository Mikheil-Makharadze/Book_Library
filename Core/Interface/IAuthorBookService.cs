namespace Core.Interface
{
    public interface IAuthorBookService
    {
        Task CreateAuthorBook(int authorId, int BookId);
        Task RemoveByAuthorId(int Id);
        Task RemoveByBookId(int Id);
    }
}
