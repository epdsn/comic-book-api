using ComicBookApi.Models;

namespace ComicBookApi.Repositories {
    public interface IComicBookRepository {

        Task<IEnumerable<ComicBook>> GetAllAsync();
        
        Task<ComicBook?> GetByIdAsync(int id);
        
        Task<ComicBook> CreateAsync(ComicBook comicBook);
        
        Task<ComicBook> UpdateAsync(ComicBook comicBook);
        
        Task<ComicBook?> DeleteAsync(int id);

        Task<bool> SaveAsync();
    
    }
}