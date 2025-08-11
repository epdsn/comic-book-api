using ComicBookApi.Models;

namespace ComicBookApi.Repositories {
    public class ComicBookRepository : IComicBookRepository {

        private readonly ICollection<ComicBook> _comicBooks;

        public ComicBookRepository() {
            _comicBooks = new List<ComicBook>();
        }

        public async Task<IEnumerable<ComicBook>> GetAllAsync() {
            return await Task.FromResult(_comicBooks.AsEnumerable());
        }

        public async Task<ComicBook?> GetByIdAsync(int id) {
            return await Task.FromResult(_comicBooks.FirstOrDefault(c => c.Id == id));
        }

        public async Task<ComicBook> CreateAsync(ComicBook comicBook) {
            comicBook.Id = _comicBooks.Count > 0 ? _comicBooks.Max(c => c.Id) + 1 : 1;
            _comicBooks.Add(comicBook);
            return await Task.FromResult(comicBook);
        }
        
        public async Task<ComicBook> UpdateAsync(ComicBook comicBook) {
            var existingComic = _comicBooks.FirstOrDefault(c => c.Id == comicBook.Id);
            if (existingComic != null) {
                _comicBooks.Remove(existingComic);
                _comicBooks.Add(comicBook);
            }
            return await Task.FromResult(comicBook);
        }

        public async Task<ComicBook?> DeleteAsync(int id) {
            var comicBook = _comicBooks.FirstOrDefault(c => c.Id == id);
            if (comicBook == null) {
                return null;
            }
            _comicBooks.Remove(comicBook);
            return await Task.FromResult(comicBook);
        }

        public async Task<bool> SaveAsync() {
            return await Task.FromResult(true);
        }
    }
}