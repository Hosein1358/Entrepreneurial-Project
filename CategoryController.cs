using Teaching.DataAccess.Data;
using Teaching.Model.Entities;
using Microsoft.AspNetCore.Mvc;
using Teaching.DataAccess.Repository;
using Teaching.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Teaching.Utility;

namespace TeachingWeb.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CategoryController : Controller
    {
        //private readonly ICategoryRepository _categRepos;
        //public CategoryController(ICategoryRepository categRepos)
        //{
        //    _categRepos = categRepos;
        //}
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _unitOfWork.CategoryRepos.GetAll();
            //IEnumerable<Category> objCategoryList = _categRepos.GetAll();
            //IEnumerable<Category> objCategoryList = _db.Categories;
            return View(objCategoryList);
        }

        //----------------------------------------------------------
        //Get
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString()) //Custom Validation
            {
                ModelState.AddModelError("name", "Name can not be tha same as Display Order");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.CategoryRepos.Add(obj);
                _unitOfWork.Save();
                //_categRepos.Add(categ);
                //_categRepos.Save();
                //await _db.Categories.AddAsync(categ);
                //await _db.SaveChangesAsync();
                TempData["Success"] = "Category Created Successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //----------------------------------------------------------
        //GET
        public IActionResult Edit(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }
            var categoryFromDb = _unitOfWork.CategoryRepos.GetFirstOrDefault(c => c.Id == id);
            //var categoryFromDb = _categRepos.GetFirstOrDefault(x => x.Id == id);
            //var categoryFromDb = _db.Categories.Find(id);
            ////var categoryFromDbFirst=_db.Categories.FirstOrDefault(c => c.Id == id);
            ////var categoryFromDbSingle = _db.Categories.SingleOrDefault(c => c.Id == id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "Name can not be tha same as Display Order");
                return View(category);
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.CategoryRepos.Update(category);
                _unitOfWork.Save();
                //_categRepos.Update(category);
                //_categRepos.Save();
                //_db.Categories.Update(category);
                //_db.SaveChanges();
                TempData["Success"] = "Category Updated Successfully";
                return RedirectToAction("Index");
            }
            return View(category);
        }

        //----------------------------------------------------------
        //GET
        public IActionResult Delete(int? iddel)
        {
            if (iddel == 0 || iddel == null)
            {
                return NotFound();
            }
            var categoryFromDb = _unitOfWork.CategoryRepos.GetFirstOrDefault(x => x.Id == iddel);
            //var categoryFromDb = _categRepos.GetFirstOrDefault(x => x.Id == iddel);
            //var categoryFromDb = _db.Categories.Find(iddel);
            ////var categoryFromDbFirst=_db.Categories.FirstOrDefault(c => c.Id == id);
            ////var categoryFromDbSingle = _db.Categories.SingleOrDefault(c => c.Id == id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        [HttpPost]        //[HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? iD)
        {
            if (iD == 0 || iD == null)
            {
                return NotFound();
            }
            var categoryFromDb = _unitOfWork.CategoryRepos.GetFirstOrDefault(x => x.Id == iD);
            //var categoryFromDb = _categRepos.GetFirstOrDefault(x => x.Id == iD);
            //var categoryFromDb = _db.Categories.Find(iD);

            if (ModelState.IsValid)
            {
                _unitOfWork.CategoryRepos.Remove(categoryFromDb);
                _unitOfWork.Save();
                //_categRepos.Remove(categoryFromDb);
                //_categRepos.Save();
                //_db.Categories.Remove(categoryFromDb);
                //_db.SaveChanges();
                TempData["Success"] = "Category Deleted Successfully";
                return RedirectToAction("Index");
            }
            return View(categoryFromDb);
        }

        //----------------------------------------------------------
    }
}
