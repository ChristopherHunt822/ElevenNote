using ElevenNote.Data;
using ElevenNote.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Services
{
    public class CategoryService
    {
        private readonly Guid _userId;

        public CategoryService(Guid userId)
        {
            _userId = userId;
        }

        //Create Category

        public bool CreateCategory(CategoryCreate model)
        {
            var entity =
                new Category()
                {
                    OwnerId = _userId,
                    Name = model.Name
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Categories.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<CategoryListItem> GetCategories()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Categories
                        .Where(c => c.OwnerId == _userId)
                        .Select(
                            c =>
                                new CategoryListItem
                                {
                                    CategoryID = c.CategoryId,
                                    Name = c.Name
                                });

                return query.ToArray();
            }
        }

        public CategoryDetail GetCategoryById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Categories
                        //.Where(c => c.CategoryId == id && e.OwnerId == _userId)
                        //.FirstorDefault();
                        .Single(c => c.CategoryId == id && c.OwnerId == _userId);

                return
                    new CategoryDetail
                    {
                        CategoryID = entity.CategoryId,
                        Name = entity.Name,
                        Notes = entity.Notes.Select(
                            n => new NoteListItem
                            {
                                NoteId = n.NoteId,
                                Title = n.Title
                            }
                            ).ToList()
                    };
            }
        }
        public bool UpdateCategory(CategoryEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Categories
                        .Single(c => c.CategoryId == model.CategoryID && c.OwnerId == _userId);

                entity.Name = model.Name;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteCategory(int categoryId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Categories
                        .Single(c => c.CategoryId == categoryId && c.OwnerId == _userId);

                ctx.Categories.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
